using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinCoffeeSale: Abstract.aEntityWindowViewModel
    {
        public vmWinCoffeeSale(object argSale = null)
        {
            if (argSale != null)
            {
                Sale = argSale as CoffeeSale;
                Details = new ObservableCollection<CoffeeSale_Details>(Sale.CoffeeSale_Details);
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

            Sale = new CoffeeSale
                {
                    Date = DateTime.Now,
                    PaymentDate = DateTime.Now,
                    Paid = true,
                    Account = ContextManager.ActiveAccounts.FirstOrDefault(),
                    Recipient = ContextManager.ActiveRecipients.FirstOrDefault(),
                    InvoiceNumber = iInvoiceNumber.ToString()
                };

            Details = new ObservableCollection<CoffeeSale_Details>();
        }

        #region Binding Properties

        private CoffeeSale _sale;
        public CoffeeSale Sale
        {
            get { return _sale; }
            set
            {
                _sale = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CoffeeSale_Details> _details;
        public ObservableCollection<CoffeeSale_Details> Details
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
            if (Sale.InvoiceNumber == "0" || Sale.InvoiceNumber == "")
            {
                FlyErrorMsg = "Введите номер накладной";
                IsFlyErrorOpened = true;
                return;
            }
            if (Details.Count == 0)
            {
                FlyErrorMsg = "Введите хотя бы один купаж кофе";
                IsFlyErrorOpened = true;
                return;
            }
            if (Details.Any(detail => detail.PackQuantity == 0))
            {
                FlyErrorMsg = "Введите количество проданных пачек";
                IsFlyErrorOpened = true;
                return;
            }
            if (Details.Any(detail => detail.Package == null))
            {
                FlyErrorMsg = "Выберите подходящую упаковку из списка";
                IsFlyErrorOpened = true;
                return;
            }
            if (Details.Any(detail => detail.Mix == null))
            {
                FlyErrorMsg = "Выберите подходящий купаж из списка";
                IsFlyErrorOpened = true;
                return;
            }

            if (Details.Any(detail => ContextManager.Context.dPackedStocks.First(
                p => p.Package.Id == detail.Package.Id
                     && p.Mix.Id == detail.Mix.Id)
                .PackQuantity < detail.PackQuantity))
            {
                FlyErrorMsg = "Недостаточно расфасованного кофе в наличии";
                IsFlyErrorOpened = true;
                return;
            }

            _sale.Sum = 0;
            foreach (var detail in Details)
                _sale.Sum += (detail.Price*detail.PackQuantity);

            _sale.CoffeeSale_Details.Clear();
            foreach (var x in Details)
                _sale.CoffeeSale_Details.Add(x);

            if (CreatingNew)
                ContextManager.Context.CoffeeSales.Add(_sale);
            SaveContext();
            if (CreatingNew)
                Refresh();
        }

        #endregion
    }
}
