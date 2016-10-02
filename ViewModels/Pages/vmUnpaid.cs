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
                Purchases.Add(new wrapCoffeePurchase {Purchase = purchase});

            Sales = new ObservableCollection<wrapSale>();
            foreach (var sale in ContextManager.Context.Sales.Where(p => !p.Paid))
                Sales.Add(new wrapSale {Sale = sale});
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
        private void cmdPay_Execute(object arg)
        {
            wrapCoffeePurchase wrapperPurchase = arg as wrapCoffeePurchase;
            if (wrapperPurchase != null)
            {
                CoffeePurchase purchase = wrapperPurchase.Purchase;

                if (purchase.PaymentDate == null)
                    return;

                purchase.Paid = true;
                // Change account balance
                ContextManager.Context.dAccountsBalances.First(
                    p => p.Account.Id == purchase.Account.Id).Balance -= wrapperPurchase.Sum;
                // Add new transaction
                ContextManager.Context.dTransactions.Add(new dTransaction
                {
                    Account = purchase.Account,
                    Date = (DateTime) purchase.PaymentDate,
                    Description = "Закупка кофе",
                    Participant = purchase.Supplier.Name,
                    Sum = - wrapperPurchase.Sum
                });
            }
            wrapSale wrapperSale = arg as wrapSale;
            if (wrapperSale != null)
            {
                Sale sale = wrapperSale.Sale;

                if (sale.PaymentDate == null)
                    return;

                sale.Paid = true;
                // Change account balance
                ContextManager.Context.dAccountsBalances.First(
                    p => p.Account.Id == sale.Account.Id).Balance += wrapperSale.Sum;
                // Add new transaction
                ContextManager.Context.dTransactions.Add(new dTransaction
                {
                    Account = sale.Account,
                    Date = (DateTime) sale.PaymentDate,
                    Description = "Продажа кофе",
                    Participant = sale.Recipient.Name,
                    Sum = wrapperSale.Sum
                });
            }

            ContextManager.Context.SaveChanges();
        }



        public class wrapCoffeePurchase
        {
            public CoffeePurchase Purchase { get; set; }
            public double Sum
            {
                get
                {
                    return Purchase.CoffeePurchaseDetails.Sum(detail => (detail.Price * detail.Quantity));
                }
            }
        }



        public class wrapSale
        {
            public Sale Sale { get; set; }
            public double Sum
            {
                get
                {
                    return Sale.SaleDetailsProducts.Sum(p => (p.Price*p.Quantity)) +
                           Sale.SaleDetailsCoffee.Sum(p => p.Price*p.Quantity);
                }
            }
        }
    }
}
