using System;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinMoneyTranfer: Abstract.aEntityWindowViewModel
    {
        public vmWinMoneyTranfer(object argTranfer)
        {
            if (argTranfer != null)
                Transfer = argTranfer as MoneyTransfer;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Transfer = new MoneyTransfer
            {
                Date = DateTime.Now
            };
        }

        private MoneyTransfer _transfer;
        public MoneyTransfer Transfer
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
            if (CreatingNew)
                ContextManager.Context.MoneyTransfers.Add(Transfer);
            SaveContext();
        }
    }
}
