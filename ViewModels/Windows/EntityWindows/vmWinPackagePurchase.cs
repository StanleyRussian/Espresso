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

        private double _sum;

        protected override void OnSaveValidation()
        {
            if (Purchase.Quantity <= 0)
                throw new Exception("Введите количество");
            if (Purchase.Price <= 0)
                throw new Exception("Введите цену");

            _sum = Purchase.Quantity * Purchase.Price;
            if (ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Purchase.Account.Id).Balance < _sum)
                throw new Exception("На выбранном счету недостаточно денег");

        }

        protected override void OnSaveCreate()
        {
            // Add purchase to database
            ContextManager.Context.PackagePurchases.Add(Purchase);
            // Correct balance on account
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Purchase.Account.Id).Balance -= _sum;
            // Add new transaction
            ContextManager.Context.dTransactions.Add(new dTransaction
            {
                Account = Purchase.Account,
                Date = Purchase.Date,
                Description = "Закупка упаковки",
                Participant = Purchase.Supplier.Name,
                Sum = -_sum
            });

            // Correct cost of package stocks
            // Find stocks of package for current package being purchased
            var packageStocks = ContextManager.Context.dPackageStocks.First(
                p => p.Package.Id == Purchase.Package.Id);
            // Check if there anything in stock already
            if (packageStocks.Quantity == 0)
                // Set cost from purchase
                packageStocks.Cost = Purchase.Price;
            else
            // Count cost based on stock and new purchase
                packageStocks.Cost =
                    Math.Round((packageStocks.Quantity*packageStocks.Cost + Purchase.Quantity*Purchase.Price)
                               /(packageStocks.Quantity + Purchase.Quantity), 2);
            // Change package stocks
            packageStocks.Quantity += Purchase.Quantity;
        }
    }
}
