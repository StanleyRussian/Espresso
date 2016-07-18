using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using ViewModels.Statistics.Abstract;

namespace ViewModels.Statistics.Suppliers
{
    public class IndividualSupplierViewModel: aTabViewModel
    {
        public IndividualSupplierViewModel(Supplier supplier)
        {
            Supplier = supplier;
            Header = supplier.Name;
        }

        public Supplier Supplier { get; }

        protected override void Load()
        { }
    }
}
