using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;
using Model.Entity;
using ViewModels.Annotations;
using ViewModels.Auxiliary;

namespace ViewModels.Windows
{
    public class vmWinStocksCorrection:INotifyPropertyChanged
    {
        public vmWinStocksCorrection()
        {
            CoffeeStocks = new ObservableCollection<wrapCoffeeStock>();
            PackedStocks = new ObservableCollection<wrapPackedStock>();
            PackageStocks = new ObservableCollection<wrapPackageStock>();
            ProductStocks = new ObservableCollection<wrapProductStock>();

            cmdGreenExpanded = new Command(cmdCoffeeExpanded_Execute);
            cmdRoastedExpanded = new Command(cmdCoffeeExpanded_Execute);
            cmdPackageExpanded = new Command(cmdPackageExpanded_Execute);
            cmdPackedExpanded = new Command(cmdPackedExpanded_Execute);
            cmdProductExpanded = new Command(cmdProductExpanded_Execute);
            cmdSave = new Command(cmdSave_Execute);
        }

        public ObservableCollection<wrapCoffeeStock> CoffeeStocks { get; }
        public ObservableCollection<wrapPackageStock> PackageStocks { get; }
        public ObservableCollection<wrapPackedStock> PackedStocks { get; }
        public ObservableCollection<wrapProductStock> ProductStocks { get; }

        public ICommand cmdRoastedExpanded { get; }
        public ICommand cmdGreenExpanded { get; }
        private void cmdCoffeeExpanded_Execute()
        {
            CoffeeStocks.Clear();
            foreach (var coffeeStock in ContextManager.Context.dCoffeeStocks)
                CoffeeStocks.Add(new wrapCoffeeStock { Stock = coffeeStock });
        }

        public ICommand cmdPackageExpanded { get; }
        private void cmdPackageExpanded_Execute()
        {
            PackageStocks.Clear();
            foreach (var packageStock in ContextManager.Context.dPackageStocks)
                PackageStocks.Add(new wrapPackageStock {Stock = packageStock});
        }

        public ICommand cmdPackedExpanded { get; }
        private void cmdPackedExpanded_Execute()
        {
            PackedStocks.Clear();
            foreach (var packedStock in ContextManager.Context.dPackedStocks)
                PackedStocks.Add(new wrapPackedStock {Stock = packedStock});
        }

        public ICommand cmdProductExpanded { get; }
        private void cmdProductExpanded_Execute()
        {
          ProductStocks.Clear();
            foreach (var productStock in ContextManager.Context.dProductStocks)
                ProductStocks.Add(new wrapProductStock {Stock = productStock});
        }

        public ICommand cmdSave { get; }
        private void cmdSave_Execute()
        {
            SaveGreen();
            SaveRoasted();
            SavePackage();
            SavePacked();
            SaveProduct();

            try
            {
                ContextManager.Context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        private void SaveGreen()
        {
            foreach (var stock in CoffeeStocks)
            {
                // If difference is bigger than zero:
                // we add difference to coffee stocks with zero cost
                if (stock.diffGreenStock > 0)
                {
                    // Check if there anything in stock already
                    if (stock.Stock.GreenQuantity == 0)
                        // Set zero cost
                        stock.Stock.GreenCost = 0;
                    else
                        // Count cost based on stock and zero
                        stock.Stock.GreenCost = Math.Round((stock.Stock.GreenQuantity * stock.Stock.GreenCost) /
                                                           (stock.Stock.GreenQuantity + stock.diffGreenStock), 2);
                    stock.Stock.GreenQuantity += stock.diffGreenStock;
                }
                // If difference is less than zero:
                // we add money loss, equal to the cost of difference
                else if (stock.diffGreenStock < 0)
                {
                    ContextManager.Context.dTransactions.Add(new dTransaction
                    {
                        Date = DateTime.Now,
                        Sum = stock.diffGreenStock * stock.Stock.GreenCost,
                        Description = "Списание" + stock.diffGreenStock + " кг зелёного кофе, сорта: " + stock.Name,
                        Account = ContextManager.Context.Accounts.First(),
                        Participant = ""
                    });

                    stock.Stock.GreenQuantity += stock.diffGreenStock;
                }
            }
        }

        private void SaveRoasted()
        {
            foreach (var stock in CoffeeStocks)
            {
                if (stock.diffRoastedStock > 0)
                {
                    if (stock.Stock.RoastedQuantity == 0)
                        stock.Stock.RoastedCost = 0;
                    else
                        stock.Stock.RoastedCost = Math.Round((stock.Stock.RoastedQuantity * stock.Stock.RoastedCost) /
                                                           (stock.Stock.RoastedQuantity + stock.diffRoastedStock), 2);
                    stock.Stock.RoastedQuantity += stock.diffRoastedStock;
                }
                else if (stock.diffRoastedStock < 0)
                {
                    ContextManager.Context.dTransactions.Add(new dTransaction
                    {
                        Date = DateTime.Now,
                        Sum = - stock.diffRoastedStock * stock.Stock.RoastedCost,
                        Description = "Списание" + stock.diffGreenStock + " кг жареного кофе, сорта: " + stock.Name,
                        Account = ContextManager.Context.Accounts.First(),
                        Participant = ""
                    });
                }
            }
        }

        private void SavePackage()
        {
            foreach (var stock in PackageStocks)
            {
                if (stock.diffQuantity > 0)
                {
                    if (stock.Stock.Quantity == 0)
                        stock.Stock.Cost = 0;
                    else
                        stock.Stock.Cost = Math.Round((stock.Stock.Quantity * stock.Stock.Cost) /
                                                           (stock.Stock.Quantity + stock.diffQuantity), 2);
                    stock.Stock.Quantity += stock.diffQuantity;
                }
                else if (stock.diffQuantity < 0)
                {
                    ContextManager.Context.dTransactions.Add(new dTransaction
                    {
                        Date = DateTime.Now,
                        Sum = - stock.diffQuantity * stock.Stock.Cost,
                        Description = "Списание" + stock.diffQuantity + " ед. упаковки: " + stock.Name,
                        Account = ContextManager.Context.Accounts.First(),
                        Participant = ""
                    });
                }
            }
        }

        private void SavePacked()
        {
            foreach (var stock in PackedStocks)
            {
                if (stock.diffQuantity > 0)
                {
                    if (stock.Stock.Quantity == 0)
                        stock.Stock.Cost = 0;
                    else
                        stock.Stock.Cost = Math.Round((stock.Stock.Quantity * stock.Stock.Cost) /
                                                           (stock.Stock.Quantity + stock.diffQuantity), 2);
                    stock.Stock.Quantity += stock.diffQuantity;
                }
                else if (stock.diffQuantity < 0)
                {
                    ContextManager.Context.dTransactions.Add(new dTransaction
                    {
                        Date = DateTime.Now,
                        Sum = - stock.diffQuantity * stock.Stock.Cost,
                        Description = "Списание" + stock.diffQuantity + " пачек упакованного кофе: " + stock.Name,
                        Account = ContextManager.Context.Accounts.First(),
                        Participant = ""
                    });
                }
            }
        }

        private void SaveProduct()
        {
            foreach (var stock in ProductStocks)
            {
                if (stock.diffQuantity > 0)
                {
                    if (stock.Stock.Quantity == 0)
                        stock.Stock.Cost = 0;
                    else
                        stock.Stock.Cost = Math.Round((stock.Stock.Quantity * stock.Stock.Cost) /
                                                           (stock.Stock.Quantity + stock.diffQuantity), 2);
                    stock.Stock.Quantity += stock.diffQuantity;
                }
                else if (stock.diffQuantity < 0)
                {
                    ContextManager.Context.dTransactions.Add(new dTransaction
                    {
                        Date = DateTime.Now,
                        Sum = - stock.diffQuantity * stock.Stock.Cost,
                        Description = "Списание" + stock.diffQuantity + " ед. сопутствующих товаров: " + stock.Name,
                        Account = ContextManager.Context.Accounts.First(),
                        Participant = ""
                    });
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class wrapCoffeeStock:INotifyPropertyChanged
    {
        private double _diffGreenStock;
        private double _diffRoastedStock;
        private double _newGreenStock;
        private double _newRoastedStock;

        public dCoffeeStock Stock { get; set; }

        public string Name => Stock.CoffeeSort.Name;

        public double newGreenStock
        {
            get { return _newGreenStock; }
            set
            {
                _newGreenStock = value;
                diffGreenStock = value - Stock.GreenQuantity;
            }
        }

        public double newRoastedStock
        {
            get { return _newRoastedStock; }
            set
            {
                _newRoastedStock = value;
                diffRoastedStock = value - Stock.RoastedQuantity;
            }
        }

        public double diffGreenStock
        {
            get { return _diffGreenStock; }
            private set
            {
                _diffGreenStock = value;
                OnPropertyChanged();
            }
        }

        public double diffRoastedStock
        {
            get { return _diffRoastedStock; }
            private set
            {
                _diffRoastedStock = value; 
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class wrapPackageStock:INotifyPropertyChanged
    {
        private double _newQuantity;
        private double _diffQuantity;

        public dPackageStocks Stock { get; set; }

        public string Name => Stock.Package.Name;

        public double newQuantity
        {
            get { return _newQuantity; }
            set
            {
                _newQuantity = value;
                diffQuantity = value - Stock.Quantity;
            }
        }

        public double diffQuantity
        {
            get { return _diffQuantity; }
            private set
            {
                _diffQuantity = value; 
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class wrapPackedStock:INotifyPropertyChanged
    {
        private int _newQuantity;
        private int _diffQuantity;

        public dPackedStocks Stock { get; set; }

        public string Name => Stock.Mix.Name 
                    + " - " + Stock.Package.Name 
                    + " - " + Stock.Package.Capacity;

        public int newQuantity
        {
            get { return _newQuantity; }
            set
            {
                _newQuantity = value;
                diffQuantity = value - Stock.Quantity;
            }
        }

        public int diffQuantity
        {
            get { return _diffQuantity; }
            private set
            {
                _diffQuantity = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class wrapProductStock:INotifyPropertyChanged
    {
        private double _newQuantity;
        private double _diffQuantity;
        public dProductStock Stock { get; set; }

        public string Name => Stock.Product.Name;

        public double newQuantity
        {
            get { return _newQuantity; }
            set
            {
                _newQuantity = value;
                diffQuantity = value - Stock.Quantity;
            }
        }

        public double diffQuantity
        {
            get { return _diffQuantity; }
            private set
            {
                _diffQuantity = value; 
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
