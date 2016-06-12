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
    class NewRoastingViewModel: INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewRoastingViewModel()
        {
            _context = new Entity.ContextContainer();

            _activeCoffeeSorts = new ObservableCollection<Entity.CoffeeSort>(
                _context.CoffeeSorts.Where(x => x.Active == true));

            NewRoasting = new Entity.Roasting();
            NewRoasting.Date = DateTime.Today;
            NewRoasting.CoffeeSort = _context.CoffeeSorts.Local.FirstOrDefault();

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
        }

        private ObservableCollection<Entity.CoffeeSort> _activeCoffeeSorts;
        public ObservableCollection<Entity.CoffeeSort> CoffeeSorts
        {
            get { return _activeCoffeeSorts; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Since DB doesn't store srinkage percentage rather then both quantities
        // some additional magic required in view model
        private int _shrinkagePercent;
        public int ShrinkagePercent
        {
            get { return _shrinkagePercent; }
            set
            {
                _shrinkagePercent = value;
                OnPropertyChanged("ShrinkagePercent");
            }

        }

        private Entity.Roasting _newRoasting;
        public Entity.Roasting NewRoasting
        {
            get { return _newRoasting; }
            set
            {
                _newRoasting = value;
                OnPropertyChanged("NewRoasting");
            }
        }

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            NewRoasting.RoastedAmount = NewRoasting.InitialAmount * (100-ShrinkagePercent) /100;
            _context.Roastings.Add(NewRoasting);
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно");
                NewRoasting = new Entity.Roasting();
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
