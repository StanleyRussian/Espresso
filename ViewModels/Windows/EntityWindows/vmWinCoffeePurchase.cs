using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinCoffeePurchase : Abstract.aEntityWindowViewModel
    {

        public vmWinCoffeePurchase(object argEntity) : base(argEntity) { }

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

        private double _sum;
        private double _oldSum;
        private CoffeePurchase _oldPurchase;

        protected override void OnOpenEdit(object argEntity)
        {
            Purchase = argEntity as CoffeePurchase;
            _oldPurchase = Purchase.Clone();
            Details = new ObservableCollection<CoffeePurchaseDetails>(Purchase.CoffeePurchaseDetails);
        }

        protected override void OnOpenNew()
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

            if (_purchase.Paid)
            {
                _sum = Details.Sum(detail => (detail.Price*detail.Quantity));
                if (ContextManager.Context.dAccountsBalances.First(
                    p => p.Account.Id == Purchase.Account.Id).Balance < _sum)
                    throw new Exception("На выбранном счету недостаточно денег");
            }
        }

        protected override void OnSaveEdit()
        {
            // Remove old purchase
            ContextManager.Context.CoffeePurchases.Remove(_oldPurchase);
            // Remove all of it's details
            foreach (var detail in _oldPurchase.CoffeePurchaseDetails)
                ContextManager.Context.CoffeePurchaseDetails.Remove(detail);

            if (_oldPurchase.Paid)
            {
                // Restore account balance
                _oldSum = _oldPurchase.CoffeePurchaseDetails.Sum(detail => (detail.Price*detail.Quantity));
                ContextManager.Context.dAccountsBalances.First(
                    p => p.Account.Id == _oldPurchase.Account.Id).Balance += _oldSum;
                // Delete transaction
                ContextManager.Context.dTransactions.Remove(
                    ContextManager.Context.dTransactions.Find(_oldPurchase.TransactionID));
            }

            foreach (var detail in _oldPurchase.CoffeePurchaseDetails)
            {
                // Find stocks of green coffee for current coffee sort
                var coffeeStock = ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id);

                // Calculate old cost
                coffeeStock.GreenCost = Math.Round((
                    coffeeStock.GreenCost*coffeeStock.GreenQuantity - detail.Price*detail.Quantity)/
                                                   (coffeeStock.GreenQuantity - detail.Quantity), 2);
                // Calculate old stock
                coffeeStock.GreenQuantity -= detail.Quantity;
            }

            OnSaveNew();
        }

        protected override void OnSaveNew()
        {
            // Just for common sense
            if (!_purchase.Paid)
                _purchase.PaymentDate = null;

            // Add details to actual purchase from temp collection
            _purchase.CoffeePurchaseDetails.Clear();
            foreach (var detail in Details)
                _purchase.CoffeePurchaseDetails.Add(detail);

            // All financial operations will be done only if it was actually paid
            if (_purchase.Paid)
            {
                // Change account balance
                ContextManager.Context.dAccountsBalances.First(
                    p => p.Account.Id == Purchase.Account.Id).Balance -= _sum;
                // Add new transaction
                var transaction = ContextManager.Context.dTransactions.Add(new dTransaction
                {
                    Account = Purchase.Account,
                    Date = Purchase.Date,
                    Description = "Закупка кофе",
                    Participant = Purchase.Supplier.Name,
                    Sum = -_sum
                });
                // Tie it to purchase
                //DOES NOT WORK
                _purchase.TransactionID = transaction.Id;
            }

            // Add purchase to databse
            ContextManager.Context.CoffeePurchases.Add(_purchase);

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
                // Change green coffee stocks
                coffeeStock.GreenQuantity += detail.Quantity;
            }
        }
    }
}