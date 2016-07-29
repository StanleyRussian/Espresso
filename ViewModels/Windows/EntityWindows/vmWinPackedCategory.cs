using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinPackedCategory :Abstract.aEntityWindowViewModel
    {
        public vmWinPackedCategory(object argCategory = null)
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
            ContextManager.Context.PackedCategories.Add(Category);
            SaveContext();
        }
    }

    #endregion
}
