using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class PackageViewModel :Abstract.aEntityWindowViewModel
    {
        public PackageViewModel(object argPackage = null)
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

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                _context.Packages.Add(Package);
            SaveContext();
        }

        #endregion
    }
}
