using System;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewRoastingViewModel: Abstract.aCreationalViewModel
    {
        public NewRoastingViewModel() : base() { }

        protected override void Refresh()
        {
            NewRoasting = new Entity.Roasting();
            NewRoasting.Date = DateTime.Now;
            NewRoasting.CoffeeSort = ContextManager.ActiveCoffeeSorts.FirstOrDefault();
        }

        // Since DB doesn't store srinkage percentage rather then both quantities
        // some additional magic required in view model
        private int _shrinkagePercent;
        public int ShrinkagePercent
        {
            get { return _shrinkagePercent; }
            set
            {
                _shrinkagePercent = value;
                OnPropertyChanged("ShrinkagePercent");
            }

        }

        private Entity.Roasting _newRoasting;
        public Entity.Roasting NewRoasting
        {
            get { return _newRoasting; }
            set
            {
                _newRoasting = value;
                OnPropertyChanged("NewRoasting");
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
