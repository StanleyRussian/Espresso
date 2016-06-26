﻿namespace Core.ViewModels.Creational
{
    public class NewCoffeeSortViewModel: Abstract.aCreationalViewModel
    {
        public NewCoffeeSortViewModel() : base() { }

        protected override void Refresh()
        {
            NewCoffeeSort = new Entity.CoffeeSort();
        }

        private Entity.CoffeeSort _newCoffeeSort;
        public Entity.CoffeeSort NewCoffeeSort
        {
            get { return _newCoffeeSort; }
            set
            {
                _newCoffeeSort = value;
                OnPropertyChanged("NewCoffeeSort");
            }
        }

        protected override void cmdSave_Execute()
        {
            _context.CoffeeSorts.Add(NewCoffeeSort);
            SaveContext();
        }
    }
}