using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class SupplierViewModel: Abstract.aEntityWindowViewModel
    {
        public SupplierViewModel(object argSupplier = null)
        {
            if (argSupplier!=null)
                Supplier = argSupplier as Supplier;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Supplier = new Supplier();
        }

        private Supplier _supplier;
        public Supplier Supplier
        {
            get { return _supplier; }
            set
            {
                _supplier = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                ContextManager.Context.Suppliers.Add(Supplier);
            SaveContext();
        }

    }
}
