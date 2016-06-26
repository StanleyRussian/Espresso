using System.Windows.Input;
using Core.Auxiliary;
using MahApps.Metro.Controls.Dialogs;

namespace Core.ViewModels.Listing.Abstract
{
    public abstract class aSubjectsListingViewModel : aListingViewModel
    {
        public aSubjectsListingViewModel()
        {
            cmdSave = new Command(cmdSave_Execute);
            cmdDelete = new Command(cmdDelete_Execute);
            cmdToggleActive = new Command(cmdToggleActive_Execute);
            cmdSelectActive = new Command(cmdSelectActive_Execute, cmdSelectActive_CanExecute);
            cmdSelectInactive = new Command(cmdSelectInactive_Execute, cmdSelectInactive_CanExecute);
            cmdSearch = new Command(cmdSearch_Execute);
            cmdClearSearch = new Command(cmdClearSearch_Execute);

            IsActiveSelected = true;
            Refresh();
        }

        #region Commands

        public ICommand cmdSearch
        { get; private set; }
        protected abstract void cmdSearch_Execute();

        public ICommand cmdClearSearch
        { get; private set; }
        protected void cmdClearSearch_Execute()
        {
            FilterName = null;
            Refresh();
        }

        public ICommand cmdSave
        { get; private set; }
        protected virtual void cmdSave_Execute()
        {
            _context.SaveChanges();
            DialogCoordinator.Instance.ShowMessageAsync(this, "Успех", "Сохранение завершено");
        }

        public ICommand cmdDelete
        { get; private set; }
        protected abstract void cmdDelete_Execute(object argSelected);

        public ICommand cmdToggleActive
        { get; private set; }
        protected abstract void cmdToggleActive_Execute(object argSelected);

        // Select ACTIVE command
        public ICommand cmdSelectActive
        { get; private set; }

        protected virtual void cmdSelectActive_Execute()
        {
            IsActiveSelected = true;
            cmdSelectActive.CanExecute(null);
            cmdSelectInactive.CanExecute(null);
        }

        protected bool cmdSelectActive_CanExecute(object arg)
        {
            return !IsActiveSelected;
        }

        // Select INACTIVE command
        public ICommand cmdSelectInactive
        { get; private set; }

        protected virtual void cmdSelectInactive_Execute()
        {
            IsActiveSelected = false;
            cmdSelectActive.CanExecute(null);
            cmdSelectInactive.CanExecute(null);
        }

        protected bool cmdSelectInactive_CanExecute(object arg)
        {
            return IsActiveSelected;
        }


        #endregion

        protected bool _isActiveSelected;
        public bool IsActiveSelected
        {
            get { return _isActiveSelected; }
            protected set
            {
                _isActiveSelected = value;
                OnPropertyChanged("IsActiveSelected");
            }
        }

        protected string _filterName;
        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                OnPropertyChanged("FilterName");
            }
        }

    }
}

