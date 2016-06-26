using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewCoffeePurchaseViewModel: Abstract.aCreationalViewModel
    {
        public NewCoffeePurchaseViewModel() : base() { }

        protected override void Refresh()
        {
            NewPurchase = new Entity.CoffeePurchase();
            Details = new ObservableCollection<Entity.CoffeePurchase_Details>();

            NewPurchase.Date = DateTime.Now;
            NewPurchase.PaymentDate = DateTime.Now;
            NewPurchase.Paid = true;
            NewPurchase.Account = ContextManager.ActiveAccounts.FirstOrDefault();
            NewPurchase.Supplier = ContextManager.ActiveSuppliers.FirstOrDefault();
        }

        #region Binding Properties

        private Entity.CoffeePurchase _newPurchase;
        public Entity.CoffeePurchase NewPurchase
        {
            get { return _newPurchase; }
            set
            {
                _newPurchase = value;
                OnPropertyChanged("NewPurchase");
            }
        }

        private ObservableCollection<Entity.CoffeePurchase_Details> _details;
        public ObservableCollection<Entity.CoffeePurchase_Details> Details
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
            _newPurchase.CoffeePurchase_Details.Clear();
            foreach (var x in Details)
                _newPurchase.CoffeePurchase_Details.Add(x);
            _context.CoffeePurchases.Add(_newPurchase);

            SaveContext();
        }

        #endregion
    }
}
