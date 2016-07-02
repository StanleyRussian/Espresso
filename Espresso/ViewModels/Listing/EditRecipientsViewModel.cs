using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class EditRecipientsViewModel : aSubjectsListingViewModel
    {
        protected override void Refresh()
        {
            if (IsActiveSelected)
                Selected = new ObservableCollection<Recipient>(
                    ContextManager.LocalRecipients.Where(p => p.Active));
            else
                Selected = new ObservableCollection<Recipient>(
                    ContextManager.LocalRecipients.Where(p => p.Active == false));
        }

        #region Binding Properties

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

        #endregion

        #region Commands

        protected override void cmdSearch_Execute()
        {
            Selected = new ObservableCollection<Recipient>(
                Selected.Where(p => p.Name.Contains(FilterName)));
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Recipient;
            _context.Recipients.Remove(selected);
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            Recipient selected = _context.Recipients.Find(((Recipient)argSelected).Id);
            selected.Active = !selected.Active;
            SaveContext();
        }

        protected override void cmdSelectActive_Execute()
        {
            var query = _context.Recipients.Local.Where(p => p.Active);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<Recipient>(query);
            base.cmdSelectActive_Execute();
        }

        protected override void cmdSelectInactive_Execute()
        {
            var query = _context.Recipients.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<Recipient>(query);
            base.cmdSelectInactive_Execute();
        }

        #endregion

    }
}
