using System.Collections.ObjectModel;
using System.Linq;
using Model;
using ViewModels.Statistics.Abstract;

namespace ViewModels.Statistics.Accounts
{
    public class ParentAccountsViewModel : aSubjectsListViewModel
    {
        private ObservableCollection<IndividualAccountViewModel> _active;
        private ObservableCollection<IndividualAccountViewModel> _inactive;

        public ParentAccountsViewModel()
        {
            Header = "Счета";
        }

        protected override void Load()
        {
            _context = ContextManager.Context;
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<IndividualAccountViewModel>();
                foreach (var active in _context.Accounts.Where(p => p.Active))
                    _active.Add(new IndividualAccountViewModel(active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<IndividualAccountViewModel>();
                foreach (var inactive in _context.Accounts.Where(p => !p.Active))
                    _inactive.Add(new IndividualAccountViewModel(inactive));
                Selected = _inactive;
            }
        }

        #region Binding Properties

        private ObservableCollection<IndividualAccountViewModel> _selected;

        public ObservableCollection<IndividualAccountViewModel> Selected
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
            _active = null;
            _inactive = null;
            Load();
        }

        protected override void cmdSearch_Execute()
        {
            //Selected = new ObservableCollection<IndividualAccountViewModel>(
            //    Selected.Where(p => p.Name.Contains(FilterName)));
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as IndividualAccountViewModel;
            _context.Accounts.Remove(selected.Account);
            SaveContext();
            cmdReload_Execute();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            var selected = argSelected as IndividualAccountViewModel;
            selected.Account.Active = !selected.Account.Active;
            SaveContext();
            cmdReload_Execute();
        }

        #endregion
    }
}
