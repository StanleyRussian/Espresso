using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;

namespace ViewModels.Windows
{
    public class vmWinUnpaidSales
    {
        public vmWinUnpaidSales()
        {
            UnpaidSales = new ObservableCollection<CoffeeSale>(
                ContextManager.Context.CoffeeSales.Where(p => !p.Paid));
            cmdPay = new Command(cmdPay_Execute);
        }

        public ObservableCollection<CoffeeSale> UnpaidSales { get; private set; }

        public ICommand cmdPay { get; private set; }
        private void cmdPay_Execute(object arg)
        {
            if (arg == null) return;
            CoffeePurchase purchase = arg as CoffeePurchase;
            purchase.Paid = true;
            ContextManager.Context.SaveChanges();
        }

    }
}
