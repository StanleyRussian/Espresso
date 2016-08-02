using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmCoffeeTranfers: aProcessListingViewModel
    {
        public vmCoffeeTranfers()
        {
            Header = "Переводы кофе";
        }
        protected override void Refresh()
        {
            var query = ContextManager.Context.CoffeeTransfers.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterMix != null)
                query = query.Where(x => x.Mix.Id == FilterMix.Id);

            Tabs = new ObservableCollection<CoffeeTransfer>(query);
        }

        private ObservableCollection<CoffeeTransfer> _tabs;
        public ObservableCollection<CoffeeTransfer> Tabs
        {
            get { return _tabs; }
            private set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        private Mix _filterMix;
        public Mix FilterMix
        {
            get { return _filterMix; }
            set
            {
                _filterMix = value;
                OnPropertyChanged();
                Refresh();
            }
        }


        protected override void cmdDelete_Execute(object argSelected)
        {
            throw new NotImplementedException();
        }
    }
}
