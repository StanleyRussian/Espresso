using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Statistics.Abstract
{
    public abstract class aParentTabViewModel: aTabViewModel
    {
        public ObservableCollection<aTabViewModel> Tabs { get; private set; }
        protected override void Load()
        {
            Tabs = new ObservableCollection<aTabViewModel>();
        }
    }
}
