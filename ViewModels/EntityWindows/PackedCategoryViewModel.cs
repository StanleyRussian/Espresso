using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class PackedCategoryViewModel :Abstract.aEntityWindowViewModel
    {
        public PackedCategoryViewModel(object argCategory = null)
        {
            if (argCategory != null)
                Category = argCategory as PackedCategory;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Category = new PackedCategory();
        }

        #region Binding Properties 

        private PackedCategory _category;
        public PackedCategory Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            _context.PackedCategories.Add(Category);
            SaveContext();
        }
    }

    #endregion
}
