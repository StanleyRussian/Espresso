using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class EditRoastingsViewModel : aProcessListingViewModel
    {
        protected override void Refresh()
        {
            var query = _context.Roastings.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            Roastings = new ObservableCollection<Roasting>(query);
        }

        #region Binding Properties

        private ObservableCollection<Roasting> _roastings;
        public ObservableCollection<Roasting> Roastings
        {
            get { return _roastings; }
            set
            {
                _roastings = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Roasting;
            _context.Roastings.Remove(selected);
            SaveContext();
        }

        #endregion

    }
}
