using System.Collections.ObjectModel;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace ViewModels.Windows
{
    public class vmWinFirstLaunch
    {
        private ContextContainer _context = ContextManager.Context;

        public vmWinFirstLaunch()
        {
            cmdGo = new Command(cmdGo_Execute);

            CoffeeSorts = new ObservableCollection<hCoffeeSort>();
            Accounts = new ObservableCollection<hAccount>();
            Packed = new ObservableCollection<hPacked>();
            Packages = new ObservableCollection<hPackage>();
            Products = new ObservableCollection<hProduct>();
            Mixes = new ObservableCollection<hMix>();
        }

        public ObservableCollection<hCoffeeSort> CoffeeSorts { get; set; }
        public ObservableCollection<hAccount> Accounts { get; set; }
        public ObservableCollection<hPacked> Packed { get; set; }
        public ObservableCollection<hPackage> Packages { get; set; }
        public ObservableCollection<hProduct> Products { get; set; }
        public ObservableCollection<hMix> Mixes { get; set; } 

        public ICommand cmdGo { get; set; }
        private void cmdGo_Execute(object argWindow)
        {
            try
            {
                Validate();
            }
            catch (Exception ex)
            {
                DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", ex.Message,
                    MessageDialogStyle.Affirmative,
                    new MetroDialogSettings
                    {
                        ColorScheme = MetroDialogColorScheme.Accented
                    });
            }

            AddAccounts();
            AddCoffee();
            AddMixes();
            AddPackages();
            AddPacked();
            AddProducts();

            try
            {
                _context.SaveChanges();

                Properties.FirstLaunch = false;
                var window = argWindow as Window;
                window?.Close();
            }
            catch (Exception ex)
            {
                DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", ex.Message,
                    MessageDialogStyle.Affirmative,
                    new MetroDialogSettings
                    {
                        ColorScheme = MetroDialogColorScheme.Accented
                    });
            }
        }

        private void Validate()
        {
            foreach (var account in Accounts)
            {
                if (string.IsNullOrEmpty(account.Name))
                    throw new Exception("Введите название счёта");
                if (account.Balance < 0)
                    throw new Exception("Баланс на счету не может быть меньше нуля");
            }

            foreach (var coffeeSort in CoffeeSorts)
            {
                if (string.IsNullOrEmpty(coffeeSort.Name))
                    throw new Exception("Название сорта не может быть пустым");
                if (coffeeSort.Cost < 0)
                    throw new Exception("Как цена может быть меньше нуля???");
                if (coffeeSort.GreenStocks < 0 || coffeeSort.RoastedStocks < 0)
                    throw new Exception("Серьёзно?");
                if (coffeeSort.ShrinkagePercent < 0 || coffeeSort.ShrinkagePercent > 100)
                    throw new Exception("Процент ужарки болжен быть от 0 до 100");
            }

            foreach (var package in Packages)
            {
                if (string.IsNullOrEmpty(package.Name))
                    throw new Exception("Введите название упаковки");
                if (package.Capacity <= 0)
                    throw new Exception("Ёмкость упаковки не может равняться нулю");
                if (package.Cost <0)
                    throw new Exception("Себестоимость упаковки не может быть меньше нуля");
                if (package.Stocks < 0)
                    throw new Exception("Остатки упаковки не могут быть меньше нуля");
            }

            foreach (var packed in Packed)
            {
                if (packed.Cost < 0)
                    throw new Exception("Себестоимость упакованного кофе не может быть меньше нуля");
                if (packed.Stocks < 0) 
                    throw new Exception("Остатки упакованного кофе не могут быть меньше нуля");
                if (packed.Mix == null)
                    throw new Exception("Введите купаж");
                if (packed.Package == null)
                    throw new Exception("Введите упаковку");
            }

            foreach (var product in Products)
            {
                if (string.IsNullOrEmpty(product.Name))
                    throw new Exception("Введите наименование товара");
                if (product.Cost<0)
                    throw new Exception("Себестоимость товаров не может быть меньше нуля");
                if (product.Stocks<0)
                    throw new Exception("Остатки товаров не могут быть меньше нуля");
            }
        }

        private void AddCoffee()
        {
            foreach (var coffeeSort in CoffeeSorts)
            {
                var add = _context.CoffeeSorts.Add(new CoffeeSort {Name = coffeeSort.Name});
                _context.dCoffeeStocks.Add(new dCoffeeStock
                {
                    CoffeeSort = add,
                    GreenCost = coffeeSort.Cost,
                    GreenQuantity = coffeeSort.GreenStocks,
                    RoastedQuantity = coffeeSort.RoastedStocks,
                    RoastedCost = Math.Round(coffeeSort.Cost*100/(100 - coffeeSort.ShrinkagePercent), 2)
                });
            }
        }

        private void AddAccounts()
        {
            foreach (var account in Accounts)
            {
                var add = _context.Accounts.Add(new Account {Name = account.Name});
                _context.dAccountsBalances.Add(new dAccountsBalance
                {
                    Account = add,
                    Balance = account.Balance
                });
            }
        }

        private void AddPackages()
        {
            foreach (var package in Packages)
            {
                var add = _context.Packages.Add(new Package
                {
                    Name = package.Name,
                    Capacity = package.Capacity
                });
                _context.dPackageStocks.Add(new dPackageStocks
                {
                    Package = add,
                    Cost = package.Cost,
                    Quantity = package.Stocks
                });
            }
        }

        private void AddMixes()
        {
            foreach (var mix in Mixes)
            {
                var add = _context.Mixes.Add(new Mix {Name = mix.Name});
                add.Mix_Details = new List<Mix_Details>();
                foreach (var detail in mix.Details)
                {
                    add.Mix_Details.Add(new Mix_Details
                    {
                        CoffeeSort = _context.CoffeeSorts.Local.First(p=>p.Name == detail.CoffeeSort.Name),
                        Ratio = detail.Ratio,
                        Mix = add
                    });
                }
            }
        }

        private void AddPacked()
        {
            foreach (var packed in Packed)
            {
                _context.dPackedStocks.Add(new dPackedStocks
                {
                    Mix = _context.Mixes.Local.First(p => p.Name == packed.Mix.Name),
                    Package = _context.Packages.Local.First(p => p.Name == packed.Package.Name),
                    Cost = packed.Cost,
                    Quantity = packed.Stocks
                });
            }
        }

        private void AddProducts()
        {
            foreach (var product in Products)
            {
                var add = _context.Products.Add(new Product {Name = product.Name});
                _context.dProductStocks.Add(new dProductStock
                {
                    Product = add,
                    Cost = product.Cost,
                    Quantity = product.Stocks
                });
            }
        }
    }

    public class hAccount
    {
        public string Name { get; set; }
        public double Balance { get; set; }
    }

    public class hCoffeeSort
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public double GreenStocks { get; set; }
        public double RoastedStocks { get; set; }
        public double ShrinkagePercent { get; set; }
    }

    public class hPackage
    {
        public string Name { get; set; }
        public double Capacity { get; set; }
        public double Cost { get; set; }
        public int Stocks { get; set; }
    }

    public class hPacked
    {
        public hMix Mix { get; set; }
        public hPackage Package { get; set; }
        public double Cost { get; set; }
        public int Stocks { get; set; }
    }

    public class hProduct
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public double Stocks { get; set; }
    }

    public class hMix
    {
        public hMix()
        {
            Details = new ObservableCollection<hMixDetail>();
        }
        public string Name { get; set; }
        public ObservableCollection<hMixDetail> Details { get; set; }
    }

    public class hMixDetail
    {
        public hCoffeeSort CoffeeSort { get; set; }
        public int Ratio { get; set; }
    }
}
