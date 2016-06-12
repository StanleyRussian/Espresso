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
    public class NewCoffeeSortViewModel: INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewCoffeeSortViewModel()
        {
            _context = new Entity.ContextContainer();
            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            Refresh();
        }

        private void Refresh()
        {
            NewCoffeeSort = new Entity.CoffeeSort();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.CoffeeSort _newCoffeeSort;
        public Entity.CoffeeSort NewCoffeeSort
        {
            get { return _newCoffeeSort; }
            set
            {
                _newCoffeeSort = value;
                OnPropertyChanged("NewCoffeeSort");
            }
        }
        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.CoffeeSorts.Add(NewCoffeeSort);
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно");
                Refresh();
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
