using System.Collections.ObjectModel;
using System.Windows.Input;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;
using System.Data.Entity;
using System.Linq;

namespace ViewModels.Pages
{
    public class vmHome : aTabViewModel
    {
        private ObservableCollection<dCoffeeStock> _coffeeStocks;
        private ObservableCollection<dPackedStocks> _packedStocks;
        private ObservableCollection<dPackageStocks> _packageStocks;
        private ObservableCollection<dAccountsBalance> _accountsBalances;
        private ObservableCollection<dProductStock> _productStocks;

        public vmHome()
        {
            cmdReload = new Command(cmdReload_Execute);
            Header = "Главная";
            IsSelected = true;
        }

        protected override void Load()
        {
            CoffeeStocks = new ObservableCollection<dCoffeeStock>(
                ContextManager.Context.dCoffeeStocks.Where(
                    p => p.GreenQuantity > 0 ||
                         p.RoastedQuantity > 0).Include(p => p.CoffeeSort));

            PackedStocks =
                new ObservableCollection<dPackedStocks>(
                    ContextManager.Context.EagerPackedStocks().Where(p => p.Quantity > 0));

            PackageStocks =
                new ObservableCollection<dPackageStocks>(
                    ContextManager.Context.dPackageStocks.Where(p => p.Quantity > 0).Include(p => p.Package));

            AccountsBalances =
                new ObservableCollection<dAccountsBalance>(
                    ContextManager.Context.dAccountsBalances.Where(p => p.Account.Active).Include(p => p.Account));

            ProductStocks =
                new ObservableCollection<dProductStock>(
                    ContextManager.Context.dProductStocks.Where(p => p.Quantity > 0).Include(p => p.Product));
        }

        #region Binding Properties

        public ObservableCollection<dCoffeeStock> CoffeeStocks
        {
            get { return _coffeeStocks; }
            private set
            {
                _coffeeStocks = value;
                OnPropertyChanged(); 
            }
        }

        public ObservableCollection<dPackedStocks> PackedStocks
        {
            get { return _packedStocks; }
            private set
            {
                _packedStocks = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<dPackageStocks> PackageStocks
        {
            get { return _packageStocks; }
            private set
            {
                _packageStocks = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<dAccountsBalance> AccountsBalances
        {
            get { return _accountsBalances; }
            private set
            {
                _accountsBalances = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<dProductStock> ProductStocks
        {
            get { return _productStocks; }
            private set
            {
                _productStocks = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand cmdReload
        { get; private set; }

        private void cmdReload_Execute()
        {
            //ContextManager.ReloadContext();
            Load();
        }
        #endregion
    }
}
