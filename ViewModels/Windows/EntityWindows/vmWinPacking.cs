using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinPacking : Abstract.aEntityWindowViewModel
    {
        public vmWinPacking(object argPacking = null)
        {
            if (argPacking != null)
                Packing = argPacking as Packing;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Packing = new Packing
            {
                Date = DateTime.Now,
                Mix = ContextManager.ActiveMixes.FirstOrDefault(),
                Package = ContextManager.ActivePackages.FirstOrDefault()
            };
        }

        #region Binding Properties 

        private Packing _packing;

        public Packing Packing
        {
            get { return _packing; }
            set
            {
                _packing = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void OnSaveValidation()
        {
            if (Packing.Quantity <= 0)
                throw new Exception("Введите количество пачек кофе");

            if (Packing.Mix.Mix_Details.Any(
                detail => ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id).RoastedQuantity 
                        < (Packing.Quantity*detail.Ratio*Packing.Package.Capacity)))
                throw new Exception("Недостаточно обжаренного кофе в наличии");

            if (ContextManager.Context.dPackageStocks.First(p => p.Package.Id == Packing.Package.Id)
                .Quantity < Packing.Quantity)
                throw new Exception("Недостаточно пачек в наличии");
        }

        protected override void OnSaveCreate()
        {
            // Add packing to database
            ContextManager.Context.Packings.Add(Packing);
            // Find or create packed stock
            var packedStocks = ContextManager.Context.dPackedStocks.FirstOrDefault(
                p => p.Mix.Id == Packing.Mix.Id &&
                     p.Package.Id == Packing.Package.Id) ??
                               ContextManager.Context.dPackedStocks.Add(new dPackedStocks
                               {
                                   Mix = Packing.Mix,
                                   Package = Packing.Package
                               });

            // Calculate cost of packed coffee
            double cost = (from detail in Packing.Mix.Mix_Details
                select ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id).RoastedCost*detail.Ratio*Packing.Package.Capacity).Sum();
            cost += ContextManager.Context.dPackageStocks.First(p => p.Package.Id == Packing.Package.Id).Cost;
            // Check if there anything in stock already
            if (packedStocks.Quantity == 0)
                // Set cost from roasting
                packedStocks.Cost = Math.Round(cost, 2);
            else
            // Count cost based on stock and new packing
                packedStocks.Cost = Math.Round((packedStocks.Quantity*packedStocks.Cost + Packing.Quantity*cost)
                                               /(packedStocks.Quantity + Packing.Quantity), 2);

            // Change packed stocks
            packedStocks.Quantity += Packing.Quantity;
            // Change stocks of roasted coffee
            foreach (var detail in Packing.Mix.Mix_Details)
            {
                ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id).RoastedQuantity -= detail.Ratio*
                                                                                     Packing.Package.Capacity*
                                                                                     Packing.Quantity;
            }
            // Change package stocks
            ContextManager.Context.dPackageStocks.First(
                p => p.Package.Id == Packing.Package.Id).Quantity -= Packing.Quantity;
        }

        #endregion
    }
}
