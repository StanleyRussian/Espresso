using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
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

        protected override void OnSaveValidation()
        { }

        protected override void OnSaveCreate()
        {
            // Add transfer to database
            ContextManager.Context.CoffeeTransfers.Add(Transfer);
            // Change stocks of roasted coffee
            foreach (var detail in Transfer.Mix.Mix_Details)
            {
                ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id).RoastedQuantity -= detail.Ratio *
                                                                                     Transfer.Quantity;
            }

        }
    }
}
