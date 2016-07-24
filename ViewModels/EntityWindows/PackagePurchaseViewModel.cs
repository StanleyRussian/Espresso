using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class PackagePurchaseViewModel : Abstract.aEntityWindowViewModel
    {
        public PackagePurchaseViewModel(object argPurchase = null)
        {
            if (argPurchase != null)
                Purchase = argPurchase as PackagePurchase;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Purchase = new PackagePurchase
            {
                Date = DateTime.Now,
                Account = ContextManager.ActiveAccounts.FirstOrDefault(),
                Supplier = ContextManager.ActiveSuppliers.FirstOrDefault()
            };
        }

        private PackagePurchase _purchase;
        public PackagePurchase Purchase
        {
            get { return _purchase; }
            set
            {
                _purchase = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                ContextManager.Context.PackagePurchases.Add(Purchase);
            SaveContext();
        }
    }
}
