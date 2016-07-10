using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using Core.Annotations;
using Core.ViewModels.Statistics.Abstract;

namespace Core.ViewModels.Statistics.Accounts
{
    public class ParentAccountsViewModel : aParentTabViewModel
    {
        public ParentAccountsViewModel()
        {
            Header = "Счета";
        }

        protected override void Load()
        {
            base.Load();
            foreach (var activeAccount in ContextManager.ActiveAccounts)
            {
                Tabs.Add(new IndividualAccountViewModel(activeAccount));
            }
        }
    }
}
