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
                Details = new ObservableCollection<CoffeePurchase_Details>(Purchase.CoffeePurchase_Details);
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
            var firstOrDefault = ContextManager.Context.CoffeeSales.OrderByDescending(p => p.InvoiceNumber).FirstOrDefault();
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

            Details = new ObservableCollection<CoffeePurchase_Details>();

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

        private ObservableCollection<CoffeePurchase_Details> _details;
        public ObservableCollection<CoffeePurchase_Details> Details
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

            _purchase.Sum = 0;
            foreach (var detail in Details)
                _purchase.Sum += (detail.Price * detail.Quantity);

            if (ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Purchase.Account.Id).Balance < Purchase.Sum)
            {
                FlyErrorMsg = "На выбранном счету недостаточно денег";
                IsFlyErrorOpened = true;
                return;
            }

            _purchase.CoffeePurchase_Details.Clear();
            foreach (var detail in Details)
            {
                _purchase.CoffeePurchase_Details.Add(detail);
                var dGreenStock = ContextManager.Context.dGreenStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id);
                dGreenStock.dCost = dGreenStock.Quantity/(dGreenStock.Quantity + detail.Quantity)*dGreenStock.dCost +
                                    detail.Quantity/(dGreenStock.Quantity + detail.Quantity)*detail.Price;
            }

            if (CreatingNew)
                ContextManager.Context.CoffeePurchases.Add(_purchase);
            SaveContext();
        }

        #endregion
    }
}
