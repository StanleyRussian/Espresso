using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinRoasting: Abstract.aEntityWindowViewModel
    {
        public vmWinRoasting(object argEntity) : base(argEntity) { }

        private int _shrinkagePercent;
        public int ShrinkagePercent
        {
            get { return _shrinkagePercent; }
            set
            {
                _shrinkagePercent = value;
                OnPropertyChanged();
            }
        }

        private Roasting _roasting;
        public Roasting Roasting
        {
            get { return _roasting; }
            set
            {
                _roasting = value;
                OnPropertyChanged();
            }
        }

        protected override void OnOpenEdit(object argEntity)
        {
            Roasting = argEntity as Roasting;
            ShrinkagePercent = (int)(100 - (Roasting.RoastedAmount * 100 / Roasting.InitialAmount));
        }

        protected override void OnOpenNew()
        {
            Roasting = new Roasting
            {
                Date = DateTime.Now,
                CoffeeSort = ContextManager.ActiveCoffeeSorts.FirstOrDefault()
            };
            ShrinkagePercent = Properties.ShrinkagePercent;
        }

        protected override void OnSaveValidation()
        {
            if (Roasting.InitialAmount <= 0)
                throw new Exception("Введите количество кофе");
            if (ShrinkagePercent < 1 || ShrinkagePercent > 100)
                throw new Exception("Введите процент ужарки от одного до ста");

            if (Roasting.InitialAmount >
                ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == Roasting.CoffeeSort.Id).GreenQuantity)
                throw new Exception("Недостаточно зелёного кофе в наличии, чтобы пожарить введённое количество");
        }

        protected override void OnSaveEdit()
        {
            throw new NotImplementedException();
        }

        protected override void OnSaveNew()
        {
            Roasting.ShrinkagePercent = (100 - ShrinkagePercent) / 100d;
            Roasting.RoastedAmount = Roasting.InitialAmount * Roasting.ShrinkagePercent;
            Properties.ShrinkagePercent = ShrinkagePercent;
            // Add roasting to database
            ContextManager.Context.Roastings.Add(Roasting);
            // Change roasted stocks cost
            var coffeeStock = ContextManager.Context.dCoffeeStocks.First(
                p => p.CoffeeSort.Id == Roasting.CoffeeSort.Id);
            // Calculate cost of coffee for current roasting
            var cost = Math.Round(coffeeStock.GreenCost*100/(100 - ShrinkagePercent),2);
            // Check if there anything in stock already
            if (coffeeStock.RoastedQuantity == 0)
                // Set cost from roasting
                coffeeStock.RoastedCost = Math.Round(cost, 2);
            else
            // Or count cost based on stock and new roasting
                coffeeStock.RoastedCost = Math.Round((coffeeStock.RoastedQuantity*coffeeStock.RoastedCost +
                                                      Roasting.RoastedAmount*cost)
                                                     /(coffeeStock.RoastedQuantity + Roasting.RoastedAmount), 2);
            // Change coffee stocks
            coffeeStock.GreenQuantity -= Roasting.InitialAmount;
            coffeeStock.RoastedQuantity += Roasting.RoastedAmount;
        }
    }
}
