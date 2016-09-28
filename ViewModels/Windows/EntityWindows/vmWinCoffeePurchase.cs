using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinCoffeePurchase : Abstract.aEntityWindowViewModel
    {
        public vmWinCoffeePurchase(object argPurchase)
        {
            if (argPurchase != null)
            {
                Purchase = argPurchase as CoffeePurchase;
                Details = new ObservableCollection<CoffeePurchaseDetails>(Purchase.CoffeePurchaseDetails);
            }
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            int iInvoiceNumber;
            var firstOrDefault = ContextManager.Context.Sales.OrderByDescending(p => p.InvoiceNumber).FirstOrDefault();
            if (firstOrDefault != null)
            {
                iInvoiceNumber = int.Parse(firstOrDefault.InvoiceNumber);
                iInvoiceNumber++;
            }
            else
                iInvoiceNumber = 1;

            Purchase = new CoffeePurchase
            {
                Date = DateTime.Now,
                PaymentDate = DateTime.Now,
                Paid = true,
                Account = ContextManager.ActiveAccounts.FirstOrDefault(),
                Supplier = ContextManager.ActiveSuppliers.FirstOrDefault(),
                InvoiceNumber = iInvoiceNumber.ToString()
            };

            Details = new ObservableCollection<CoffeePurchaseDetails>();
        }

        #region Binding Properties

        private CoffeePurchase _purchase;

        public CoffeePurchase Purchase
        {
            get { return _purchase; }
            set
            {
                _purchase = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CoffeePurchaseDetails> _details;

        public ObservableCollection<CoffeePurchaseDetails> Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        private double _sum;

        protected override void OnSaveValidation()
        {
            if (Purchase.InvoiceNumber == "0" || Purchase.InvoiceNumber == "")
                throw new Exception("Введите номер накладной");

            if (Details.Count == 0)
                throw new Exception("Введите хотя бы один сорт кофе");

            if (Details.Any(detail => detail.Quantity == 0))
                throw new Exception("Введите количество закупленного кофе");

            if (Details.Any(detail => detail.Price == 0))
                throw new Exception("Введите цену отличную от нуля");

            _sum = Details.Sum(detail => (detail.Price * detail.Quantity));

            if (ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Purchase.Account.Id).Balance < _sum)
                throw new Exception("На выбранном счету недостаточно денег");
        }

        protected override void OnSaveCreate()
        {
            // Just for common sense
            if (!_purchase.Paid)
                _purchase.PaymentDate = null;

            // Add details to actual purchase from temp collection
            _purchase.CoffeePurchaseDetails.Clear();
            foreach (var detail in Details)
                _purchase.CoffeePurchaseDetails.Add(detail);

            // Add purchase to databse
            ContextManager.Context.CoffeePurchases.Add(_purchase);

            // All financial operations will be done only if it was actually paid
            if (_purchase.Paid)
            {
                // Change account balance
                ContextManager.Context.dAccountsBalances.First(
                    p => p.Account.Id == Purchase.Account.Id).Balance -= _sum;
                // Add new transaction
                ContextManager.Context.dTransactions.Add(new dTransaction
                {
                    Account = Purchase.Account,
                    Date = Purchase.Date,
                    Description = "Закупка кофе",
                    Participant = Purchase.Supplier.Name,
                    Sum = -_sum
                });
            }

            // Correct cost of green coffee stocks
            foreach (var detail in Details)
            {
                // Find stocks of green coffee for current coffee sort
                var coffeeStock = ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id);
                // Check if there anything in stock already
                if (coffeeStock.GreenQuantity == 0)
                    // Set cost from purchase
                    coffeeStock.GreenCost = detail.Price;
                else
                // Count cost based on stock and new purchase
                    coffeeStock.GreenCost = Math.Round((coffeeStock.GreenQuantity*coffeeStock.GreenCost
                                                        + detail.Quantity*detail.Price)/
                                                       (coffeeStock.GreenQuantity + detail.Quantity), 2);
            }

            foreach (var detail in Details)
            {
                // Find stocks of green coffee for current coffee sort
                var coffeeStock = ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id);
                // Change green coffee stocks
                coffeeStock.GreenQuantity += detail.Quantity;
            }
        }

        #endregion
    }
}