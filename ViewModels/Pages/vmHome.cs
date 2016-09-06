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
        private ObservableCollection<dGreenStock> _greenStocks;
        private ObservableCollection<dRoastedStock> _roastedStocks;
        private ObservableCollection<dPackedStocks> _packedStocks;
        private ObservableCollection<dPackageStocks> _packageStocks;
        private ObservableCollection<dAccountsBalance> _accountsBalances;
        private double _greenStocksCostsSum;
        private double _roastedStocksCostsSum;
        private double _packedStocksCostsSum;
        private double _packageStocksCostsSum;
        private double _overallSum;

        public vmHome()
        {
            cmdReload = new Command(cmdReload_Execute);
            Header = "Главная";
            IsSelected = true;
        }

        protected override void Load()
        {
            ContextManager.ReloadContext();
            GreenStocksCostsSum = 0;
            RoastedStocksCostsSum = 0;
            PackedStocksCostsSum = 0;
            PackageStocksCostsSum = 0;

                GreenStocks =
                new ObservableCollection<dGreenStock>(
                    ContextManager.Context.dGreenStocks.Where(p => p.Quantity > 0).Include(p => p.CoffeeSort));
            foreach (var stock in GreenStocks)
                GreenStocksCostsSum += (double) stock.dCost*stock.Quantity;

            RoastedStocks =
                new ObservableCollection<dRoastedStock>(
                    ContextManager.Context.dRoastedStocks.Where(p => p.Quantity > 0).Include(p => p.CoffeeSort));
            foreach (var stock in RoastedStocks)
                RoastedStocksCostsSum += (double) stock.dCost*stock.Quantity;

            PackedStocks =
                new ObservableCollection<dPackedStocks>(
                    ContextManager.Context.EagerPackedStocks().Where(p => p.Quantity > 0));
            foreach (var stock in PackedStocks)
                PackedStocksCostsSum += (double) stock.dCost*stock.Quantity;

            PackageStocks =
                new ObservableCollection<dPackageStocks>(
                    ContextManager.Context.dPackageStocks.Where(p => p.Quantity > 0).Include(p => p.Package));
            foreach (var stock in PackageStocks)
                PackageStocksCostsSum += (double) stock.dCost*stock.Quantity;

            AccountsBalances =
                new ObservableCollection<dAccountsBalance>(
                    ContextManager.Context.dAccountsBalances.Where(p => p.Account.Active).Include(p => p.Account));

            OverallSum = GreenStocksCostsSum + RoastedStocksCostsSum + PackageStocksCostsSum + PackedStocksCostsSum;
        }

        #region Binding Properties

        public ObservableCollection<dGreenStock> GreenStocks
        {
            get { return _greenStocks; }
            private set
            {
                _greenStocks = value;
                OnPropertyChanged(); 
            }
        }

        public double GreenStocksCostsSum
        {
            get { return _greenStocksCostsSum; }
            private set
            {
                _greenStocksCostsSum = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<dRoastedStock> RoastedStocks
        {
            get { return _roastedStocks; }
            private set
            {
                _roastedStocks = value; 
                OnPropertyChanged();
            }
        }

        public double RoastedStocksCostsSum
        {
            get { return _roastedStocksCostsSum; }
            private set
            {
                _roastedStocksCostsSum = value;
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

        public double PackedStocksCostsSum
        {
            get { return _packedStocksCostsSum; }
            private set
            {
                _packedStocksCostsSum = value; 
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

        public double PackageStocksCostsSum
        {
            get { return _packageStocksCostsSum; }
            private set
            {
                _packageStocksCostsSum = value; 
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

        public double OverallSum
        {
            get { return _overallSum; }
            private set
            {
                _overallSum = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand cmdReload
        { get; private set; }

        private void cmdReload_Execute()
        {
            ContextManager.ReloadContext();
            Load();
        }
        #endregion
    }
}
