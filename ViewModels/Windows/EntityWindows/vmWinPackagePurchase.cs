using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinPackagePurchase : Abstract.aEntityWindowViewModel
    {
        public vmWinPackagePurchase(object argPurchase = null)
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
                Supplier = ContextManager.ActiveSuppliers.FirstOrDefault(),
                Package = ContextManager.ActivePackages.FirstOrDefault()
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
            if (Purchase.PackQuantity <= 0)
            {
                FlyErrorMsg = "Введите количество пачек";
                IsFlyErrorOpened = true;
                return;
            }

            if (Purchase.Price <= 0)
            {
                FlyErrorMsg = "Введите цену";
                IsFlyErrorOpened = true;
                return;
            }

            if (Purchase.PackQuantity <= 0)
            {
                FlyErrorMsg = "Введите цену";
                IsFlyErrorOpened = true;
                return;
            }

            Purchase.Sum = Purchase.PackQuantity * Purchase.Price;

            if (ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Purchase.Account.Id).Balance < Purchase.Sum)
            {
                FlyErrorMsg = "На выбранном счету недостаточно денег";
                IsFlyErrorOpened = true;
                return;
            }

            if (CreatingNew)
                ContextManager.Context.PackagePurchases.Add(Purchase);
            SaveContext();
        }
    }
}
