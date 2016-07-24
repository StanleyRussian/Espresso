using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class CoffeePurchaseViewModel: Abstract.aEntityWindowViewModel
    {
        public CoffeePurchaseViewModel(object argPurchase)
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
            Purchase = new CoffeePurchase
            {
                Date = DateTime.Now,
                PaymentDate = DateTime.Now,
                Paid = true,
                Account = ContextManager.ActiveAccounts.FirstOrDefault(),
                Supplier = ContextManager.ActiveSuppliers.FirstOrDefault(),
                Sum = 0
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
            foreach (var detail in Details)
                _purchase.Sum += (detail.Price*detail.Quantity);
            _purchase.CoffeePurchase_Details.Clear();
            foreach (var x in Details)
                _purchase.CoffeePurchase_Details.Add(x);
            if (CreatingNew)
                ContextManager.Context.CoffeePurchases.Add(_purchase);
            SaveContext();
        }

        #endregion
    }
}
