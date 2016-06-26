using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Core
{
    public static class ContextManager
    {
        private static Entity.ContextContainer _context;
        public static Entity.ContextContainer Context
        {
            get
            {
                if (_context == null)
                    _context = new Entity.ContextContainer();
                return _context;
            }
        }

        #region "Local" helper properties

        static ContextManager()
        {
            _context = new Entity.ContextContainer();
        }

        public static ObservableCollection<Entity.Account> LocalAccounts
        {
            get
            {
                if (_context.Accounts.Local.Count == 0)
                    _context.Accounts.Load();
                return _context.Accounts.Local;
            }
        }

        public static ObservableCollection<Entity.CoffeeSort> LocalCoffeeSorts
        {
            get
            {
                if (_context.CoffeeSorts.Local.Count == 0)
                    _context.CoffeeSorts.Load();
                return _context.CoffeeSorts.Local;
            }
        }

        public static ObservableCollection<Entity.Supplier> LocalSuppliers
        {
            get
            {
                if (_context.Suppliers.Local.Count == 0)
                    _context.Suppliers.Load();
                return _context.Suppliers.Local;
            }
        }

        public static ObservableCollection<Entity.Mix> LocalMixes
        {
            get
            {
                if (_context.Mixes.Local.Count == 0)
                    _context.Mixes.Load();
                return _context.Mixes.Local;
            }
        }

        public static ObservableCollection<Entity.Recipient> LocalRecipients
        {
            get
            {
                if (_context.Recipients.Local.Count == 0)
                    _context.Recipients.Load();
                return _context.Recipients.Local;
            }
        }

        public static ObservableCollection<Entity.Package> LocalPackages
        {
            get
            {
                if (_context.Packages.Local.Count == 0)
                    _context.Packages.Load();
                return _context.Packages.Local;
            }
        }

        public static ObservableCollection<Entity.CoffeePurchase> LocalCoffeePurchases
        {
            get
            {
                if (_context.CoffeePurchases.Local.Count == 0)
                    _context.CoffeePurchases.Load();
                return _context.CoffeePurchases.Local;
            }
        }

        public static ObservableCollection<Entity.dAccountsBalance> dLocalAccountsBalances
        {
            get
            {
                if (_context.dAccountsBalances.Local.Count == 0)
                    _context.dAccountsBalances.Load();
                return _context.dAccountsBalances.Local;
            }
        }

        public static ObservableCollection<Entity.dGreenStock> dLocalGreenStocks
        {
            get
            {
                if (_context.dGreenStocks.Local.Count == 0)
                    _context.dGreenStocks.Load();
                return _context.dGreenStocks.Local;
            }
        }

        public static ObservableCollection<Entity.dPackageStocks> dLocalPackageStocks
        {
            get
            {
                if (_context.dPackageStocks.Local.Count == 0)
                    _context.dPackageStocks.Load();
                return _context.dPackageStocks.Local;
            }
        }

        public static ObservableCollection<Entity.dPackedStocks> dLocalPackedStocks
        {
            get
            {
                if (_context.dPackedStocks.Local.Count == 0)
                    _context.dPackedStocks.Load();
                return _context.dPackedStocks.Local;
            }
        }

        public static ObservableCollection<Entity.dRoastedStock> dLocalRoastedStocks
        {
            get
            {
                if (_context.dRoastedStocks.Local.Count == 0)
                    _context.dRoastedStocks.Load();
                return _context.dRoastedStocks.Local;
            }
        }

        public static ObservableCollection<Entity.MonthlyExpense> LocalMonthlyExpenses
        {
            get
            {
                if (_context.MonthlyExpenses.Local.Count == 0)
                    _context.MonthlyExpenses.Load();
                return _context.MonthlyExpenses.Local;
            }
        }

        public static ObservableCollection<Entity.PackedCategory> LocalPackedCategories
        {
            get
            {
                if (_context.PackedCategories.Local.Count == 0)
                    _context.PackedCategories.Load();
                return _context.PackedCategories.Local;
            }
        }
        #endregion

        #region "Active" helper properties

        public static ObservableCollection<Entity.Account> ActiveAccounts
        {
            get
            {
                if (_context.Accounts.Local.Count == 0)
                    _context.Accounts.Load();
                return new ObservableCollection<Entity.Account>(
                    _context.Accounts.Local.Where(x => x.Active == true));
            }
        }

        public static ObservableCollection<Entity.CoffeeSort> ActiveCoffeeSorts
        {
            get
            {
                if (_context.CoffeeSorts.Local.Count == 0)
                    _context.CoffeeSorts.Load();
                return new ObservableCollection<Entity.CoffeeSort>(
                    _context.CoffeeSorts.Local.Where(x => x.Active == true));
            }
        }

        public static ObservableCollection<Entity.Supplier> ActiveSuppliers
        {
            get
            {
                if (_context.Suppliers.Local.Count == 0)
                    _context.Suppliers.Load();
                return new ObservableCollection<Entity.Supplier>(
                    _context.Suppliers.Local.Where(x => x.Active == true));
            }
        }

        public static ObservableCollection<Entity.Mix> ActiveMixes
        {
            get
            {
                if (_context.Mixes.Local.Count == 0)
                    _context.Mixes.Load();
                return new ObservableCollection<Entity.Mix>(
                    _context.Mixes.Local.Where(x => x.Active == true));
            }
        }

        public static ObservableCollection<Entity.Recipient> ActiveRecipients
        {
            get
            {
                if (_context.Recipients.Local.Count == 0)
                    _context.Recipients.Load();
                return new ObservableCollection<Entity.Recipient>(
                    _context.Recipients.Local.Where(x => x.Active == true));
            }
        }

        public static ObservableCollection<Entity.Package> ActivePackages
        {
            get
            {
                if (_context.Packages.Local.Count == 0)
                    _context.Packages.Load();
                return new ObservableCollection<Entity.Package>(
                    _context.Packages.Local.Where(x => x.Active == true));
            }
        }

        public static ObservableCollection<Entity.PackedCategory> ActivePackedCategories
        {
            get
            {
                if (_context.PackedCategories.Local.Count == 0)
                    _context.PackedCategories.Load();
                return new ObservableCollection<Entity.PackedCategory>(
                    _context.PackedCategories.Local.Where(x => x.Active == true));
            }
        }

        #endregion
    }
}
