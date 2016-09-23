using System;
using System.Collections.ObjectModel;
using System.Linq;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmPackings : aProcessListingViewModel
    {
        public vmPackings()
        {
            Header = "Фасовки";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.Packings.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterMix != null)
                query = query.Where(p => p.Mix.Id == FilterMix.Id);
            if (FilterPackage != null)
                query = query.Where(p => p.Package.Id == FilterPackage.Id);
            Tabs = new ObservableCollection<Packing>(query);
        }

        private ObservableCollection<Packing> _tabs;

        public ObservableCollection<Packing> Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        private Package _filterPackage;

        public Package FilterPackage
        {
            get { return _filterPackage; }
            set
            {
                _filterPackage = value;
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

        protected override async void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Packing;

            // Checking if there would be enought packed coffee left if user deletes selected Packing
            //if (ContextManager.Context.SaleDetailsCoffee.Any(p => p.Mix.Id == selected.Mix.Id))
            {
                if (ContextManager.Context.dPackedStocks.First(
                    p => p.Mix.Id == selected.Mix.Id &&
                         p.Package.Id == selected.Package.Id)
                    .Quantity - selected.Quantity < 0)
                {
                    await DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка",
                        "Вы не можете удалить эту фасовку, недостаточно остатков фасованного кофе",
                        MessageDialogStyle.Affirmative,
                        new MetroDialogSettings {ColorScheme = MetroDialogColorScheme.Accented});
                    return;
                }
            }

            var messageDialogResult = await DialogCoordinator.Instance.ShowMessageAsync(this, "Подтверждение",
                "Вы уверены, что хотите удалить фасовку купажа " + selected.Mix.Name + " за " + selected.Date.Date +
                " число?",
                MessageDialogStyle.AffirmativeAndNegative);
            if (messageDialogResult == MessageDialogResult.Negative) return;

            try
            {
                ContextManager.Context.Packings.Remove(selected);
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
