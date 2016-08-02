using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmPackedCategories: aSubjectsListViewModel
    {
        private ObservableCollection<PackedCategory> _active;
        private ObservableCollection<PackedCategory> _inactive;

        public vmPackedCategories()
        {
            Header = "Категории фасовки";
        }

        protected override void Load()
        {
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<PackedCategory>(
                    ContextManager.Context.PackedCategories.Where(p => p.Active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<PackedCategory>(
                    ContextManager.Context.PackedCategories.Where(p => !p.Active));
                Selected = _inactive;
            }
        }

        private ObservableCollection<PackedCategory> _selected;
        public ObservableCollection<PackedCategory> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSearch_Execute()
        {
            throw new NotImplementedException();
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            throw new NotImplementedException();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            var selected = argSelected as PackedCategory;
            selected.Active = !selected.Active;
            SaveContext();
            cmdReload_Execute();
        }
    }
}
