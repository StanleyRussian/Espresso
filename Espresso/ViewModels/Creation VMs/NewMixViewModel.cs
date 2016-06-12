using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    class NewMixViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewMixViewModel()
        {
            _context = new Entity.ContextContainer();

            _activeCoffeeSorts = new ObservableCollection<Entity.CoffeeSort>(
                _context.CoffeeSorts.Where(x => x.Active == true));

            cmdSave = new Auxiliary.Command(cmdSave_Execute);

            Refresh();
        }

        private void Refresh()
        {
            NewMix = new Entity.Mix();
            Details = new ObservableCollection<Entity.Mix_Details>();
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.Mix _newMix;
        public Entity.Mix NewMix
        {
            get { return _newMix; }
            set
            {
                _newMix = value;
                OnPropertyChanged("NewMix");
            }
        }

        private ObservableCollection<Entity.Mix_Details> _details;
        public ObservableCollection<Entity.Mix_Details> Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged("Details");
            }
        }

        private ObservableCollection<Entity.CoffeeSort> _activeCoffeeSorts;
        public ObservableCollection<Entity.CoffeeSort> CoffeeSorts
        {
            get { return _activeCoffeeSorts; }
        }

        #endregion

        #region Commands

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            double total = 0;
            foreach (var x in Details)
                total += x.Ratio;
            if (total != 100)
            {
                MessageBox.Show("Неправильные пропорции, общая сумма не равна 100%");
                return;
            }

            _newMix.Mix_Details.Clear();
            foreach (var x in Details)
                _newMix.Mix_Details.Add(x);
            _context.Mixes.Add(_newMix);

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

    #endregion
}