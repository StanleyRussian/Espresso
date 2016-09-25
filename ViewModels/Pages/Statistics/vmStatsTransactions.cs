using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;

namespace ViewModels.Pages.Statistics
{
    public class vmStatsTransactions: aTabViewModel
    {
        public vmStatsTransactions()
        {
            Header = "По финансам";

            cmdFilter30Days = new Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Command(cmdFilterAll_Execute);
            cmdFilterCurrentMonth = new Command(cmdFilterCurrentMonth_Execute);
            cmdFilter = new Command(cmdFilter_Execute);
        }

        protected override void Load()
        {
            Income = new ObservableCollection<dTransaction>();
            Outcome = new ObservableCollection<dTransaction>();
            UnpaidIncome = new ObservableCollection<unpaidOperation>();
            UnpaidOutcome = new ObservableCollection<unpaidOperation>();

            cmdFilterCurrentMonth.Execute(null);
        }

        private void Refresh()
        {
            Income.Clear();
            Outcome.Clear();
            UnpaidIncome.Clear();
            UnpaidOutcome.Clear();

            var queryTransactions = ContextManager.Context.dTransactions.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            foreach (var transaction in queryTransactions)
            {
                if (transaction.Sum>0)
                    Income.Add(transaction);
                else
                    Outcome.Add(transaction);
            }

            var querySales = ContextManager.Context.Sales.Where(
                p => p.Date >= _filterFrom && p.Date <= _filterTo && p.Paid == false);
            foreach (var sale in querySales)
            {
                double sum = sale.SaleDetailsCoffee.Sum(detailCoffee => detailCoffee.Price*detailCoffee.Quantity) + 
                    sale.SaleDetailsProducts.Sum(detailProduct => detailProduct.Price*detailProduct.Quantity);

                UnpaidIncome.Add(new unpaidOperation
                {
                    Date = sale.Date,
                    Sum = sum,
                    Description = "Неоплаченная продажа " + sale.Recipient.Name + " от " + sale.Date + " числа"
                });
            }

            var queryPurchases = ContextManager.Context.CoffeePurchases.Where(
                p => p.Date >= _filterFrom && p.Date <= _filterTo && p.Paid == false);
            foreach (var purchase in queryPurchases)
            {
                double sum = purchase.CoffeePurchaseDetails.Sum(detailCoffee => detailCoffee.Price*detailCoffee.Quantity);
                UnpaidOutcome.Add(new unpaidOperation
                {
                    Date = purchase.Date,
                    Sum = sum,
                    Description = "Неоплаченная закупка кофе от " + purchase.Date + " числа"
                });
            }
        }

        public ObservableCollection<dTransaction> Income { get; private set; }
        public ObservableCollection<dTransaction> Outcome { get; private set; }
        public ObservableCollection<unpaidOperation> UnpaidIncome { get; private set; }
        public ObservableCollection<unpaidOperation> UnpaidOutcome { get; private set; }

        public double TotalIncome => Income.Sum(p => p.Sum);
        public double TotalOutcome => Outcome.Sum(p => p.Sum);
        public double TotalUnpaidIncome => UnpaidIncome.Sum(p => p.Sum);
        public double TotalUnpaidOutcome => UnpaidOutcome.Sum(p => p.Sum);
        public double TotalPaid => TotalIncome - TotalOutcome;
        public double Total => TotalPaid + TotalUnpaidIncome - TotalUnpaidOutcome;

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


    public class unpaidOperation
    {
        public DateTime Date { get; set; }
        public double Sum { get; set; }
        public string Description { get; set; }
    }
}
