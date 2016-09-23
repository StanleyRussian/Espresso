using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using Model;
using Model.Entity;
using ViewModels.Annotations;
using ViewModels.Auxiliary;

namespace ViewModels.Pages.Statistics
{
    public class vmStatsCoffeePurchases:aTabViewModel
    {
        public vmStatsCoffeePurchases()
        {
            Header = "Сорта кофе и закупки";
            cmdRowSelected = new Command(cmdRowSelected_Execute);
        }

        protected override void Load()
        {
            CoffeeStocks = new ObservableCollection<wrapCoffeeStock>();

            var stocks = new ObservableCollection<dCoffeeStock>(
                ContextManager.Context.dCoffeeStocks.Where(
                    p => p.CoffeeSort.Active).Include(p => p.CoffeeSort));

            foreach (var coffeeStock in stocks)
                CoffeeStocks.Add(new wrapCoffeeStock(coffeeStock));

            cmdRowSelected.Execute(CoffeeStocks[0].CoffeeStock.CoffeeSort);
        }

        private ObservableCollection<wrapCoffeeStock> _coffeeStocks;
        private SeriesCollection _chartData;

        public ObservableCollection<wrapCoffeeStock> CoffeeStocks
        {
            get { return _coffeeStocks; }
            private set
            {
                _coffeeStocks = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection ChartData
        {
            get { return _chartData; }
            set
            {
                _chartData = value;
                OnPropertyChanged();
            }
        }

        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public ICommand cmdRowSelected { get; private set; }
        private void cmdRowSelected_Execute(object argSelected)
        {
            try
            {
                var sort = argSelected as CoffeeSort;
                Labels = new List<string>();
                DateTime yearAgo = DateTime.Today.AddDays(-365);

                var purchases = ContextManager.Context.CoffeePurchaseDetails.Where(
                    p => p.CoffeeSort.Id == sort.Id &&
                         p.CoffeePurchase.Date >= yearAgo).OrderBy(p => p.CoffeePurchase.Date);
                if (!purchases.Any()) return;

                ChartValues<double> prices = new ChartValues<double>();

                foreach (var purchase in purchases)
                {
                    prices.Add(purchase.Price);
                    Labels.Add(purchase.CoffeePurchase.Date.ToShortDateString());
                }

                ChartData = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = sort.Name,
                        DataLabels = true,
                        LabelPoint = point => point.Y + "грн",
                        Values = prices,
                        Stroke = Brushes.Green
                    }
                };

                YFormatter = value => value + "грн";
            }
            // WPF or smth throws exception when switch tabs while has something selected in DataGrid
            // It appears harmless, so I supress it with this catch
            // Would be great to figure out wtf is it
            catch(Exception ex) { }
        }
    }





    public class wrapCoffeeStock:INotifyPropertyChanged
    {
        public wrapCoffeeStock(dCoffeeStock argStock)
        {
            CoffeeStock = argStock;
            CoffeeSort = ContextManager.Context.CoffeeSorts.Find(CoffeeStock.CoffeeSort.Id);
            cmdPurchaseStep = new Command(cmdPurchaseStep_Execute);
        }

        public dCoffeeStock CoffeeStock { get; }
        public CoffeeSort CoffeeSort { get; }

        public string LastPurchaseStep
        {
            get { return CoffeeSort.LastPurchaseStep; }
            set
            {
                CoffeeSort.LastPurchaseStep = value;
                OnPropertyChanged();
            }
        }

        public ICommand cmdPurchaseStep { get; private set; }
        private void cmdPurchaseStep_Execute()
        {
            // Querying all coffee purchases for last year for current coffee sort
            DateTime yearAgo = DateTime.Today.AddDays(-365);
            var purchases = ContextManager.Context.CoffeePurchaseDetails.Where(
                p => p.CoffeeSort.Id == CoffeeSort.Id &&
                     p.CoffeePurchase.Date >= yearAgo).OrderBy(p => p.CoffeePurchase.Date);
            if (!purchases.Any()) return;

            // Finding all months with purchases
            List<string> months = new List<string>();
            foreach (CoffeePurchaseDetails purchase in purchases)
            {
                string month = purchase.CoffeePurchase.Date.Month.ToString();
                if (!months.Contains(month))
                    months.Add(month);
            }

            if (months.Count >= purchases.Count())
                LastPurchaseStep = "Раз в " + months.Count / purchases.Count() + " месяц(а)";
            else
                LastPurchaseStep = "Раз в " + months.Count * 30 / purchases.Count() + " дней";

            ContextManager.Context.SaveChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
