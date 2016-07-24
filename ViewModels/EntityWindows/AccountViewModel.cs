﻿
using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class AccountViewModel: Abstract.aEntityWindowViewModel
    {
        public AccountViewModel(object argAccount = null)
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

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                ContextManager.Context.Accounts.Add(Account);
            SaveContext();
        }

        #endregion
    }
}