using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    class MixesViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public MixesViewModel()
        {
            _context = new Entity.ContextContainer();
            _context.CoffeeSorts.Load();
            _context.Mixes.Load();

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            cmdDelete = new Auxiliary.Command(cmdDelete_Execute);
            cmdNew = new Auxiliary.Command(cmdNew_Execute);

            cmdToggleActive = new Auxiliary.Command(cmdToggleActive_Execute);
            cmdSelectActive = new Auxiliary.Command(cmdSelectActive_Execute, cmdSelectActive_CanExecute);
            cmdSelectInactive = new Auxiliary.Command(cmdSelectInactive_Execute, cmdSelectInactive_CanExecute);
            cmdSearch = new Auxiliary.Command(cmdSearch_Execute);
            cmdClearSearch = new Auxiliary.Command(cmdClearSearch_Execute);

            MixesSelected = new ObservableCollection<MixViewModel>();

            Refresh();
        }

        private void Refresh()
        {
            var query = _context.Mixes.Local.Where(p => p.Active == true);
            _mixesSelected.Clear();
            foreach (var x in query)
                _mixesSelected.Add(new MixViewModel(x));
            OnPropertyChanged("MixesSelected");

            activeIsSelected = true;

            cmdSelectActive.CanExecute(null);
            cmdSelectInactive.CanExecute(null);
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<MixViewModel> _mixesSelected;
        public ObservableCollection<MixViewModel> MixesSelected
        {
            get { return _mixesSelected; }
            set
            {
                _mixesSelected = value;
                OnPropertyChanged("MixesSelected");
            }
        }

        public ObservableCollection<Entity.CoffeeSort> CoffeeSorts
        {
            get { return _context.CoffeeSorts.Local; }
        }

        private string _filterName;
        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                OnPropertyChanged("FilterName");
            }
        }

        #endregion

        #region Commands

        public ICommand cmdSearch
        { get; private set; }

        private void cmdSearch_Execute()
        {
            var query = MixesSelected.Where(p => p.Name.Contains(FilterName) == true);
            _mixesSelected.Clear();
            foreach (var x in query)
                _mixesSelected.Add(x);
            OnPropertyChanged("MixesSelected");
        }

        public ICommand cmdClearSearch
        { get; private set; }

        private void cmdClearSearch_Execute()
        {
            FilterName = null;
            cmdSelectActive_Execute();
        }

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            foreach (var p in MixesSelected)
                p.SaveDetails();

            _context.SaveChanges();
            Refresh();
        }

        public ICommand cmdDelete
        { get; private set; }

        private void cmdDelete_Execute(object argSelected)
        {
            if (argSelected == null)
            {
                MessageBox.Show("Вы не выбрали купаж!");
                return;
            }

            MixViewModel selected = argSelected as MixViewModel;
            foreach (var detail in selected.Details)
                _context.Mix_Details.Remove(_context.Mix_Details.Find(detail.Id));

            _context.Mixes.Remove(_context.Mixes.Find(selected.Id));
            _context.SaveChanges();
            Refresh();
        }

        public ICommand cmdNew
        { get; private set; }

        private void cmdNew_Execute(object argSelected)
        {
            new Views.NewMix().ShowDialog();
            _context.Mixes.Load();
            Refresh();
        }

        public ICommand cmdToggleActive
        { get; private set; }

        private void cmdToggleActive_Execute(object argSelected)
        {
            MixViewModel selected = argSelected as MixViewModel;
            selected.Active = (selected.Active == true) ? false : true;
            _context.SaveChanges();
            Refresh();
        }

        private bool activeIsSelected;

        // Select ACTIVE command
        public ICommand cmdSelectActive
        { get; private set; }

        private void cmdSelectActive_Execute()
        {
            var query = _context.Mixes.Local.Where(p => p.Active == true);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            _mixesSelected.Clear();
            foreach (var x in query)
                _mixesSelected.Add(new MixViewModel(x));
            OnPropertyChanged("MixesSelected");

            activeIsSelected = true;

            cmdSelectActive.CanExecute(null);
            cmdSelectInactive.CanExecute(null);
        }

        private bool cmdSelectActive_CanExecute(object arg)
        {
            return !activeIsSelected;
        }

        // Select INACTIVE command
        public ICommand cmdSelectInactive
        { get; private set; }

        private void cmdSelectInactive_Execute()
        {
            var query = _context.Mixes.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            _mixesSelected.Clear();
            foreach (var x in query)
                _mixesSelected.Add(new MixViewModel(x));
            OnPropertyChanged("MixesSelected");

            activeIsSelected = false;

            cmdSelectActive.CanExecute(null);
            cmdSelectInactive.CanExecute(null);
        }

        private bool cmdSelectInactive_CanExecute(object arg)
        {
            return activeIsSelected;
        }

        #endregion
    }

    public class MixViewModel
    {
        private Entity.Mix _mix;

        public MixViewModel(Entity.Mix argMix)
        {
            _mix = argMix;

            Details = new ObservableCollection<Entity.Mix_Details>();
            foreach (var x in _mix.Mix_Details)
            {
                Details.Add(x);
            }
        }

        public void SaveDetails()
        {
            var context_Details = _mix.Mix_Details.ToList();
            foreach (var detail in Details)
            {
                if (!context_Details.Contains(detail))
                    _mix.Mix_Details.Add(detail);
            }
        }

        public int Id
        {
            get { return _mix.Id; }
        }

        public string Name
        {
            get { return _mix.Name; }
            set { _mix.Name = value; }
        }

        public string Description
        {
            get { return _mix.Description; }
            set { _mix.Description = value; }
        }

        public bool Active
        {
            get { return _mix.Active; }
            set { _mix.Active = value; }
        }

        public ObservableCollection<Entity.Mix_Details> Details
        { get; set; }
    }
}
