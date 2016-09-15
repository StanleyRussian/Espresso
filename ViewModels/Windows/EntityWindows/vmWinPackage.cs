using System;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinPackage :Abstract.aEntityWindowViewModel
    {
        public vmWinPackage(object argPackage = null)
        {
            if (argPackage != null)
                Package = argPackage as Package;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Package = new Package();
        }

        #region Binding Properties

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

        #endregion

        #region Commands

        protected override void OnSaveValidation()
        {
            if (Package.Capacity <= 0)
                throw new Exception("Введите ёмкость пачки");
        }

        protected override void OnSaveCreate()
        {
            ContextManager.Context.Packages.Add(Package);
            ContextManager.Context.dPackageStocks.Add(new dPackageStocks { Package = Package });
        }

        #endregion
    }
}
