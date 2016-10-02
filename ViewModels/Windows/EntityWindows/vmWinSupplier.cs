using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinSupplier: Abstract.aEntityWindowViewModel
    {
        public vmWinSupplier(object argEntity) : base(argEntity) { }

        protected override void OnOpenNew()
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

        protected override void OnSaveValidation() { }

        protected override void OnSaveEdit() { }

        protected override void OnSaveNew()
        {
            ContextManager.Context.Suppliers.Add(Supplier);
        }

        protected override void OnOpenEdit(object argEntity)
        {
            Supplier = argEntity as Supplier;
        }
    }
}
