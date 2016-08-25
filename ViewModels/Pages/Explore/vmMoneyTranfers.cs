using System;
using System.Collections.ObjectModel;
using System.Linq;
using MahApps.Metro.Controls.Dialogs;
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


        protected override async void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as MoneyTransfer;

            var messageDialogResult = await DialogCoordinator.Instance.ShowMessageAsync(this, "Подтверждение",
                    "Вы уверены, что хотите удалить денежный перевод со счёта " + selected.InitialAccount.Name + 
                    " на счёт: " + selected.TargetAccount.Name + " за " + selected.Date.Date + " число?",
                MessageDialogStyle.AffirmativeAndNegative);
            if (messageDialogResult == MessageDialogResult.Negative) return;

            try
            {

                ContextManager.Context.MoneyTransfers.Remove(selected);
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", ex.Message,
                    MessageDialogStyle.Affirmative,
                    new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
            }
            SaveContext();
            Refresh();

        }
    }
}
