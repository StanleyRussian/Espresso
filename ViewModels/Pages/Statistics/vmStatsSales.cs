using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Model;
using Model.Entity;
using ViewModels.Annotations;
using ViewModels.Auxiliary;

namespace ViewModels.Pages.Statistics
{
    public enum Month
    {
        Январь = 1,
        Февраль,
        Март,
        Апрель,
        Май,
        Июнь,
        Июль,
        Август,
        Сентябрь,
        Октябрь,
        Ноябрь,
        Декабрь
    }

    public class vmStatsSales:aTabViewModel
    {
        public vmStatsSales()
        {
            Header = "Клиенты и продажи";
            cmdFilter30Days = new Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Command(cmdFilterAll_Execute);
            cmdFilterCurrentMonth = new Command(cmdFilterCurrentMonth_Execute);
            cmdFilter = new Command(cmdFilter_Execute);
        }

        protected override void Load()
        {
            cmdFilterCurrentMonth.Execute(null);
            Refresh();
        }

        private void Refresh()
        {
            Recipients = new ObservableCollection<wrapRecipient>();

            var query = ContextManager.Context.Sales.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            foreach (var sale in query)
            {
                var quantity = 0d;
                var profit = 0d;
                var turnover = 0d;

                foreach (var detailCoffee in sale.SaleDetailsCoffee)
                {
                    quantity += detailCoffee.Quantity * detailCoffee.Package.Capacity;
                    profit += detailCoffee.Price - detailCoffee.Cost;
                    turnover += detailCoffee.Price;
                }

                var recipient = Recipients.FirstOrDefault(p => p.Recipient.Id == sale.Recipient.Id);
                if (recipient == null)
                {
                    recipient = new wrapRecipient
                    {
                        Recipient = sale.Recipient,
                        Sales = new ObservableCollection<wrapSale>()
                    };
                    Recipients.Add(recipient);
                }

                recipient.Quantity += quantity;
                recipient.Profit += profit;
                recipient.Turnover += turnover;
                recipient.Sales.Add(new wrapSale {Sale = sale});
            }

            // By some odd reason without this pie charts does not update
            OnPropertyChanged("Recipients");
        }

        public ObservableCollection<wrapRecipient> Recipients { get; private set; }

        private DateTime _filterFrom;
        public DateTime FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                OnPropertyChanged();
                _filterMonth = null;
                OnPropertyChanged("FilterMonth");
                _filterYear = value.Year;
                OnPropertyChanged("FilterYear");
            }
        }

        private DateTime _filterTo;
        public DateTime FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                OnPropertyChanged();
                _filterMonth = null;
                OnPropertyChanged("FilterMonth");
                _filterYear = value.Year;
                OnPropertyChanged("FilterYear");
            }
        }

        private Month? _filterMonth;
        public Month? FilterMonth
        {
            get { return _filterMonth; }
            set
            {
                _filterMonth = value;
                OnPropertyChanged();

                _filterTo = new DateTime(FilterYear, FilterMonth.GetHashCode(),
                    DateTime.DaysInMonth(FilterYear, FilterMonth.GetHashCode()));
                OnPropertyChanged("FilterTo");

                _filterFrom = new DateTime(FilterYear, FilterMonth.GetHashCode(), 1);
                OnPropertyChanged("FilterFrom");

                Refresh();
            }
        }

        private int _filterYear;
        public int FilterYear
        {
            get { return _filterYear; }
            set
            {
                _filterYear = value;
                OnPropertyChanged();
                _filterFrom = new DateTime(value, _filterFrom.Month, _filterFrom.Day);
                OnPropertyChanged("FilterFrom");
                _filterTo = new DateTime(value, _filterTo.Month, _filterTo.Day);
                OnPropertyChanged("FilterTo");
                Refresh();
            }
        }

        private wrapRecipient _filterRecipient;
        public wrapRecipient FilterRecipient
        {
            get { return _filterRecipient; }
            set
            {
                _filterRecipient = value;
                OnPropertyChanged();
            }
        }

        public ICommand cmdFilter30Days { get; }
        private void cmdFilter30Days_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged("FilterTo");
            _filterFrom = DateTime.Now.AddDays(-30);
            OnPropertyChanged("FilterFrom");
            _filterMonth = null;
            OnPropertyChanged("FilterMonth");
            _filterYear = DateTime.Now.Year;
            OnPropertyChanged("FilterYear");
            Refresh();
        }

        public ICommand cmdFilterAll { get; }
        private void cmdFilterAll_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged("FilterTo");
            _filterFrom = FilterTo.AddDays(-365);
            OnPropertyChanged("FilterFrom");
            _filterMonth = null;
            OnPropertyChanged("FilterMonth");
            _filterYear = DateTime.Now.Year;
            OnPropertyChanged("FilterYear");
            Refresh();
        }

        public ICommand cmdFilterCurrentMonth { get; }
        private void cmdFilterCurrentMonth_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged("FilterTo");
            _filterFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            OnPropertyChanged("FilterFrom");
            _filterMonth = (Month)DateTime.Now.Month;
            OnPropertyChanged("FilterMonth");
            _filterYear = DateTime.Now.Year;
            OnPropertyChanged("FilterYear");
            Refresh();
        }

        public ICommand cmdFilter { get; }
        private void cmdFilter_Execute()
        {
            Refresh();
        }
    }


    public class wrapRecipient
    {
        public ObservableCollection<wrapSale> Sales { get; set; }
        public Recipient Recipient { get; set; }
        public double Quantity { get; set; }
        public double Profit { get; set; }
        public double Turnover { get; set; }

        public string Name => Recipient.Name;

        public string PurchaseStep
        {
            get
            {
                if (!Sales.Any()) return null;

                // Finding all months with purchases
                List<string> months = new List<string>();
                foreach (var sale in Sales)
                {
                    string month = sale.Date.Month.ToString();
                    if (!months.Contains(month))
                        months.Add(month);
                }

                if (months.Count >= Sales.Count())
                    return "Раз в " + months.Count / Sales.Count() + " месяц(а)";
                return "Раз в " + months.Count * 30 / Sales.Count() + " дней";
            }
        }

        public string UsualPurchase
        {
            get
            {
                if (!Sales.Any()) return null;

                SortedDictionary<string, int> purchaseDictionary = new SortedDictionary<string, int>();
                foreach (var sale in Sales)
                {
                    foreach (var detailCoffee in sale.Sale.SaleDetailsCoffee)
                    {
                        if (purchaseDictionary.ContainsKey(detailCoffee.Mix.Name))
                            purchaseDictionary[detailCoffee.Mix.Name] += 1;
                        else
                            purchaseDictionary.Add(detailCoffee.Mix.Name, 1);
                    }
                }

                return purchaseDictionary.OrderByDescending(p => p.Value).First().Key;
            }
        }
    }


    public class wrapSale
    {
        public Sale Sale { get; set; }

        public double Sum => Sale.SaleDetailsCoffee.Sum(p => p.Price) + Sale.SaleDetailsProducts.Sum(p => p.Price);
        public double Cost => Sale.SaleDetailsCoffee.Sum(p => p.Cost) + Sale.SaleDetailsProducts.Sum(p => p.Cost);
        public double Profit => Sale.SaleDetailsCoffee.Sum(p => (p.Price - p.Cost)) + Sale.SaleDetailsProducts.Sum(p => (p.Price - p.Cost));

        public DateTime Date => Sale.Date;
    }
}
