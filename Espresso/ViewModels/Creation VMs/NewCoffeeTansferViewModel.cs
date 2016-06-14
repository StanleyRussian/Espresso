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
    class NewCoffeeTransferViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewCoffeeTransferViewModel()
        {
            _context = new Entity.ContextContainer();

            _activeMixes = new ObservableCollection<Entity.Mix>(
                _context.Mixes.Where(x => x.Active == true));

            NewCoffeeTransfer = new Entity.CoffeeTransfer();
            NewCoffeeTransfer.Date = DateTime.Now;
            NewCoffeeTransfer.Mix = _context.Mixes.Local.FirstOrDefault();

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
        }

        private ObservableCollection<Entity.Mix> _activeMixes;
        public ObservableCollection<Entity.Mix> Mixes
        {
            get { return _activeMixes; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.CoffeeTransfer _newCoffeeTransfer;
        public Entity.CoffeeTransfer NewCoffeeTransfer
        {
            get { return _newCoffeeTransfer; }
            set
            {
                _newCoffeeTransfer = value;
                OnPropertyChanged("NewCoffeeTransfer");
            }
        }

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.CoffeeTransfers.Add(NewCoffeeTransfer);
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно");
                NewCoffeeTransfer = new Entity.CoffeeTransfer();
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
