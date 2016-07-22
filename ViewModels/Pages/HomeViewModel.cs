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
        private ContextContainer _context;
        private ObservableCollection<dGreenStock> _greenStocks;
        private ObservableCollection<dRoastedStock> _roastedStocks;
        private ObservableCollection<dPackedStocks> _packedStocks;
        private ObservableCollection<dPackageStocks> _packageStocks;
        private ObservableCollection<dAccountsBalance> _accountsBalances;

        public HomeViewModel()
        {
            cmdReload = new Command(Load);
            Header = "Главная";
            IsSelected = true;
        }

        protected override void Load()
        {
            _context = ContextManager.Context;
            GreenStocks = 
                new ObservableCollection<dGreenStock>(_context.dGreenStocks.Include(p => p.CoffeeSort));
            RoastedStocks = 
                new ObservableCollection<dRoastedStock>(_context.dRoastedStocks.Include(p=>p.CoffeeSort));
            PackedStocks = 
                new ObservableCollection<dPackedStocks>(_context.EagerPackedStocks());
            PackageStocks = 
                new ObservableCollection<dPackageStocks>(_context.dPackageStocks.Include(p => p.Package));
            AccountsBalances =
                new ObservableCollection<dAccountsBalance>(
                    _context.dAccountsBalances
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

        #endregion
    }
}
