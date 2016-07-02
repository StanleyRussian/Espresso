using System;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewCoffeeTransferViewModel: Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewCoffeeTransfer = new Entity.CoffeeTransfer
            {
                Date = DateTime.Now,
                Mix = ContextManager.ActiveMixes.FirstOrDefault()
            };
        }

        private Entity.CoffeeTransfer _newCoffeeTransfer;
        public Entity.CoffeeTransfer NewCoffeeTransfer
        {
            get { return _newCoffeeTransfer; }
            set
            {
                _newCoffeeTransfer = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSave_Execute()
        {
            _context.CoffeeTransfers.Add(NewCoffeeTransfer);
            SaveContext();
        }
    }
}
