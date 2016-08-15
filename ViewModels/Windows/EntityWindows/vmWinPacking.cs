using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinPacking :Abstract.aEntityWindowViewModel
    {
        public vmWinPacking(object argPacking = null)
        {
            if (argPacking!=null)
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

        protected override void cmdSave_Execute()
        {
            if (Packing.PackQuantity <= 0)
            {
                FlyErrorMsg = "Введите количество пачек кофе";
                IsFlyErrorOpened = true;
                return;
            }

            if (Packing.Mix.Mix_Details.Any(
                    detail => ContextManager.Context.dRoastedStocks.First(
                        p => p.CoffeeSort.Id == detail.CoffeeSort.Id)
                    .Quantity < (Packing.PackQuantity*detail.Ratio/100)))
            {
                FlyErrorMsg = "Недостаточно обжаренного кофе в наличии";
                IsFlyErrorOpened = true;
                return;
            }

            if (ContextManager.Context.dPackageStocks.First(p=>p.Package.Id == Packing.Package.Id)
                    .Quantity < Packing.PackQuantity)
            {
                FlyErrorMsg = "Недостаточно пачек в наличии";
                IsFlyErrorOpened = true;
                return;
            }
            if (CreatingNew)
                ContextManager.Context.Packings.Add(Packing);
            SaveContext();
        }

        #endregion
    }
}
