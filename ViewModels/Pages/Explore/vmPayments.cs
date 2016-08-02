﻿using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmPayments: aProcessListingViewModel
    {
        public vmPayments()
        {
            Header = "Платежи";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.Payments.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterAccount != null)
                query = query.Where(p => p.Account.Id == FilterAccount.Id);

            Tabs = new ObservableCollection<Payment>(query);
        }

        private ObservableCollection<Payment> _tabs;
        public ObservableCollection<Payment> Tabs
        {
            get { return _tabs; }
            private set
            {
                _tabs = value;
                OnPropertyChanged();
            }
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

        protected override void cmdDelete_Execute(object argSelected)
        { }
    }
}
