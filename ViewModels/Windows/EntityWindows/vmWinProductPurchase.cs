using System;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Windows.EntityWindows.Abstract;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinProductPurchase: aEntityWindowViewModel
    {
        public vmWinProductPurchase(object argPurchase = null)
        {
            if (argPurchase != null)
                Purchase = argPurchase as ProductPurchase;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Purchase = new ProductPurchase
            {
                Date = DateTime.Now,
                Account = ContextManager.ActiveAccounts.FirstOrDefault(),
                Supplier = ContextManager.ActiveSuppliers.FirstOrDefault()
            };
        }

        private ProductPurchase _purchase;
        public ProductPurchase Purchase
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
            // Add product purchase to database
            ContextManager.Context.ProductPurchases.Add(Purchase);

            // Correct balance on account
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Purchase.Account.Id).Balance -= _sum;
            // Add new transaction
            ContextManager.Context.dTransactions.Add(new dTransaction
            {
                Account = Purchase.Account,
                Date = Purchase.Date,
                Description = "Закупка: " + Purchase.Product.Name,
                Participant = Purchase.Supplier.Name,
                Sum = _sum
            });


            // Find stocks of package for current product being purchase
            var productStock = ContextManager.Context.dProductStocks.First(
                p => p.Product.Id == Purchase.Product.Id);
            // Check if there anything in stock already
            if (productStock.Quantity == 0)
                // Set cost from purchase
                productStock.Cost = Purchase.Price;
            else
            // Count cost based on stock and new purchase
                productStock.Cost =
                    Math.Round((productStock.Quantity*productStock.Cost + Purchase.Quantity*Purchase.Price)
                               /(productStock.Quantity + Purchase.Quantity), 2);

            // Change product stock
            productStock.Quantity += Purchase.Quantity;
        }
    }
}
