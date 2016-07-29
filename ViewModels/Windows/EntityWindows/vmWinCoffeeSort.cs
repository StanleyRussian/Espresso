using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinCoffeeSort: Abstract.aEntityWindowViewModel
    {
        public vmWinCoffeeSort(object argSort = null)
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
                ContextManager.Context.CoffeeSorts.Add(Sort);
            SaveContext();
        }
    }
}
