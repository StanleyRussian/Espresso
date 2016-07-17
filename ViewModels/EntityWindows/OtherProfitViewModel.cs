﻿using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class OtherProfitViewModel : Abstract.aEntityWindowViewModel
    {
        public OtherProfitViewModel(object argProfit = null)
        {
            if (argProfit != null)
                Profit = argProfit as OtherProfit;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Profit = new OtherProfit
            {
                Date = DateTime.Now,
                Account = ContextManager.ActiveAccounts.FirstOrDefault()
            };
        }

        #region Binding Properties 

        private OtherProfit _profit;
        public OtherProfit Profit
        {
            get { return _profit; }
            set
            {
                _profit = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                _context.OtherProfits.Add(Profit);
            SaveContext();
        }
        #endregion
    }
}
