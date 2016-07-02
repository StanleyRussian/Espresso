namespace Core.ViewModels.Creational
{
    public class NewPackedCategoryViewModel :Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewPackedCategory = new Entity.PackedCategory();
        }

        #region Binding Properties 

        private Entity.PackedCategory _newPackedCategory;
        public Entity.PackedCategory NewPackedCategory
        {
            get { return _newPackedCategory; }
            set
            {
                _newPackedCategory = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            _context.PackedCategories.Add(NewPackedCategory);
            SaveContext();
        }
    }

    #endregion
}
