using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;

namespace ViewModels.Windows
{
    public class vmWinFirstLaunch
    {
        public vmWinFirstLaunch()
        {
            cmdGo = new Command(cmdGo_Execute);
            CoffeeSorts = new ObservableCollection<hCoffeeSort>();
        }

        public string AccountName { get; set; }
        public double AccountBalance { get; set; }
        public ObservableCollection<hCoffeeSort> CoffeeSorts { get; set; }
        public ICommand cmdGo { get; set; }

        private void cmdGo_Execute()
        {
            if (AccountName == "" || AccountBalance < 0) 
            {
                DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", "Заполните все поля",
                    MessageDialogStyle.Affirmative,
                    new MetroDialogSettings {ColorScheme = MetroDialogColorScheme.Accented});
                return;
            }

            ContextManager.Context.Accounts.Add(new Account {Name = AccountName});
            ContextManager.Context.dAccountsBalances.First(p => p.Account.Name == AccountName).Balance = AccountBalance;
            foreach (var coffeeSort in CoffeeSorts)
            {
                if (coffeeSort.Name == "" || 
                    coffeeSort.Cost < 0 || 
                    coffeeSort.GreenStocks < 0 || 
                    coffeeSort.RoastedStocks < 0) 
                {
                    DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", "Заполните все поля таблицы",
                        MessageDialogStyle.Affirmative,
                        new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
                    return;
                }
                ContextManager.Context.CoffeeSorts.Add(new CoffeeSort {Name = coffeeSort.Name});
                var dGreenStock = ContextManager.Context.dGreenStocks.First(p => p.CoffeeSort.Name == coffeeSort.Name);
                dGreenStock.Quantity = coffeeSort.GreenStocks;
                dGreenStock.dCost = coffeeSort.Cost;

                var dRoastedStock = ContextManager.Context.dRoastedStocks.First(p => p.CoffeeSort.Name == coffeeSort.Name);
                dRoastedStock.Quantity = coffeeSort.RoastedStocks;
                dRoastedStock.dCost = coffeeSort.Cost;

                Properties.FirstLaunch = false;
            }
        }
    }

    public class hCoffeeSort
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public double GreenStocks { get; set; }
        public double RoastedStocks { get; set; }
    }
}
