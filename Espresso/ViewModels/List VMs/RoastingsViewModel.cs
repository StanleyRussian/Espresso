using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    class RoastingsViewModel
    {
        private Entity.ContextContainer _context;

        public RoastingsViewModel()
        {
            _context = new Entity.ContextContainer();
            _context.CoffeeSorts.Load();
            _context.Roastings.Load();

            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);

            Refresh();
        }

        private void Refresh()
        {
            var query = _context.Roastings.Local.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            Roastings = new ObservableCollection<Entity.Roasting>(query);
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Entity.Roasting> _roastings;
        public ObservableCollection<Entity.Roasting> Roastings
        {
            get { return _roastings; }
            set
            {
                _roastings = value;
                OnPropertyChanged("Roastings");
            }
        }

        public ObservableCollection<Entity.CoffeeSort> CoffeeSorts
        {
            get { return _context.CoffeeSorts.Local; }
        }

        private DateTime _filterFrom;
        public DateTime FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                OnPropertyChanged("FilterFrom");
                Refresh();
            }
        }

        private DateTime _filterTo;
        public DateTime FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                OnPropertyChanged("FilterTo");
                Refresh();
            }
        }

        #endregion

        #region Commands

        public ICommand cmdSave
        { get; private set; }

        private void cmdSaveChanges_Execute()
        {
            _context.SaveChanges();
            Refresh();
        }

        public ICommand cmdDelete
        { get; private set; }

        private void cmdDelete_Execute(object argSelected)
        {
            if (argSelected == null)
            {
                MessageBox.Show("Вы не выбрали счёт!");
                return;
            }

            var selected = argSelected as Entity.Roasting;
            try
            {
                _context.Roastings.Remove(selected);
                _context.SaveChanges();
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    public ICommand cmdNew
        { get; private set; }

        private void cmdNew_Execute(object argSelected)
        {
            new Views.NewRoasting().ShowDialog();
            Refresh();
        }

        public ICommand cmdFilter30Days
        { get; private set; }

        private void cmdFilter30Days_Execute()
        {
            _filterTo = DateTime.Now;
            FilterFrom = DateTime.Now.AddDays(-30);
        }

        public ICommand cmdFilterAll
        { get; private set; }

        private void cmdFilterAll_Execute()
        {
            _filterTo = DateTime.Now;
            FilterFrom = DateTime.MinValue;
        }
        #endregion

    }
}
