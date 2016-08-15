using System.Collections.ObjectModel;
using System.Linq;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmOtherProfits: aProcessListingViewModel
    {
        public vmOtherProfits()
        {
            Header = "Прочие доходы";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.OtherProfits.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterAccount != null)
                query = query.Where(p => p.Account.Id == FilterAccount.Id);

            Tabs = new ObservableCollection<OtherProfit>(query);
        }

        private ObservableCollection<OtherProfit> _tabs;
        public ObservableCollection<OtherProfit> Tabs
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

        protected override async void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as OtherProfit;

            var messageDialogResult = await DialogCoordinator.Instance.ShowMessageAsync(this, "Подтверждение",
                    "Вы уверены, что хотите удалить доход " + selected.Designation + " за " + selected.Date.Date + " число?",
                MessageDialogStyle.AffirmativeAndNegative);
            if (messageDialogResult == MessageDialogResult.Negative) return;

            ContextManager.Context.OtherProfits.Remove(selected);
            SaveContext();
            Refresh();

        }
    }
}
