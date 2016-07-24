using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class CoffeeSaleViewModel: Abstract.aEntityWindowViewModel
    {
        public CoffeeSaleViewModel(object argSale = null)
        {
            if (argSale != null)
                Sale = argSale as CoffeeSale;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Sale = new CoffeeSale
            {
                Date = DateTime.Now,
                PaymentDate = DateTime.Now,
                Paid = true,
                Account = ContextManager.ActiveAccounts.FirstOrDefault(),
                Recipient = ContextManager.ActiveRecipients.FirstOrDefault()
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
            if (CreatingNew)
            {
                _sale.Sale_Details.Clear();
                foreach (var x in Details)
                    _sale.Sale_Details.Add(x);
                ContextManager.Context.CoffeeSales.Add(_sale);
            }
            SaveContext();
        }

        #endregion
    }
}
