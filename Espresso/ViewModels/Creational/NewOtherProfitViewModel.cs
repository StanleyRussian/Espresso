using System;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewOtherProfitViewModel : Abstract.aCreationalViewModel
    {
        public NewOtherProfitViewModel() : base() { }

        protected override void Refresh()
        {
            NewOtherProfit = new Entity.OtherProfit();
            NewOtherProfit.Date = DateTime.Now;
            NewOtherProfit.Account = ContextManager.ActiveAccounts.FirstOrDefault();
        }

        #region Binding Properties 

        private Entity.OtherProfit _newOtherProfit;
        public Entity.OtherProfit NewOtherProfit
        {
            get { return _newOtherProfit; }
            set
            {
                _newOtherProfit = value;
                OnPropertyChanged("NewOtherProfit");
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            _context.OtherProfits.Add(NewOtherProfit);
            SaveContext();
        }
        #endregion
    }
}
