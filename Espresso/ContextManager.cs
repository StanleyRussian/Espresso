using System.Collections.ObjectModel;
using System.Linq;
using Model.Entity;

namespace Model
{
    public static class ContextManager
    {
        private static ContextContainer _context;
        public static ContextContainer Context => _context ?? (_context = new ContextContainer());

        public static void ReloadContext()
        {
            _context = new ContextContainer();
        }

        public static ObservableCollection<Account> ActiveAccounts => new ObservableCollection<Account>(Context.Accounts.Where(p => p.Active));
        public static ObservableCollection<CoffeeSort> ActiveCoffeeSorts => new ObservableCollection<CoffeeSort>(Context.CoffeeSorts.Where(p => p.Active));
        public static ObservableCollection<Mix> ActiveMixes => new ObservableCollection<Mix>(Context.Mixes.Where(p => p.Active));
        public static ObservableCollection<Package> ActivePackages => new ObservableCollection<Package>(Context.Packages.Where(p => p.Active));
        public static ObservableCollection<Recipient> ActiveRecipients => new ObservableCollection<Recipient>(Context.Recipients.Where(p => p.Active));
        public static ObservableCollection<Supplier> ActiveSuppliers => new ObservableCollection<Supplier>(Context.Suppliers.Where(p => p.Active));

        public static ObservableCollection<Account> AllAccounts => new ObservableCollection<Account>(Context.Accounts);
        public static ObservableCollection<CoffeeSort> AllCoffeeSorts => new ObservableCollection<CoffeeSort>(Context.CoffeeSorts);
        public static ObservableCollection<Mix> AllMixes => new ObservableCollection<Mix>(Context.Mixes);
        public static ObservableCollection<Package> AllPackages => new ObservableCollection<Package>(Context.Packages);
        public static ObservableCollection<Recipient> AllRecipients => new ObservableCollection<Recipient>(Context.Recipients);
        public static ObservableCollection<Supplier> AllSuppliers => new ObservableCollection<Supplier>(Context.Suppliers);
    }
}
