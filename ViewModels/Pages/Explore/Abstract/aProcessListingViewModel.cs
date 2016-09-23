using System;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Model;
using ViewModels.Auxiliary;

namespace ViewModels.Pages.Explore.Abstract
{
    public abstract class aProcessListingViewModel: aTabViewModel
    {
        protected aProcessListingViewModel()
        {
            cmdSave = new Command(cmdSave_Execute);
            cmdDelete = new Command(cmdDelete_Execute);
            cmdFilter30Days = new Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Command(cmdFilterAll_Execute);
            cmdReload = new Command(cmdReload_Execute);
        }

        protected void SaveContext()
        {
            try
            {
                ContextManager.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", ex.Message,
                    MessageDialogStyle.Affirmative, new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
            }
        }

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

        protected override void Load()
        {
            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);
            Refresh();
        }

        protected abstract void Refresh();

        #region Binding Properties

        protected DateTime _filterFrom;
        public DateTime FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        protected DateTime _filterTo;
        public DateTime FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        #endregion

        #region Commands

        public ICommand cmdReload
        { get; private set; }
        private void cmdReload_Execute()
        {
            Load();
        }

        public ICommand cmdSave
        { get; private set; }
        protected virtual void cmdSave_Execute()
        {
            ContextManager.Context.SaveChanges();
            DialogCoordinator.Instance.ShowMessageAsync(this, "Успех", "Сохранение завершено");
        }

        public ICommand cmdDelete
        { get; private set; }
        protected abstract void cmdDelete_Execute(object argSelected);

        public ICommand cmdFilter30Days
        { get; private set; }
        protected void cmdFilter30Days_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged("FilterTo");
            _filterFrom = DateTime.Now.AddDays(-30);
            OnPropertyChanged("FilterFrom");
            Refresh();
        }

        public ICommand cmdFilterAll
        { get; private set; }
        protected void cmdFilterAll_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged("FilterTo");
            _filterFrom = FilterTo.AddDays(-365);
            OnPropertyChanged("FilterFrom");
            Refresh();
        }

        #endregion
    }
}
