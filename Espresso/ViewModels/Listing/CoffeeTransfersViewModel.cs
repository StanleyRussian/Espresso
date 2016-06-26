using System.Collections.ObjectModel;
using System.Linq;

namespace Core.ViewModels.Listing
{
    public class CoffeeTransfersViewModel : Abstract.aProcessListingViewModel
    {
        protected override void Refresh()
        {
            var query = _context.CoffeeTransfers.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            CoffeeTransfers = new ObservableCollection<Entity.CoffeeTransfer>(query);
        }

        #region Binding Properties 

        private ObservableCollection<Entity.CoffeeTransfer> _coffeeTransfers;
        public ObservableCollection<Entity.CoffeeTransfer> CoffeeTransfers
        {
            get { return _coffeeTransfers; }
            set
            {
                _coffeeTransfers = value;
                OnPropertyChanged("CoffeeTransfers");
            }
        }

        #endregion

        #region Commands

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Entity.CoffeeTransfer;
            _context.CoffeeTransfers.Remove(selected);
            SaveContext();
        }

        #endregion
    }
}
