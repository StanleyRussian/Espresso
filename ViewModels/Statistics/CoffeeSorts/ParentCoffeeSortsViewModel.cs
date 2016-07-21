using System.Collections.ObjectModel;
using System.Linq;
using Model;
using ViewModels.Statistics.Abstract;

namespace ViewModels.Statistics.CoffeeSorts
{
    public class ParentCoffeeSortsViewModel:aSubjectsListViewModel
    {
        private ObservableCollection<IndividualCoffeeSortViewModel> _active;
        private ObservableCollection<IndividualCoffeeSortViewModel> _inactive;

        public ParentCoffeeSortsViewModel()
        {
            Header = "Сорта кофе";
        }

        protected override void Load()
        {
            _context = ContextManager.Context;
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<IndividualCoffeeSortViewModel>();
                foreach (var active in _context.CoffeeSorts.Where(p => p.Active))
                    _active.Add(new IndividualCoffeeSortViewModel(active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<IndividualCoffeeSortViewModel>();
                foreach (var inactive in _context.CoffeeSorts.Where(p => !p.Active))
                    _inactive.Add(new IndividualCoffeeSortViewModel(inactive));
                Selected = _inactive;
            }
        }

        #region Binding Properties

        private ObservableCollection<IndividualCoffeeSortViewModel> _selected;
        public ObservableCollection<IndividualCoffeeSortViewModel> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        #endregion

        protected override void cmdReload_Execute()
        {
            _active = null;
            _inactive = null;
            Load();
        }

        // placeholer
        protected override void cmdSearch_Execute()
        { }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as IndividualCoffeeSortViewModel;
            _context.CoffeeSorts.Remove(selected.CoffeeSort);
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            var selected = argSelected as IndividualCoffeeSortViewModel;
            selected.CoffeeSort.Active = !selected.CoffeeSort.Active;
            SaveContext();
            cmdReload_Execute();
        }
    }
}
