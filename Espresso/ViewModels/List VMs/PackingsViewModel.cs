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
    class PackingsViewModel
    {
        private Entity.ContextContainer _context;

        public PackingsViewModel()
        {
            _context = new Entity.ContextContainer();
            _context.Packings.Load();
            _context.Mixes.Load();
            _context.Packages.Load();
            _context.PackedCategories.Load();

            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            cmdDelete = new Auxiliary.Command(cmdDelete_Execute);
            cmdNew = new Auxiliary.Command(cmdNew_Execute);
            cmdFilter30Days = new Auxiliary.Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Auxiliary.Command(cmdFilterAll_Execute);

            Refresh();
        }

        private void Refresh()
        {
            var query = _context.Packings.Local.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            Packings = new ObservableCollection<Entity.Packing>(query);
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Entity.Packing> _roastings;
        public ObservableCollection<Entity.Packing> Packings
        {
            get { return _roastings; }
            set
            {
                _roastings = value;
                OnPropertyChanged("Packings");
            }
        }

        public ObservableCollection<Entity.Mix> Mixes
        {
            get { return _context.Mixes.Local; }
        }

        public ObservableCollection<Entity.Package> Packages
        {
            get { return _context.Packages.Local; }
        }

        public ObservableCollection<Entity.PackedCategory> PackedCategories
        {
            get { return _context.PackedCategories.Local; }
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

        private void cmdSave_Execute()
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
                MessageBox.Show("Вы не выбрали обжжарку!");
                return;
            }

            var selected = argSelected as Entity.Packing;
            try
            {
                _context.Packings.Remove(selected);
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
            new Views.NewPacking().ShowDialog();
            _context.Packings.Load();
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
