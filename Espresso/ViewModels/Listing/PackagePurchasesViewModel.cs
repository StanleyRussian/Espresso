using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class PackagePurchasesViewModel : aProcessListingViewModel
    {
        protected override void Refresh()
        {
            var query = _context.PackagePurchases.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            PackagePurchases = new ObservableCollection<PackagePurchase>(query);
        }

        #region Binding Properties

        private ObservableCollection<PackagePurchase> _packagePurchases;
        public ObservableCollection<PackagePurchase> PackagePurchases
        {
            get { return _packagePurchases; }
            set
            {
                _packagePurchases = value;
                OnPropertyChanged("PackagePurchases");
            }
        }

        #endregion

        #region Commands

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as PackagePurchase;
            _context.PackagePurchases.Remove(selected);
            SaveContext();
        }

        #endregion

    }
}
