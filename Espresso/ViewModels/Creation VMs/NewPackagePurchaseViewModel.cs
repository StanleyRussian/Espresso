using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    class NewPackagePurchaseViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewPackagePurchaseViewModel()
        {
            _context = new Entity.ContextContainer();

            _activeAccounts = new ObservableCollection<Entity.Account>(
                _context.Accounts.Where(x => x.Active == true));
            _activeSuppliers = new ObservableCollection<Entity.Supplier>(
                _context.Suppliers.Where(x => x.Active == true));
            _activePackages = new ObservableCollection<Entity.Package>(
                _context.Packages.Where(x => x.Active == true));

            NewPackagePurchase = new Entity.PackagePurchase();
            NewPackagePurchase.Date = DateTime.Now;

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
        }

        private ObservableCollection<Entity.Account> _activeAccounts;
        public ObservableCollection<Entity.Account> Accounts
        {
            get { return _activeAccounts; }
        }

        private ObservableCollection<Entity.Supplier> _activeSuppliers;
        public ObservableCollection<Entity.Supplier> Suppliers
        {
            get { return _activeSuppliers; }
        }

        private ObservableCollection<Entity.Package> _activePackages;
        public ObservableCollection<Entity.Package> Packages
        {
            get { return _activePackages; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.PackagePurchase _newPackagePurchase;
        public Entity.PackagePurchase NewPackagePurchase
        {
            get { return _newPackagePurchase; }
            set
            {
                _newPackagePurchase = value;
                OnPropertyChanged("NewPackagePurchase");
            }
        }

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.PackagePurchases.Add(NewPackagePurchase);
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно");
                NewPackagePurchase = new Entity.PackagePurchase();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var x in ex.EntityValidationErrors)
                    foreach (var y in x.ValidationErrors)
                        MessageBox.Show(y.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
