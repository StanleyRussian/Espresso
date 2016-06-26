﻿namespace Core.ViewModels.Creational
{
    public class NewAccountViewModel: Abstract.aCreationalViewModel
    {

        public NewAccountViewModel() : base() { }

        protected override void Refresh()
        {
            NewAccount = new Entity.Account();
        }

        #region Binding Properties

        private Entity.Account _newAccount;
        public Entity.Account NewAccount
        {
            get { return _newAccount; }
            set
            {
                _newAccount = value;
                OnPropertyChanged("NewAccount");
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            _context.Accounts.Add(NewAccount);
            SaveContext();
        }

        #endregion
    }
}
