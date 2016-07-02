using System;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewRoastingViewModel: Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewRoasting = new Entity.Roasting
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

        private Entity.Roasting _newRoasting;
        public Entity.Roasting NewRoasting
        {
            get { return _newRoasting; }
            set
            {
                _newRoasting = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSave_Execute()
        {
            NewRoasting.RoastedAmount = NewRoasting.InitialAmount * (100-ShrinkagePercent) /100;
            _context.Roastings.Add(NewRoasting);

            SaveContext();
        }
    }
}
