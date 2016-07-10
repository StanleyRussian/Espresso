using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Core.Annotations;
using Core.Auxiliary;
using Core.ViewModels.Statistics;
using Core.ViewModels.Statistics.Abstract;

namespace Core.ViewModels.Pages
{
    public class HomeViewModel : aTabViewModel
    {
        private Entity.ContextContainer _context = ContextManager.Context;

        public HomeViewModel()
        {
            cmdReload = new Command(cmdReload_Execute);
            Header = "Главная";
            IsSelected = true;
        }

        public string Header { get; set; }

        protected override void Load()
        {
            _context.Accounts.Load();
            _context.Mixes.Load();
            _context.Packages.Load();
            _context.CoffeeSorts.Load();

            _context.dGreenStocks.Load();
            _context.dRoastedStocks.Load();
            _context.dPackedStocks.Load();
            _context.dPackageStocks.Load();
            _context.dAccountsBalances.Load();
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
            //_context.dGreenStocks.Load();
            //_context.dRoastedStocks.Load();
            //_context.dPackedStocks.Load();
            //_context.dPackageStocks.Load();
            //_context.dAccountsBalances.Load();
        }
        #endregion
    }
}
