﻿using System;
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
            try
            {
                if (Purchase.Quantity <= 0)
                    throw new Exception("Введите количество");

                if (Purchase.Price <= 0)
                    throw new Exception("Введите цену");

                Purchase.dSum = Purchase.Quantity*Purchase.Price;

                if (ContextManager.Context.dAccountsBalances.First(
                    p => p.Account.Id == Purchase.Account.Id).Balance < Purchase.dSum)
                    throw new Exception("На выбранном счету недостаточно денег");

                if (CreatingNew)
                    ContextManager.Context.PackagePurchases.Add(Purchase);
                ContextManager.Context.SaveChanges();

                //// Find stocks of package for current package being purchased
                //var packageStocks = ContextManager.Context.dPackageStocks.First(
                //    p => p.Package.Id == Purchase.Package.Id);
                //// Check if there anything in stock already
                //if (packageStocks.Quantity == 0)
                //    // Set cost from purchase
                //    packageStocks.dCost = Purchase.Price;
                //else
                //    // Count cost based on stock and new purchase
                //    packageStocks.dCost = (packageStocks.Quantity * packageStocks.dCost + Purchase.Quantity * Purchase.Price) 
                //        / (packageStocks.Quantity + Purchase.Quantity);

                SaveContext();
                if (CreatingNew)
                    Refresh();
            }
            catch (Exception ex)
            {
                FlyErrorMsg = ex.Message;
                IsFlyErrorOpened = true;
            }
        }
    }
}
