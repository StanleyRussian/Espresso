using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewCoffeeSaleViewModel: Abstract.aCreationalViewModel
    {
        public NewCoffeeSaleViewModel() : base() { }

        protected override void Refresh()
        {
            NewSale = new Entity.CoffeeSale();
            Details = new ObservableCollection<Entity.CoffeeSale_Details>();

            NewSale.Date = DateTime.Now;
            NewSale.PaymentDate = DateTime.Now;
            NewSale.Paid = true;
            NewSale.Account = ContextManager.ActiveAccounts.FirstOrDefault();
            NewSale.Recipient = ContextManager.ActiveRecipients.FirstOrDefault();
        }

        #region Binding Properties

        private Entity.CoffeeSale _newSale;
        public Entity.CoffeeSale NewSale
        {
            get { return _newSale; }
            set
            {
                _newSale = value;
                OnPropertyChanged("NewSale");
            }
        }

        private ObservableCollection<Entity.CoffeeSale_Details> _details;
        public ObservableCollection<Entity.CoffeeSale_Details> Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged("Details");
            }
        }
        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            _newSale.Sale_Details.Clear();
            foreach (var x in Details)
                _newSale.Sale_Details.Add(x);
            _context.CoffeeSales.Add(_newSale);

            SaveContext();
        }

        #endregion
    }
}
