using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinSale: Abstract.aEntityWindowViewModel
    {
        public vmWinSale(object argEntity) : base(argEntity) { }

        private Sale _sale;
        public Sale Sale
        {
            get { return _sale; }
            set
            {
                _sale = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SaleDetailCoffee> _details;
        public ObservableCollection<SaleDetailCoffee> CoffeeDetails
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SaleDetailProduct> _productDetails;
        public ObservableCollection<SaleDetailProduct> ProductDetails
        {
            get { return _productDetails; }
            set
            {
                _productDetails = value; 
                OnPropertyChanged();
            }
        }

        private double _sum;

        protected override void OnOpenEdit(object argEntity)
        {
            Sale = argEntity as Sale;
            CoffeeDetails = new ObservableCollection<SaleDetailCoffee>(Sale.SaleDetailsCoffee);
            ProductDetails = new ObservableCollection<SaleDetailProduct>(Sale.SaleDetailsProducts);
        }

        protected override void OnOpenNew()
        {
            int iInvoiceNumber;
            var firstOrDefault = ContextManager.Context.Sales.OrderByDescending(p => p.InvoiceNumber).FirstOrDefault();
            if (firstOrDefault != null)
            {
                iInvoiceNumber = int.Parse(firstOrDefault.InvoiceNumber);
                iInvoiceNumber++;
            }
            else
                iInvoiceNumber = 1;

            Sale = new Sale
            {
                Date = DateTime.Now,
                PaymentDate = DateTime.Now,
                Paid = true,
                Account = ContextManager.ActiveAccounts.FirstOrDefault(),
                Recipient = ContextManager.ActiveRecipients.FirstOrDefault(),
                InvoiceNumber = iInvoiceNumber.ToString()
            };

            CoffeeDetails = new ObservableCollection<SaleDetailCoffee>();
            ProductDetails = new ObservableCollection<SaleDetailProduct>();
        }

        protected override void OnSaveValidation()
        {
            // General sale checks
            if (Sale.InvoiceNumber == "0" || Sale.InvoiceNumber == "")
                throw new Exception();
            // Coffee related checks
            if (CoffeeDetails.Any(detail => detail.Quantity == 0))
                throw new Exception("Введите количество проданных пачек кофе");
            if (CoffeeDetails.Any(detail => detail.Package == null))
                throw new Exception("Выберите подходящую упаковку из списка");
            if (CoffeeDetails.Any(detail => detail.Mix == null))
            {
                FlyErrorMsg = "Выберите подходящий купаж из списка";
                IsFlyErrorOpened = true;
                return;
            }
            // Products related checks
            if (ProductDetails.Any(detail => detail.Quantity == 0))
                throw new Exception("Введите количество проданных товаров");
            if (ProductDetails.Any(detail => detail.Product == null))
                throw new Exception("Выберите подходящий товар из списка");
            // Database involved coffee checks
            if (CoffeeDetails.Any(detail => ContextManager.Context.dPackedStocks.First(
                p => p.Package.Id == detail.Package.Id
                     && p.Mix.Id == detail.Mix.Id).Quantity < detail.Quantity))
                throw new Exception("Недостаточно расфасованного кофе в наличии");
        }

        protected override void OnSaveEdit()
        {
            throw new NotImplementedException();
        }

        protected override void OnSaveNew()
        {
            //Just for common sense
            if (!_sale.Paid)
                _sale.PaymentDate = null;

            _sale.SaleDetailsCoffee.Clear();
            foreach (var detail in CoffeeDetails)
            {
                _sale.SaleDetailsCoffee.Add(detail);
                // Calculate sale sum
                _sum += detail.Price*detail.Quantity;
                // Calculate sale cost
                detail.Cost = ContextManager.Context.dPackedStocks.First(
                    p => p.Mix.Id == detail.Mix.Id
                         && p.Package.Id == detail.Package.Id).Cost;
                // Change packed stocks
                ContextManager.Context.dPackedStocks.First(
                    p => p.Mix.Id == detail.Mix.Id
                         && p.Package.Id == detail.Package.Id).Quantity -= detail.Quantity;
            }

            _sale.SaleDetailsProducts.Clear();
            foreach (var detail in ProductDetails)
            {
                _sale.SaleDetailsProducts.Add(detail);
                // Calculate sale sum
                _sum += detail.Price*detail.Quantity;
                // Calculate sale cost
                detail.Cost = ContextManager.Context.dProductStocks.First(
                    p => p.Product.Id == detail.Product.Id).Cost;
                // Change product stocks
                ContextManager.Context.dProductStocks.First(
                    p => p.Product.Id == detail.Product.Id).Quantity -= detail.Quantity;
            }
            // Add sale to database
            ContextManager.Context.Sales.Add(_sale);

            if (Sale.Paid)
            {
                // Correct account balance
                ContextManager.Context.dAccountsBalances.First(
                    p => p.Account.Id == Sale.Account.Id).Balance += _sum;
                // Add new transaction
                ContextManager.Context.dTransactions.Add(new dTransaction
                {
                    Account = Sale.Account,
                    Date = Sale.Date,
                    Description = "Продажа",
                    Participant = Sale.Recipient.Name,
                    Sum = _sum
                });
            }
        }
    }
}
