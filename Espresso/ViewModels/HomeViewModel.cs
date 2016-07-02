using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Core.Annotations;
using Core.Auxiliary;

namespace Core.ViewModels
{
    public class HomeViewModel:INotifyPropertyChanged
    {
        private Entity.ContextContainer _context = ContextManager.Context;

        public HomeViewModel()
        {
            cmdReload_Execute();
            cmdReload = new Command(cmdReload_Execute);
        }

        #region Binding Properties

        public ObservableCollection<Entity.dGreenStock> GreenStocks => _context.dGreenStocks.Local;
        public ObservableCollection<Entity.dRoastedStock> RoastedStocks => _context.dRoastedStocks.Local;
        public ObservableCollection<Entity.dPackedStocks> PackedStocks => _context.dPackedStocks.Local;
        public ObservableCollection<Entity.dPackageStocks> PackageStocks => _context.dPackageStocks.Local;
        public ObservableCollection<Entity.dAccountsBalance> AccountsBalances => _context.dAccountsBalances.Local;

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

            _context.dGreenStocks.Load();
            _context.dRoastedStocks.Load();
            _context.dPackedStocks.Load();
            _context.dPackageStocks.Load();
            _context.dAccountsBalances.Load();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
