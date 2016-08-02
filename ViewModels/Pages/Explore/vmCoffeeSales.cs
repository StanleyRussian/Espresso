using System;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmCoffeeSales: aProcessListingViewModel
    {
        public vmCoffeeSales()
        {
            Header = "Продажи кофе";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.CoffeeSales.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterAccount != null)
                query = query.Where(p => p.Account.Id == FilterAccount.Id);
            if (FilterRecipient != null)
                query = query.Where(p => p.Recipient.Id == FilterRecipient.Id);
            if (FilterMix != null)
                query = query.Where(p => p.Sale_Details
                    .FirstOrDefault(x => x.Mix.Id == FilterMix.Id) != null);

        }

        private Account _filterAccount;
        public Account FilterAccount
        {
            get { return _filterAccount; }
            set
            {
                _filterAccount = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private Recipient _filterRecipient;
        public Recipient FilterRecipient
        {
            get { return _filterRecipient; }
            set
            {
                _filterRecipient = value;
                OnPropertyChanged();
                Refresh();
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
