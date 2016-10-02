using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinAccount: Abstract.aEntityWindowViewModel
    {
        public vmWinAccount(object argEntity) : base(argEntity) { }
        
        private Account _account;
        public Account Account
        {
            get { return _account; }
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        protected override void OnOpenEdit(object argEntity)
        {
            Account = argEntity as Account;
        }

        protected override void OnOpenNew()
        {
            Account = new Account();
        }

        protected override void OnSaveValidation() { }

        protected override void OnSaveEdit() { }

        protected override void OnSaveNew()
        {
            ContextManager.Context.Accounts.Add(Account);
            ContextManager.Context.dAccountsBalances.Add(new dAccountsBalance
            {
                Account = Account,
                Balance = 0
            });
        }

    }
}
