using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;
using System;
using System.Windows;

namespace ViewModels.Windows
{
    public class vmWinFirstLaunch
    {
        public vmWinFirstLaunch()
        {
            cmdGo = new Command(cmdGo_Execute);
            AccountName = "Наличка";
            ShrinkagePercent = 20;
            CoffeeSorts = new ObservableCollection<hCoffeeSort>();
        }

        public string AccountName { get; set; }

        public int ShrinkagePercent
        {
            get { return Properties.ShrinkagePercent; }
            set { Properties.ShrinkagePercent = value; }
        }

        public double AccountBalance { get; set; }
        public ObservableCollection<hCoffeeSort> CoffeeSorts { get; set; }

        public ICommand cmdGo { get; set; }

        private void cmdGo_Execute(object argWindow)
        {
            try
            {
                if (string.IsNullOrEmpty(AccountName) || AccountBalance < 0)
                {
                    DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", "Заполните все поля",
                        MessageDialogStyle.Affirmative,
                        new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
                    return;
                }

                ContextManager.Context.Accounts.Add(new Account { Name = AccountName });
                ContextManager.Context.SaveChanges();

                ContextManager.Context.dAccountsBalances.First(p => p.Account.Name == AccountName).Balance = AccountBalance;
                ContextManager.Context.SaveChanges();

                if (CoffeeSorts.Count == 0
                    || CoffeeSorts.Count == 1
                        && CoffeeSorts[0].Name == ""
                        && CoffeeSorts[0].Name == null
                        && CoffeeSorts[0].Cost == 0
                        && CoffeeSorts[0].GreenStocks == 0
                        && CoffeeSorts[0].RoastedStocks == 0)
                {
                    Properties.FirstLaunch = false;
                    var window1 = argWindow as Window;
                    window1?.Close();
                    return;
                }

                foreach (var coffeeSort in CoffeeSorts)
                {
                    if (string.IsNullOrEmpty(coffeeSort.Name)
                        || coffeeSort.Cost < 0
                        || coffeeSort.GreenStocks < 0
                        || coffeeSort.RoastedStocks < 0)
                    {
                        DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", "Заполните все поля таблицы",
                            MessageDialogStyle.Affirmative,
                            new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
                        return;
                    }
                    ContextManager.Context.CoffeeSorts.Add(new CoffeeSort { Name = coffeeSort.Name });
                    ContextManager.Context.SaveChanges();

                    var dGreenStock = ContextManager.Context.dGreenStocks.First(p => p.CoffeeSort.Name == coffeeSort.Name);
                    dGreenStock.Quantity = coffeeSort.GreenStocks;
                    dGreenStock.dCost = coffeeSort.Cost;

                    var dRoastedStock = ContextManager.Context.dRoastedStocks.First(p => p.CoffeeSort.Name == coffeeSort.Name);
                    dRoastedStock.Quantity = coffeeSort.RoastedStocks;
                    dRoastedStock.dCost = coffeeSort.Cost * 100 /(100 - ShrinkagePercent );

                    ContextManager.Context.SaveChanges();
                }

                Properties.FirstLaunch = false;
                var window = argWindow as Window;
                window.Close();
            }
            catch (Exception ex)
            {
                DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", ex.Message,
                    MessageDialogStyle.Affirmative,
                    new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
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
