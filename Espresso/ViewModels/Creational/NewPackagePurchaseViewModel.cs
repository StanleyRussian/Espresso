using System;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewPackagePurchaseViewModel : Abstract.aCreationalViewModel
    {
        public NewPackagePurchaseViewModel() : base() { }

        protected override void Refresh()
        {
            NewPackagePurchase = new Entity.PackagePurchase();
            NewPackagePurchase.Date = DateTime.Now;
            NewPackagePurchase.Account = ContextManager.ActiveAccounts.FirstOrDefault();
            NewPackagePurchase.Supplier = ContextManager.ActiveSuppliers.FirstOrDefault();
        }

        private Entity.PackagePurchase _newPackagePurchase;
        public Entity.PackagePurchase NewPackagePurchase
        {
            get { return _newPackagePurchase; }
            set
            {
                _newPackagePurchase = value;
                OnPropertyChanged("NewPackagePurchase");
            }
        }

        protected override void cmdSave_Execute()
        {
            _context.PackagePurchases.Add(NewPackagePurchase);
            SaveContext();
        }
    }
}
