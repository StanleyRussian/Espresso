using System;
using System.Collections.ObjectModel;
using System.Linq;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmRoastings : aProcessListingViewModel
    {
        public vmRoastings()
        {
            Header = "Обжарки";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.Roastings.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterCoffeeSort != null)
                query = query.Where(p => p.CoffeeSort.Id == FilterCoffeeSort.Id);
            Tabs = new ObservableCollection<Roasting>(query);
        }

        private ObservableCollection<Roasting> _tabs;

        public ObservableCollection<Roasting> Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        private CoffeeSort _filterCoffeeSort;

        public CoffeeSort FilterCoffeeSort
        {
            get { return _filterCoffeeSort; }
            set
            {
                _filterCoffeeSort = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        protected override async void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Roasting;

            // Checking if there would be enought roasted coffee left if user deletes selected Roasting
            //if (ContextManager.Context.Packings.Any(
            //        p => p.Mix.Mix_Details.Any(
            //            t => t.CoffeeSort.Id == selected.CoffeeSort.Id)))
            {
                if (ContextManager.Context.dRoastedStocks.First(
                    p => p.CoffeeSort.Id == selected.CoffeeSort.Id)
                    .Quantity - selected.RoastedAmount < 0)
                {
                    await DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка",
                        "Вы не можете удалить эту обжарку, недостаточно остатков обжаренного кофе",
                        MessageDialogStyle.Affirmative,
                        new MetroDialogSettings {ColorScheme = MetroDialogColorScheme.Accented});
                    return;

                }
            }

            var messageDialogResult = await DialogCoordinator.Instance.ShowMessageAsync(this, "Подтверждение",
                "Вы уверены, что хотите удалить обжарку кофе " + selected.CoffeeSort.Name + " за " + selected.Date.Date +
                " число?",
                MessageDialogStyle.AffirmativeAndNegative);
            if (messageDialogResult == MessageDialogResult.Negative) return;

            try
            {
                ContextManager.Context.Roastings.Remove(selected);
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
