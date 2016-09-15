using System.Data.SqlClient;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinAccount: Abstract.aEntityWindowViewModel
    {
        public vmWinAccount(object argAccount = null)
        {
            if (argAccount != null)
                Account = argAccount as Account;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Account = new Account();
        }
        
        #region Binding Properties

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

        #endregion

        #region Commands

        protected override void OnSaveValidation()
        { }

        protected override void OnSaveCreate()
        {
            ContextManager.Context.Accounts.Add(Account);
            ContextManager.Context.dAccountsBalances.Add(new dAccountsBalance
            {
                Account = Account,
                Balance = 0
            });
        }

        #endregion
    }
}
