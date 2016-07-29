using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;
using ViewModels.Properties;

namespace ViewModels.Pages.Statistics.CoffeeSorts
{
    public class vmSoloCoffeeSort: INotifyPropertyChanged
    {
        public vmSoloCoffeeSort(CoffeeSort sort)
        {
            Sort = sort;
            cmdPurchaseStep = new Command(cmdPurchaseStep_Execute);

            GreenStocks = ContextManager.Context.dGreenStocks.First(p => p.CoffeeSort.Id == Sort.Id).Quantity;
            RoastedStocks = ContextManager.Context.dRoastedStocks.First(p => p.CoffeeSort.Id == Sort.Id).Quantity;

            PurchaseStepMessage = Sort.dLastPurchaseStep ?? "Никогда не вычислялся";

        }

        public CoffeeSort Sort { get; }
        public double GreenStocks { get; private set; }
        public double RoastedStocks { get; private set; }

        private string _purchaseStepMessage;
        public string PurchaseStepMessage
        {
            get { return _purchaseStepMessage; }
            private set
            {
                _purchaseStepMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand cmdPurchaseStep { get; private set; }
        private void cmdPurchaseStep_Execute()
        {
            // Querying all coffee purchases for last year for current coffee sort
            DateTime yearAgo = DateTime.Today.AddDays(-365);
            var query = ContextManager.Context.CoffeePurchases.Where(p =>
                p.Date >= yearAgo &&
                p.CoffeePurchase_Details.FirstOrDefault(x => x.CoffeeSort.Id == Sort.Id) != null);

            if (!query.Any()) return;

            // Finding all months with purchases
            List<string> months = new List<string>();
            foreach (CoffeePurchase purchase in query)
            {
                string month = purchase.Date.Month.ToString();
                if (!months.Contains(month))
                    months.Add(month);
            }
            if (months.Count >= query.Count())
                PurchaseStepMessage = "Раз в " + months.Count/query.Count() + " месяц(а)";
            else
                PurchaseStepMessage = "Раз в " + months.Count*30/query.Count() + " дней";

            Sort.dLastPurchaseStep = PurchaseStepMessage;
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
