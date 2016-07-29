using System.Collections.ObjectModel;
using Model;

namespace ViewModels.Pages.Statistics.CoffeeSorts
{
    public class vmStatsCoffeeSorts : aTabViewModel
    {
        public vmStatsCoffeeSorts()
        {
            Header = "По сортам кофе";
        }

        protected override void Load()
        {
            CoffeeSorts = new ObservableCollection<vmSoloCoffeeSort>();
            foreach (var sort in ContextManager.ActiveCoffeeSorts)
                CoffeeSorts.Add(new vmSoloCoffeeSort(sort));
        }

        public ObservableCollection<vmSoloCoffeeSort> CoffeeSorts { get; private set; }
    }
}
