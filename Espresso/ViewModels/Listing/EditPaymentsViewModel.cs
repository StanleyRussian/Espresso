using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class EditPaymentsViewModel : aProcessListingViewModel
    {
        protected override void Refresh()
        {
            var query = _context.Payments.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            Payments = new ObservableCollection<Payment>(query);
        }

        #region Binding Properties

        private ObservableCollection<Payment> _payments;
        public ObservableCollection<Payment> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Payment;
            _context.Payments.Remove(selected);
            SaveContext();
        }

        #endregion

    }
}
