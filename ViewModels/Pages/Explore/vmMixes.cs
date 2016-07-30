using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmMixes:aSubjectsListViewModel
    {
        private ObservableCollection<Mix> _active;
        private ObservableCollection<Mix> _inactive;

        public vmMixes()
        {
            Header = "Купажи";
        }

        protected override void Load()
        {
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<Mix>(
                    ContextManager.Context.Mixes.Where(p => p.Active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<Mix>(
                    ContextManager.Context.Mixes.Where(p => !p.Active));
                Selected = _inactive;
            }
        }

        private ObservableCollection<Mix> _selected;
        public ObservableCollection<Mix> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdReload_Execute()
        {
            ContextManager.ReloadContext();
            Load();
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
            var selected = argSelected as Mix;
            selected.Active = !selected.Active;
            SaveContext();
            cmdReload_Execute();
        }
    }
}
