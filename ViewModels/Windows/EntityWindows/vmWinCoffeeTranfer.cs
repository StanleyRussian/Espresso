using System;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;

namespace ViewModels.Windows.EntityWindows
{
    public class CoffeeTransferViewModel: Abstract.aEntityWindowViewModel
    {
        public CoffeeTransferViewModel(object argEntity) : base(argEntity) { }

        //public CoffeeTransferViewModel(object argTranfer = null)
        //{
        //    if (argTranfer != null)
        //        Transfer = argTranfer as CoffeeTransfer;
        //    else
        //    {
        //        CreatingNew = true;
        //        OnOpenNew();
        //    }
        //}

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

        private CoffeeTransfer _oldTransfer;

        protected override void OnOpenEdit(object argEntity)
        {
            Transfer = argEntity as CoffeeTransfer;
            _oldTransfer = Transfer.Clone();
        }

        protected override void OnOpenNew()
        {
            Transfer = new CoffeeTransfer
            {
                Date = DateTime.Now,
                Mix = ContextManager.ActiveMixes.FirstOrDefault()
            };
        }

        protected override void OnSaveEdit()
        {
            // Change stocks of roasted coffee
            foreach (var detail in Transfer.Mix.Mix_Details)
            {
                ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id).RoastedQuantity
                        -= detail.Ratio*Transfer.Quantity + _oldTransfer.Quantity*detail.Ratio;
            }
        }

        protected override void OnSaveValidation() { }

        protected override void OnSaveNew()
        {
            // Add transfer to database
            ContextManager.Context.CoffeeTransfers.Add(Transfer);
            // Change stocks of roasted coffee
            foreach (var detail in Transfer.Mix.Mix_Details)
            {
                ContextManager.Context.dCoffeeStocks.First(
                    p => p.CoffeeSort.Id == detail.CoffeeSort.Id).RoastedQuantity 
                        -= detail.Ratio * Transfer.Quantity;
            }
        }
    }
}
