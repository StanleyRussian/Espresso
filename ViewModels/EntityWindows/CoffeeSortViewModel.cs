using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class CoffeeSortViewModel: Abstract.aEntityWindowViewModel
    {
        public CoffeeSortViewModel(object argSort = null)
        {
            if (argSort != null)
                Sort = argSort as CoffeeSort;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Sort = new CoffeeSort();
        }

        private CoffeeSort _sort;
        public CoffeeSort Sort
        {
            get { return _sort; }
            set
            {
                _sort = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                _context.CoffeeSorts.Add(Sort);
            SaveContext();
        }
    }
}
