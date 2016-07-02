namespace Core.ViewModels.Creational
{
    public class NewPackageViewModel :Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewPackage = new Entity.Package();
        }

        #region Binding Properties

        private Entity.Package _newPackage;
        public Entity.Package NewPackage
        {
            get { return _newPackage; }
            set
            {
                _newPackage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            _context.Packages.Add(NewPackage);
            SaveContext();
        }

        #endregion
    }
}
