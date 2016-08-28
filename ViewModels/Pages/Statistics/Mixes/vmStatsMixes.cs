using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Entity;

namespace ViewModels.Pages.Statistics.Mixes
{
    public class vmStatsMixes: aTabViewModel
    {
        public vmStatsMixes()
        {
            Header = "Купажи";
        }

        protected override void Load()
        {
            //foreach (var mix in ContextManager.Context.Mixes)
            //{
            //    mix.dCost = 0;
            //    foreach (var detail in mix.Mix_Details)
            //    {
            //        mix.dCost += ContextManager.Context.CoffeePurchase_Details.OrderByDescending(p => p.Id)
            //            .First(p => p.CoffeeSort.Id == detail.CoffeeSort.Id)
            //            .Price * detail.Ratio / 100;
            //    }
            //}
            //ContextManager.Context.SaveChanges();

            //Tabs = new ObservableCollection<Mix>(ContextManager.Context.Mixes);
        }

        public ObservableCollection<Mix> Tabs { get; set; }
    }
}
