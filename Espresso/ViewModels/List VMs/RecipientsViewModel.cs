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
    public class RecipientsViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public RecipientsViewModel()
        {
            cmdDelete = new Auxiliary.Command(cmdDelete_Execute);
            cmdNew = new Auxiliary.Command(cmdNew_Execute);
            cmdSave = new Auxiliary.Command(cmdSave_Execute);

            cmdToggleActive = new Auxiliary.Command(cmdToggleActive_Execute);
            cmdSelectActive = new Auxiliary.Command(cmdSelectActive_Execute, cmdSelectActive_CanExecute);
            cmdSelectInactive = new Auxiliary.Command(cmdSelectInactive_Execute, cmdSelectInactive_CanExecute);
            cmdSearch = new Auxiliary.Command(cmdSearch_Execute);
            cmdCleanSearch = new Auxiliary.Command(cmdCleanSearch_Execute);

            _context = new Entity.ContextContainer();
            _context.Recipients.Load();
            Refresh();
        }

        private void Refresh()
        {
            RecipientsSelected = new ObservableCollection<Entity.Recipient>(
                _context.Recipients.Local.Where(p => p.Active == true));
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

        private ObservableCollection<Entity.Recipient> _suppliersSelected;
        public ObservableCollection<Entity.Recipient> RecipientsSelected
        {
            get { return _suppliersSelected; }
            set
            {
                _suppliersSelected = value;
                OnPropertyChanged("RecipientsSelected");
            }
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
            RecipientsSelected = new ObservableCollection<Entity.Recipient>(
                RecipientsSelected.Where(p => p.Name.Contains(FilterName) == true));
        }

        public ICommand cmdCleanSearch
        { get; private set; }

        private void cmdCleanSearch_Execute()
        {
            FilterName = null;
            cmdSelectActive_Execute();
        }

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.SaveChanges();
        }

        public ICommand cmdNew
        { get; private set; }

        private void cmdNew_Execute()
        {
            new Views.NewRecipient().ShowDialog();
            _context.Recipients.Load();
            Refresh();
        }

        public ICommand cmdDelete
        { get; private set; }

        private void cmdDelete_Execute(object argSelected)
        {
            if (argSelected == null)
            {
                MessageBox.Show("Вы не выбрали клиента!");
                return;
            }

            var selected = argSelected as Entity.Recipient;
            try
            {
                _context.Recipients.Remove(selected);
                _context.SaveChanges();
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ICommand cmdToggleActive
        { get; private set; }

        private void cmdToggleActive_Execute(object argSelected)
        {
            Entity.Recipient selected = _context.Recipients.Find(((Entity.Recipient)argSelected).Id);
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
            var query = _context.Recipients.Local.Where(p => p.Active == true);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            RecipientsSelected = new ObservableCollection<Entity.Recipient>(query);
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
            var query = _context.Recipients.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            RecipientsSelected = new ObservableCollection<Entity.Recipient>(query);
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
}
