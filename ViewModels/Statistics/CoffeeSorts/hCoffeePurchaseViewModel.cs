using System;
using System.Linq;
using Model.Entity;

namespace ViewModels.Statistics.CoffeeSorts
{
    // Helper class to represent coffee purchase 
    // along with quantity of purchased coffee sort
    public class hCoffeePurchaseViewModel
    {
        public hCoffeePurchaseViewModel(CoffeePurchase argPurchase, CoffeeSort argSort)
        {
            Date = argPurchase.Date;
            Supplier = argPurchase.Supplier.Name;

            var purchaseDetails = argPurchase.CoffeePurchase_Details.First(p => p.CoffeeSort.Id == argSort.Id);
            Quantity = purchaseDetails.Quantity;
            Price = purchaseDetails.Price;
            Sum = Quantity*Price;
        }

        public DateTime Date { get; set; }
        public string Supplier { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Sum { get; set; }
    }
}
