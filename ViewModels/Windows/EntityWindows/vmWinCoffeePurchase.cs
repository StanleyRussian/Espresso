using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinCoffeePurchase: Abstract.aEntityWindowViewModel
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

        protected override void cmdSave_Execute()
        {
            if (Purchase.InvoiceNumber == "0" || Purchase.InvoiceNumber == "")
            {
                FlyErrorMsg = "Введите номер накладной";
                IsFlyErrorOpened = true;
                return;
            }

            if (Details.Count == 0)
            {
                FlyErrorMsg = "Введите хотя бы один сорт кофе";
                IsFlyErrorOpened = true;
                return;
            }

            if (Details.Any(detail => detail.Quantity == 0))
            {
                FlyErrorMsg = "Введите количество закупленного кофе";
                IsFlyErrorOpened = true;
                return;
            }

            if (Details.Any(detail => detail.Price == 0))
            {
                FlyErrorMsg = "Введите цену отличную от нуля";
                IsFlyErrorOpened = true;
                return;
            }

            _purchase.dSum = 0;
            foreach (var detail in Details)
                _purchase.dSum += (detail.Price * detail.Quantity);

            if (ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Purchase.Account.Id).Balance < Purchase.dSum)
            {
                FlyErrorMsg = "На выбранном счету недостаточно денег";
                IsFlyErrorOpened = true;
                return;
            }

            _purchase.CoffeePurchaseDetails.Clear();
            foreach (var detail in Details)
            {
                _purchase.CoffeePurchaseDetails.Add(detail);
                //// Find stocks of green coffee for current coffee sort
                //var greenStock = ContextManager.Context.dGreenStocks.First( 
                //    p => p.CoffeeSort.Id == detail.CoffeeSort.Id);
                //// Check if there anything in stock already
                //if (greenStock.Quantity == 0)
                //    // Set cost from purchase
                //    greenStock.dCost = detail.Price;
                //else
                //    // Count cost based on stock and new purchase
                //    greenStock.dCost = (greenStock.Quantity*greenStock.dCost 
                //                            + detail.Quantity*detail.Price)/
                //                       (greenStock.Quantity + detail.Quantity);
            }

            if (CreatingNew)
                ContextManager.Context.CoffeePurchases.Add(_purchase);
            SaveContext();

            if (CreatingNew)
                Refresh();
        }

        #endregion
    }
}
