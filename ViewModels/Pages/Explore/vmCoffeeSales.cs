using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmCoffeeSales : aProcessListingViewModel
    {
        public vmCoffeeSales()
        {
            Header = "Продажи кофе";
            cmdFilterUnpaid = new Command(cmdFilterUnpaid_Execute);
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.CoffeeSales.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterAccount != null)
                query = query.Where(p => p.Account.Id == FilterAccount.Id);
            if (FilterRecipient != null)
                query = query.Where(p => p.Recipient.Id == FilterRecipient.Id);
            if (FilterMix != null)
                query = query.Where(p => p.CoffeeSale_Details
                    .FirstOrDefault(x => x.Mix.Id == FilterMix.Id) != null);

            Tabs = new ObservableCollection<CoffeeSale>(query);
        }

        private ObservableCollection<CoffeeSale> _tabs;

        public ObservableCollection<CoffeeSale> Tabs
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

        public ICommand cmdFilterUnpaid { get; private set; }

        public void cmdFilterUnpaid_Execute()
        {
            Tabs = new ObservableCollection<CoffeeSale>(
                ContextManager.Context.CoffeeSales.Where(p => !p.Paid));
        }

        protected override async void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as CoffeeSale;

            var messageDialogResult = await DialogCoordinator.Instance.ShowMessageAsync(this, "Подтверждение",
                "Вы уверены, что хотите удалить продажу кофе " + selected.Recipient.Name + " за " + selected.Date.Date +
                " число?",
                MessageDialogStyle.AffirmativeAndNegative);
            if (messageDialogResult == MessageDialogResult.Negative) return;

            try
            {
                ContextManager.Context.CoffeeSales.Remove(selected);
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", ex.Message,
                    MessageDialogStyle.Affirmative,
                    new MetroDialogSettings {ColorScheme = MetroDialogColorScheme.Accented});
            }

            SaveContext();
            Refresh();
        }
    }
}
