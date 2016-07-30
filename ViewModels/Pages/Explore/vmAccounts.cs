using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmAccounts : aSubjectsListViewModel
    {
        private ObservableCollection<Account> _active;
        private ObservableCollection<Account> _inactive;

        public vmAccounts()
        {
            Header = "Счета";
        }

        protected override void Load()
        {
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<Account>(
                    ContextManager.Context.Accounts.Where(p => p.Active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<Account>(
                    ContextManager.Context.Accounts.Where(p => !p.Active));
                Selected = _inactive;
            }
        }

        #region Binding Properties

        private ObservableCollection<Account> _selected;
        public ObservableCollection<Account> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdReload_Execute()
        {
            ContextManager.ReloadContext();
            Load();
        }

        protected override void cmdSearch_Execute()
        { }

        protected override void cmdDelete_Execute(object argSelected)
        { }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            var selected = argSelected as Account;
            selected.Active = !selected.Active;
            SaveContext();
            cmdReload_Execute();
        }

        #endregion
    }
}
