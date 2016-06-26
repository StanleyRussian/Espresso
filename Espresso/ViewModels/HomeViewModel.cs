using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Core.ViewModels
{
    public class HomeViewModel: INotifyPropertyChanged
    {
        private Entity.ContextContainer _context = ContextManager.Context;

        public HomeViewModel()
        {
            cmdReload_Execute();
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Entity.dGreenStock> GreenStocks
        {
            get { return _context.dGreenStocks.Local; }
        }

        public ObservableCollection<Entity.dRoastedStock> RoastedStocks
        {
            get { return _context.dRoastedStocks.Local; }
        }

        public ObservableCollection<Entity.dPackedStocks> PackedStocks
        {
            get { return _context.dPackedStocks.Local; }
        }

        public ObservableCollection<Entity.dPackageStocks> PackageStocks
        {
            get { return _context.dPackageStocks.Local; }
        }

        public ObservableCollection<Entity.dAccountsBalance> AccountsBalances
        {
            get { return _context.dAccountsBalances.Local; }
        }
        #endregion

        #region Commands

        public ICommand cmdReload
        { get; private set; }

        private void cmdReload_Execute()
        {
            //_context.Accounts.Load();
            //_context.Mixes.Load();
            //_context.Packages.Load();
            //_context.CoffeeSorts.Load();

            //_context.dGreenStocks.Load();
            //_context.dRoastedStocks.Load();
            //_context.dPackedStocks.Load();
            //_context.dPackageStocks.Load();
            //_context.dAccountsBalances.Load();
        }
        #endregion
    }
}
