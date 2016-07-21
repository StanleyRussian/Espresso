using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using ViewModels.Statistics.Abstract;

namespace ViewModels.Statistics.CoffeePurchases
{
    public class IndividualCoffeePurchaseViewModel:aTabViewModel
    {
        public IndividualCoffeePurchaseViewModel(CoffeePurchase purchase)
        {
            Purchase = purchase;
            Header = purchase.Date + " - " + purchase.Supplier.Name;
        }

        public CoffeePurchase Purchase { get; }
        public ObservableCollection<CoffeePurchase_Details> Details { get; private set; }

        protected override void Load()
        {
            Details = new ObservableCollection<CoffeePurchase_Details>(Purchase.CoffeePurchase_Details);
        }
    }
}
