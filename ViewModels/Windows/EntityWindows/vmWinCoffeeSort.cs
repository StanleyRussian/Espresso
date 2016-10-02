using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinCoffeeSort: Abstract.aEntityWindowViewModel
    {
        public vmWinCoffeeSort(object argEntity) : base(argEntity) { }

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

        protected override void OnOpenEdit(object argEntity)
        {
            Sort = argEntity as CoffeeSort;
        }

        protected override void OnOpenNew()
        {
            Sort = new CoffeeSort();
        }

        protected override void OnSaveValidation() { }

        protected override void OnSaveEdit() { }

        protected override void OnSaveNew()
        {
            ContextManager.Context.CoffeeSorts.Add(Sort);
            ContextManager.Context.dCoffeeStocks.Add(new dCoffeeStock { CoffeeSort = Sort });
        }
    }
}
