using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinSupplier: Abstract.aEntityWindowViewModel
    {
        public vmWinSupplier(object argSupplier = null)
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

        protected override void OnSaveValidation()
        { }

        protected override void OnSaveCreate()
        {
            ContextManager.Context.Suppliers.Add(Supplier);
        }
    }
}
