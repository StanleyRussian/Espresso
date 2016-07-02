using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class EditMonthlyExpensesViewModel : aSubjectsListingViewModel
    {
        protected override void Refresh()
        {
            if (IsActiveSelected)
                Selected = new ObservableCollection<MonthlyExpense>(
                    ContextManager.LocalMonthlyExpenses.Where(p => p.Active));
            else
                Selected = new ObservableCollection<MonthlyExpense>(
                    ContextManager.LocalMonthlyExpenses.Where(p => p.Active == false));
        }

        #region Binding Properties 

        private ObservableCollection<MonthlyExpense> _selected;
        public ObservableCollection<MonthlyExpense> Selected
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

        protected override void cmdSearch_Execute()
        {
            Selected = new ObservableCollection<MonthlyExpense>(
                Selected.Where(p => p.Designation.Contains(FilterName)));
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as MonthlyExpense;
            _context.MonthlyExpenses.Remove(selected);
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            MonthlyExpense selected = (MonthlyExpense)argSelected;
            selected.Active = !selected.Active;
            SaveContext();
        }

        protected override void cmdSelectActive_Execute()
        {
            var query = _context.MonthlyExpenses.Local.Where(p => p.Active);
            if (FilterName != null)
                query = query.Where(p => p.Designation.Contains(FilterName));
            Selected = new ObservableCollection<MonthlyExpense>(query);
            base.cmdSelectActive_Execute();
        }

        protected override void cmdSelectInactive_Execute()
        {
            var query = _context.MonthlyExpenses.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Designation.Contains(FilterName));
            Selected = new ObservableCollection<MonthlyExpense>(query);
            base.cmdSelectInactive_Execute();
        }

        #endregion
    }
}
