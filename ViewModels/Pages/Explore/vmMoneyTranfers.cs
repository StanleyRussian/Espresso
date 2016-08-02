using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmMoneyTranfers: aProcessListingViewModel
    {
        public vmMoneyTranfers()
        {
            Header = "Денежные переводы";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.MoneyTransfers.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            Tabs = new ObservableCollection<MoneyTransfer>(query);
        }

        private ObservableCollection<MoneyTransfer> _tabs;
        public ObservableCollection<MoneyTransfer> Tabs
        {
            get { return _tabs; }
            private set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }


        protected override void cmdDelete_Execute(object argSelected)
        { }
    }
}
