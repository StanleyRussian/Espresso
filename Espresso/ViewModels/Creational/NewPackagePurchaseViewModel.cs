using System;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewPackagePurchaseViewModel : Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewPackagePurchase = new Entity.PackagePurchase
            {
                Date = DateTime.Now,
                Account = ContextManager.ActiveAccounts.FirstOrDefault(),
                Supplier = ContextManager.ActiveSuppliers.FirstOrDefault()
            };
        }

        private Entity.PackagePurchase _newPackagePurchase;
        public Entity.PackagePurchase NewPackagePurchase
        {
            get { return _newPackagePurchase; }
            set
            {
                _newPackagePurchase = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSave_Execute()
        {
            _context.PackagePurchases.Add(NewPackagePurchase);
            SaveContext();
        }
    }
}
