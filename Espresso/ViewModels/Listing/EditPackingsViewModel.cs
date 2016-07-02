using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class EditPackingsViewModel : aProcessListingViewModel
    {
        protected override void Refresh()
        {
            var query = _context.Packings.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            Packings = new ObservableCollection<Packing>(query);
        }

        #region Binding Properties

        private ObservableCollection<Packing> _packings;
        public ObservableCollection<Packing> Packings
        {
            get { return _packings; }
            set
            {
                _packings = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Packing;
            _context.Packings.Remove(selected);
            SaveContext();
        }

        #endregion

    }
}
