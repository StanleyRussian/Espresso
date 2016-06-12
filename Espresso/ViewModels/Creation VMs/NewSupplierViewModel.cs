using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    public class NewSupplierViewModel: INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewSupplierViewModel() 
        {
            _context = new Entity.ContextContainer();
            NewSupplier = new Entity.Supplier();

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.Supplier _newSupplier;
        public Entity.Supplier NewSupplier
        {
            get { return _newSupplier; }
            set
            {
                _newSupplier = value;
                OnPropertyChanged("NewSupplier");
            }
        }

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.Suppliers.Add(NewSupplier);
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно");
                NewSupplier = new Entity.Supplier();
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
