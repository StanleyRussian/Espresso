using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;

namespace ViewModels.Pages
{
    public class vmUnpaid : aTabViewModel
    {
        private ObservableCollection<wrapCoffeePurchase> _purchases;
        private ObservableCollection<wrapSale> _sales;

        public vmUnpaid()
        {
            Header = "Неоплаченные";
            cmdPay = new Command(cmdPay_Execute);
        }

        protected override void Load()
        {
            Purchases = new ObservableCollection<wrapCoffeePurchase>();
            foreach (var purchase in ContextManager.Context.CoffeePurchases.Where(p => !p.Paid))
                Purchases.Add(new wrapCoffeePurchase(purchase));

            Sales = new ObservableCollection<wrapSale>();
            foreach (var sale in ContextManager.Context.Sales.Where(p => !p.Paid))
                Sales.Add(new wrapSale(sale));
        }

        public ObservableCollection<wrapCoffeePurchase> Purchases
        {
            get { return _purchases; }
            private set
            {
                _purchases = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<wrapSale> Sales
        {
            get { return _sales; }
            private set
            {
                _sales = value;
                OnPropertyChanged();
            }
        }

        public ICommand cmdPay { get; private set; }

        private void cmdPay_Execute(object argPurchase)
        {
            wrapCoffeePurchase wrapper = argPurchase as wrapCoffeePurchase;
            CoffeePurchase purchase = wrapper.Purchase;

            if (purchase.PaymentDate == null)
                return;

            purchase.Paid = true;
            // Change account balance
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == purchase.Account.Id).Balance -= wrapper.Sum;
            // Add new transaction
            ContextManager.Context.dTransactions.Add(new dTransaction
            {
                Account = purchase.Account,
                Date = purchase.Date,
                Description = "Закупка кофе",
                Participant = purchase.Supplier.Name,
                Sum = wrapper.Sum
            });

            // Correct cost of green coffee stocks
            foreach (var detail in purchase.CoffeePurchaseDetails)
            {
                // Find stocks of green coffee for current coffee sort
                var coffeeStock = ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id);
                // Check if there anything in stock already
                if (coffeeStock.GreenQuantity == 0)
                    // Set cost from purchase
                    coffeeStock.GreenCost = detail.Price;
                else
                // Count cost based on stock and new purchase
                    coffeeStock.GreenCost = Math.Round((coffeeStock.GreenQuantity*coffeeStock.GreenCost
                                                        + detail.Quantity*detail.Price)/
                                                       (coffeeStock.GreenQuantity + detail.Quantity), 2);

                ContextManager.Context.SaveChanges();
            }
        }



        public class wrapCoffeePurchase
        {
            public wrapCoffeePurchase(CoffeePurchase argPurchase)
            {
                Purchase = argPurchase;
                Sum = argPurchase.CoffeePurchaseDetails.Sum(detail => (detail.Price*detail.Quantity));
            }

            public CoffeePurchase Purchase { get; }
            public double Sum { get; }
        }



        public class wrapSale
        {
            public wrapSale(Sale argSale)
            {
                Sale = argSale;
                Sum = argSale.SaleDetailsProducts.Sum(p => (p.Price*p.Quantity)) +
                      argSale.SaleDetailsCoffee.Sum(p => p.Price*p.Quantity);
            }

            public Sale Sale { get; }
            public double Sum { get; }
        }
    }
}
