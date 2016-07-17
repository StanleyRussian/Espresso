using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class CoffeeTransferViewModel: Abstract.aEntityWindowViewModel
    {
        public CoffeeTransferViewModel(object argTranfer = null)
        {
            if (argTranfer != null)
                Transfer = argTranfer as CoffeeTransfer;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Transfer = new CoffeeTransfer
            {
                Date = DateTime.Now,
                Mix = ContextManager.ActiveMixes.FirstOrDefault()
            };
        }

        private CoffeeTransfer _transfer;
        public CoffeeTransfer Transfer
        {
            get { return _transfer; }
            set
            {
                _transfer = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSave_Execute()
        {
            _context.CoffeeTransfers.Add(Transfer);
            SaveContext();
        }
    }
}
