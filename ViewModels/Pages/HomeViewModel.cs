using System.Collections.ObjectModel;
using System.Windows.Input;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;
using ViewModels.Statistics.Abstract;
using System.Data.Entity;
using System.Linq;

namespace ViewModels.Pages
{
    public class HomeViewModel : aTabViewModel
    {
        //private ContextContainer _context;
        private ObservableCollection<dGreenStock> _greenStocks;
        private ObservableCollection<dRoastedStock> _roastedStocks;
        private ObservableCollection<dPackedStocks> _packedStocks;
        private ObservableCollection<dPackageStocks> _packageStocks;
        private ObservableCollection<dAccountsBalance> _accountsBalances;

        public HomeViewModel()
        {
            cmdReload = new Command(cmdReload_Execute);
            Header = "Главная";
            IsSelected = true;
        }

        protected override void Load()
        {
            GreenStocks = 
                new ObservableCollection<dGreenStock>(ContextManager.Context.dGreenStocks.Include(p => p.CoffeeSort));
            RoastedStocks = 
                new ObservableCollection<dRoastedStock>(ContextManager.Context.dRoastedStocks.Include(p=>p.CoffeeSort));
            PackedStocks = 
                new ObservableCollection<dPackedStocks>(ContextManager.Context.EagerPackedStocks());
            PackageStocks = 
                new ObservableCollection<dPackageStocks>(ContextManager.Context.dPackageStocks.Include(p => p.Package));
            AccountsBalances =
                new ObservableCollection<dAccountsBalance>(
                    ContextManager.Context.dAccountsBalances
                        .Where(p => p.Account.Active)
                        .Include(p => p.Account));
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

        public ObservableCollection<dRoastedStock> RoastedStocks
        {
            get { return _roastedStocks; }
            private set
            {
                _roastedStocks = value; 
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
