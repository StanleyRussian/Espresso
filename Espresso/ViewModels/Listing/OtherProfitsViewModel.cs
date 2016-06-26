using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class OtherProfitsViewModel : aProcessListingViewModel
    {
        protected override void Refresh()
        {
            var query = _context.OtherProfits.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            OtherProfits = new ObservableCollection<OtherProfit>(query);
        }

        #region Binding Properties

        private ObservableCollection<OtherProfit> _otherProfits;
        public ObservableCollection<OtherProfit> OtherProfits
        {
            get { return _otherProfits; }
            set
            {
                _otherProfits = value;
                OnPropertyChanged("OtherProfits");
            }
        }

        #endregion

        #region Commands

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as OtherProfit;
            _context.OtherProfits.Remove(selected);
            SaveContext();
        }

        #endregion

    }
}
