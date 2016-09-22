using System.Collections.ObjectModel;
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
                ContextContainer _context = ContextManager.Context;

                if (string.IsNullOrEmpty(AccountName))
                    throw new Exception("Заполните название основного расчётного счета");
                if (AccountBalance < 0)
                    throw new Exception("Баланс на счету не может быть меньше нуля");

                foreach (var coffeeSort in CoffeeSorts)
                {
                    if (string.IsNullOrEmpty(coffeeSort.Name))
                        throw new Exception("Название сорта не может быть пустым");
                    if (coffeeSort.Cost < 0)
                        throw new Exception("Как цена может быть меньше нуля???");
                    if (coffeeSort.GreenStocks < 0 || coffeeSort.RoastedStocks < 0)
                        throw new Exception("Серьёзно?");
                }

                var newAccount = _context.Accounts.Add(new Account { Name = AccountName });
                _context.dAccountsBalances.Add(new dAccountsBalance
                {
                    Account = newAccount,
                    Balance = AccountBalance
                });

                if (CoffeeSorts.Count == 0)
                {
                    _context.SaveChanges();
                    Properties.FirstLaunch = false;
                    var window1 = argWindow as Window;
                    window1?.Close();
                    return;
                }

                foreach (var coffeeSort in CoffeeSorts)
                {
                    var newSort = _context.CoffeeSorts.Add(new CoffeeSort
                    {
                        Name = coffeeSort.Name,
                        MinGreenStocks = coffeeSort.MinGreenStocks,
                        MinRoastedStocks = coffeeSort.MinRoastedStocks
                    });

                    _context.dCoffeeStocks.Add(new dCoffeeStock
                    {
                        CoffeeSort = newSort,
                        GreenQuantity = coffeeSort.GreenStocks,
                        GreenCost = coffeeSort.Cost,
                        RoastedQuantity = coffeeSort.RoastedStocks,
                        RoastedCost = Math.Round(coffeeSort.Cost*100/(100 - ShrinkagePercent), 2)
                    });
                }

                _context.SaveChanges();

                Properties.FirstLaunch = false;
                var window = argWindow as Window;
                window?.Close();
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
        public double MinGreenStocks { get; set; }
        public double MinRoastedStocks { get; set; }
    }
}
