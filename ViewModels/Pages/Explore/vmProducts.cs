using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmProducts: aSubjectsListViewModel
    {
        private ObservableCollection<Product> _active;
        private ObservableCollection<Product> _inactive;

        public vmProducts()
        {
            Header = "Товары";
        }

        protected override void Load()
        {
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<Product>(
                    ContextManager.Context.Products.Where(p => p.Active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<Product>(
                    ContextManager.Context.Products.Where(p => !p.Active));
                Selected = _inactive;
            }
        }

        private ObservableCollection<Product> _selected;
        public ObservableCollection<Product> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value; 
                OnPropertyChanged();
            }
        }

        protected override void cmdSearch_Execute()
        { }

        protected override void cmdDelete_Execute(object argSelected)
        { }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            var selected = argSelected as Product;
            selected.Active = !selected.Active;
            SaveContext();
            cmdReload_Execute();
        }
    }
}
