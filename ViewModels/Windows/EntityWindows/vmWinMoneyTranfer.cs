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
                Tranfer = argTranfer as MoneyTransfer;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            throw new NotImplementedException();
        }

        private MoneyTransfer _tranfer;
        public MoneyTransfer Tranfer
        {
            get { return _tranfer; }
            set
            {
                _tranfer = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                ContextManager.Context.MoneyTransfers.Add(Tranfer);
            SaveContext();
        }
    }
}
