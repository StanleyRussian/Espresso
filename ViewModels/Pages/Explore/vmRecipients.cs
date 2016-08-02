using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmRecipients: aSubjectsListViewModel
    {
        private ObservableCollection<Recipient> _active;
        private ObservableCollection<Recipient> _inactive;

        public vmRecipients()
        {
            Header = "Клиенты";
        }

        protected override void Load()
        {
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<Recipient>(
                    ContextManager.Context.Recipients.Where(p => p.Active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<Recipient>(
                    ContextManager.Context.Recipients.Where(p => !p.Active));
                Selected = _inactive;
            }
        }

        private ObservableCollection<Recipient> _selected;
        public ObservableCollection<Recipient> Selected
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
            var selected = argSelected as Recipient;
            selected.Active = !selected.Active;
            SaveContext();
            cmdReload_Execute();
        }
    }
}
