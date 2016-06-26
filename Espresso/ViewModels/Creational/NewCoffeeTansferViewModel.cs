using System;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewCoffeeTransferViewModel: Abstract.aCreationalViewModel
    {
        public NewCoffeeTransferViewModel() : base() { }

        protected override void Refresh()
        {
            NewCoffeeTransfer = new Entity.CoffeeTransfer();
            NewCoffeeTransfer.Date = DateTime.Now;
            NewCoffeeTransfer.Mix = ContextManager.ActiveMixes.FirstOrDefault();
        }

        private Entity.CoffeeTransfer _newCoffeeTransfer;
        public Entity.CoffeeTransfer NewCoffeeTransfer
        {
            get { return _newCoffeeTransfer; }
            set
            {
                _newCoffeeTransfer = value;
                OnPropertyChanged("NewCoffeeTransfer");
            }
        }

        protected override void cmdSave_Execute()
        {
            _context.CoffeeTransfers.Add(NewCoffeeTransfer);
            SaveContext();
        }
    }
}
