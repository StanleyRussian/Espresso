using System;
using System.Configuration;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinRoasting: Abstract.aEntityWindowViewModel
    {
        public vmWinRoasting(object argRoasting = null)
        {
            if (argRoasting != null)
            {
                Roasting = argRoasting as Roasting;
                ShrinkagePercent = (int) (100 - (Roasting.RoastedAmount*100/Roasting.InitialAmount));
            }
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Roasting = new Roasting
            {
                Date = DateTime.Now,
                CoffeeSort = ContextManager.ActiveCoffeeSorts.FirstOrDefault()
            };
            ShrinkagePercent = Settings.Default.ShrinkagePercent;
        }

        // Since DB doesn't store srinkage percentage rather then both quantities
        // addtional property required in view model
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

        protected override void cmdSave_Execute()
        {
            if (Roasting.InitialAmount <= 0)
            {
                FlyErrorMsg = "Введите количество кофе";
                IsFlyErrorOpened = true;
                return;
            }
            if (ShrinkagePercent < 1|| ShrinkagePercent > 100)
            {
                FlyErrorMsg = "Введите процент ужарки от одного до ста";
                IsFlyErrorOpened = true;
                return;
            }
            if (Roasting.InitialAmount >
                ContextManager.Context.dGreenStocks.First(p => p.CoffeeSort.Id == Roasting.CoffeeSort.Id).Quantity)
            {
                FlyErrorMsg = "Недостаточно зелёного кофе в наличии, чтобы пожарить введённое количество";
                IsFlyErrorOpened = true;
                return;
            }

            Roasting.RoastedAmount = Roasting.InitialAmount * (100-ShrinkagePercent) /100;
            Settings.Default.ShrinkagePercent = ShrinkagePercent;
            Settings.Default.Save();
            if (CreatingNew)
                ContextManager.Context.Roastings.Add(Roasting);
            SaveContext();
        }
    }
}
