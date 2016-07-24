using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class RoastingViewModel: Abstract.aEntityWindowViewModel
    {
        public RoastingViewModel(object argRoasting = null)
        {
            if (argRoasting !=null)
                Roasting = argRoasting as Roasting;
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
            Roasting.RoastedAmount = Roasting.InitialAmount * (100-ShrinkagePercent) /100;
            if (CreatingNew)
                ContextManager.Context.Roastings.Add(Roasting);
            SaveContext();
        }
    }
}
