using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinSale: Abstract.aEntityWindowViewModel
    {
        public vmWinSale(object argSale = null)
        {
            if (argSale != null)
            {
                Sale = argSale as Sale;
                CoffeeDetails = new ObservableCollection<SaleDetailCoffee>(Sale.SaleDetailsCoffee);
                ProductDetails = new ObservableCollection<SaleDetailProduct>(Sale.SaleDetailsProducts);
            }
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
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

        #region Binding Properties

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

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            try
            {
                // General sale checks
                if (Sale.InvoiceNumber == "0" || Sale.InvoiceNumber == "")
                    throw new Exception();
                // Coffee related checks
                //if (CoffeeDetails.Count == 0)
                //    throw new Exception("Введите хотя бы один купаж кофе");
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
                if (ProductDetails.Any(detail=>detail.Product == null))
                    throw new Exception("Выберите подходящий товар из списка");
                // Database involved coffee checks
                if (CoffeeDetails.Any(detail => ContextManager.Context.dPackedStocks.First(
                    p => p.Package.Id == detail.Package.Id
                         && p.Mix.Id == detail.Mix.Id)
                    .Quantity < detail.Quantity))
                {
                    FlyErrorMsg = "Недостаточно расфасованного кофе в наличии";
                    IsFlyErrorOpened = true;
                    return;
                }

                _sale.dSum = 0;
                _sale.dCost = 0;
                _sale.SaleDetailsCoffee.Clear();

                foreach (var detail in CoffeeDetails)
                {
                    _sale.SaleDetailsCoffee.Add(detail);
                    _sale.dSum += detail.Price*detail.Quantity;
                    _sale.dCost += (double) ContextManager.Context.dPackedStocks.First(
                        p=>p.Mix.Id == detail.Mix.Id && p.Package.Id == detail.Package.Id)
                        .dCost * detail.Quantity;
                }

                _sale.SaleDetailsProducts.Clear();

                foreach (var detail in ProductDetails)
                {
                    _sale.SaleDetailsProducts.Add(detail);
                    _sale.dSum += detail.Price*detail.Quantity;
                    _sale.dCost += (double) ContextManager.Context.dProductStocks.First(
                        p=>p.Product.Id == detail.Product.Id)
                        .dCost * detail.Quantity;
                }

                if (CreatingNew)
                    ContextManager.Context.Sales.Add(_sale);

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

        #endregion
    }
}
