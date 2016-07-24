using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;

namespace ViewModels.Statistics.Abstract
{
    public abstract class aSubjectsListViewModel :aTabViewModel
    {
        protected aSubjectsListViewModel()
        {
            cmdToggleActive = new Command(cmdToggleActive_Execute);
            cmdSelectActive = new Command(cmdSelectActive_Execute, cmdSelectActive_CanExecute);
            cmdSelectInactive = new Command(cmdSelectInactive_Execute, cmdSelectInactive_CanExecute);
            cmdSearch = new Command(cmdSearch_Execute);
            cmdClearSearch = new Command(cmdClearSearch_Execute);
            cmdDelete = new Command(cmdDelete_Execute);
            cmdReload = new Command(cmdReload_Execute);

            IsActiveSelected = true;
        }

        protected void SaveContext()
        {
            try
            {
                ContextManager.Context.SaveChanges();
                ContextManager.ReloadContext();
            }
            catch (Exception ex)
            {
                DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", ex.Message,
                    MessageDialogStyle.Affirmative, new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
                foreach (DbEntityEntry entry in ContextManager.Context.ChangeTracker.Entries())
                    if (entry.State == EntityState.Deleted)
                        entry.State = EntityState.Unchanged;
            }
        }


        #region Commands

        public ICommand cmdReload
        { get; private set; }
        protected abstract void cmdReload_Execute();

        public ICommand cmdSearch
        { get; private set; }
        protected abstract void cmdSearch_Execute();

        public ICommand cmdClearSearch
        { get; private set; }
        protected void cmdClearSearch_Execute()
        {
            FilterName = null;
            Load();
        }

        public ICommand cmdDelete
        { get; private set; }
        protected abstract void cmdDelete_Execute(object argSelected);

        protected bool IsEmpty(object argSelected)
        {
            if (argSelected == null)
            {
                DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", "Вы ничего не выбрали",
                    MessageDialogStyle.Affirmative, new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
                return true;
            }
            return false;
        }

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
            Load();
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
            Load();
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
                OnPropertyChanged();
            }
        }

        protected string _filterName;
        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                OnPropertyChanged();
            }
        }
    }
}

