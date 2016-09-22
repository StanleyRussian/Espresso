using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmProductPurchases: aProcessListingViewModel
    {
        public vmProductPurchases()
        {
            Header = "Закупка товаров";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.ProductPurchases.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterSupplier != null)
                query = query.Where(p => p.Supplier.Id == FilterSupplier.Id);
            if (FilterAccount != null)
                query = query.Where(p => p.Account.Id == FilterAccount.Id);
            if (FilterProduct != null)
                query = query.Where(p => p.Product.Id == FilterProduct.Id);
            Tabs = new ObservableCollection<ProductPurchase>(query);
        }

        private ObservableCollection<ProductPurchase> _tabs;
        public ObservableCollection<ProductPurchase> Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        private Package _filterProduct;
        public Package FilterProduct
        {
            get { return _filterProduct; }
            set
            {
                _filterProduct = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private Supplier _filterSupplier;
        public Supplier FilterSupplier
        {
            get { return _filterSupplier; }
            set
            {
                _filterSupplier = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private Account _filterAccount;
        public Account FilterAccount
        {
            get { return _filterAccount; }
            set
            {
                _filterAccount = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        protected override void cmdDelete_Execute(object argSelected)
        { }
    }
}
