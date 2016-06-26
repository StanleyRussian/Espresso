using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class AccountsViewModel: aSubjectsListingViewModel
    {
        protected override void Refresh()
        {
            if (IsActiveSelected)
                Selected = new ObservableCollection<Account>(
                    ContextManager.LocalAccounts.Where(p => p.Active));
            else
                Selected = new ObservableCollection<Account>(
                    ContextManager.LocalAccounts.Where(p => p.Active == false));
        }

        #region Binding Properties

        private ObservableCollection<Account> _selected;
        public ObservableCollection<Account> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        #endregion

        #region Commands

        protected override void cmdSearch_Execute()
        {
            Selected = new ObservableCollection<Account>(
                Selected.Where(p => p.Name.Contains(FilterName)));
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Account;
            _context.Accounts.Remove(selected);
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            Account selected = (Account) argSelected;
            selected.Active = !selected.Active;
            SaveContext();
        }

        protected override void cmdSelectActive_Execute()
        {
            var query = ContextManager.LocalAccounts.Where(p => p.Active);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<Account>(query);
            base.cmdSelectActive_Execute();
        }

        protected override void cmdSelectInactive_Execute()
        {
            var query = ContextManager.LocalAccounts.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<Account>(query);
            base.cmdSelectInactive_Execute();
        }
        #endregion
    }
}
