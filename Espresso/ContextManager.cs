using System.Collections.ObjectModel;
using System.Linq;
using Model.Entity;

namespace Model
{
    public static class ContextManager
    {
        private static ContextContainer _context;
        public static ContextContainer Context => _context ?? (_context = new ContextContainer());

        public static ObservableCollection<Account> ActiveAccounts
        {
            get { return new ObservableCollection<Account>(Context.Accounts.Where(p => p.Active)); }
        }

        public static ObservableCollection<CoffeeSort> ActiveCoffeeSorts
        {
            get { return new ObservableCollection<CoffeeSort>(Context.CoffeeSorts.Where(p => p.Active)); }
        }

        public static ObservableCollection<Mix> ActiveMixes
        {
            get { return new ObservableCollection<Mix>(Context.Mixes.Where(p => p.Active)); }
        }

        public static ObservableCollection<Package> ActivePackages
        {
            get { return new ObservableCollection<Package>(Context.Packages.Where(p => p.Active)); }
        }

        public static ObservableCollection<PackedCategory> ActivePackedCategories
        {
            get { return new ObservableCollection<PackedCategory>(Context.PackedCategories.Where(p => p.Active)); }
        }

        public static ObservableCollection<Recipient> ActiveRecipients
        {
            get { return new ObservableCollection<Recipient>(Context.Recipients.Where(p => p.Active)); }
        }

        public static ObservableCollection<Supplier> ActiveSuppliers
        {
            get { return new ObservableCollection<Supplier>(Context.Suppliers.Where(p => p.Active)); }
        }
    }
}
