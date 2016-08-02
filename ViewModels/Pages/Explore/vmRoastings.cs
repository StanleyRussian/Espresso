using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmRoastings: aProcessListingViewModel
    {
        public vmRoastings()
        {
            Header = "Обжарки";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.Roastings.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterCoffeeSort != null)
                query = query.Where(p => p.CoffeeSort.Id == FilterCoffeeSort.Id);
            Tabs = new ObservableCollection<Roasting>(query);
        }

        private ObservableCollection<Roasting> _tabs;
        public ObservableCollection<Roasting> Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        private CoffeeSort _filterCoffeeSort;
        public CoffeeSort FilterCoffeeSort
        {
            get { return _filterCoffeeSort; }
            set
            {
                _filterCoffeeSort = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        protected override void cmdDelete_Execute(object argSelected)
        { }
    }
}
