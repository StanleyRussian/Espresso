using System;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinPackage :Abstract.aEntityWindowViewModel
    {
        public vmWinPackage(object argEntity) : base(argEntity) { }

        private Package _package;
        public Package Package
        {
            get { return _package; }
            set
            {
                _package = value;
                OnPropertyChanged();
            }
        }

        protected override void OnOpenEdit(object argEntity)
        {
            Package = argEntity as Package;
        }

        protected override void OnOpenNew()
        {
            Package = new Package();
        }

        protected override void OnSaveValidation()
        {
            if (Package.Capacity <= 0)
                throw new Exception("Введите ёмкость пачки");
        }

        protected override void OnSaveEdit() { }

        protected override void OnSaveNew()
        {
            ContextManager.Context.Packages.Add(Package);
            ContextManager.Context.dPackageStocks.Add(new dPackageStocks { Package = Package });
        }
    }
}
