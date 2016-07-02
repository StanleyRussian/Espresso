using System;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewOtherProfitViewModel : Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewOtherProfit = new Entity.OtherProfit
            {
                Date = DateTime.Now,
                Account = ContextManager.ActiveAccounts.FirstOrDefault()
            };
        }

        #region Binding Properties 

        private Entity.OtherProfit _newOtherProfit;
        public Entity.OtherProfit NewOtherProfit
        {
            get { return _newOtherProfit; }
            set
            {
                _newOtherProfit = value;
                OnPropertyChanged();
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
