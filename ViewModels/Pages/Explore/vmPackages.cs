using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmPackages:aSubjectsListViewModel
    {
        private ObservableCollection<Package> _active;
        private ObservableCollection<Package> _inactive;

        public vmPackages()
        {
            Header = "Упаковки";
        }

        protected override void Load()
        {
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<Package>(
                    ContextManager.Context.Packages.Where(p => p.Active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<Package>(
                    ContextManager.Context.Packages.Where(p => !p.Active));
                Selected = _inactive;
            }
        }

        private ObservableCollection<Package> _selected;
        public ObservableCollection<Package> Selected
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
            var selected = argSelected as Package;
            selected.Active = !selected.Active;
            SaveContext();
            cmdReload_Execute();
        }
    }
}
