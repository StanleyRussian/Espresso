﻿namespace Core.ViewModels.Creational
{
    public class NewSupplierViewModel: Abstract.aCreationalViewModel
    {
        public NewSupplierViewModel() : base() { }

        protected override void Refresh()
        {
            NewSupplier = new Entity.Supplier();
        }

        private Entity.Supplier _newSupplier;
        public Entity.Supplier NewSupplier
        {
            get { return _newSupplier; }
            set
            {
                _newSupplier = value;
                OnPropertyChanged("NewSupplier");
            }
        }

        protected override void cmdSave_Execute()
        {
            _context.Suppliers.Add(NewSupplier);
            SaveContext();
        }

    }
}