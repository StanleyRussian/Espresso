using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override void cmdSave_Execute()
        {
            try
            {
                if (Purchase.Quantity <= 0)
                    throw new Exception("Введите количество");

                if (Purchase.Price <= 0)
                    throw new Exception("Введите цену");

                Purchase.dSum = Purchase.Quantity * Purchase.Price;

                if (ContextManager.Context.dAccountsBalances.First(
                    p => p.Account.Id == Purchase.Account.Id).Balance < Purchase.dSum)
                    throw new Exception("На выбранном счету недостаточно денег");

                if (CreatingNew)
                    ContextManager.Context.ProductPurchases.Add(Purchase);
                ContextManager.Context.SaveChanges();

                // Find stocks of package for current product being purchase
                var productStock = ContextManager.Context.dProductStocks.First(
                    p => p.Product.Id == Purchase.Product.Id);
                // Check if there anything in stock already
                if (productStock.Quantity == 0)
                    // Set cost from purchase
                    productStock.dCost = Purchase.Price;
                else
                    // Count cost based on stock and new purchase
                    productStock.dCost = (productStock.Quantity * productStock.dCost + Purchase.Quantity * Purchase.Price)
                        / (productStock.Quantity + Purchase.Quantity);


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
