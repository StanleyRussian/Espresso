using System;
using System.Collections.ObjectModel;
using System.Linq;
using MahApps.Metro.Controls.Dialogs;
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


        protected override async void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as CoffeeTransfer;

            var messageDialogResult = await DialogCoordinator.Instance.ShowMessageAsync(this, "Подтверждение",
                    "Вы уверены, что хотите удалить перевод кофе за " + selected.Date.Date + " число?",
                MessageDialogStyle.AffirmativeAndNegative);
            if (messageDialogResult == MessageDialogResult.Negative) return;

            try
            {
                ContextManager.Context.CoffeeTransfers.Remove(selected);
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
