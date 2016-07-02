using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewCoffeePurchaseViewModel: Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewPurchase = new Entity.CoffeePurchase
            {
                Date = DateTime.Now,
                PaymentDate = DateTime.Now,
                Paid = true,
                Account = ContextManager.ActiveAccounts.FirstOrDefault(),
                Supplier = ContextManager.ActiveSuppliers.FirstOrDefault(),
                Sum = 0
            };

            Details = new ObservableCollection<Entity.CoffeePurchase_Details>();

        }

        #region Binding Properties

        private Entity.CoffeePurchase _newPurchase;
        public Entity.CoffeePurchase NewPurchase
        {
            get { return _newPurchase; }
            set
            {
                _newPurchase = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Entity.CoffeePurchase_Details> _details;
        public ObservableCollection<Entity.CoffeePurchase_Details> Details
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
            foreach (var detail in Details)
                _newPurchase.Sum += (detail.Price*detail.Quantity);
            _newPurchase.CoffeePurchase_Details.Clear();
            foreach (var x in Details)
                _newPurchase.CoffeePurchase_Details.Add(x);
            _context.CoffeePurchases.Add(_newPurchase);

            SaveContext();
        }

        #endregion
    }
}
