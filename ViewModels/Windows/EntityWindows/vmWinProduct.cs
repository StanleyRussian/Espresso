using Model;
using Model.Entity;
using ViewModels.Windows.EntityWindows.Abstract;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinProduct : aEntityWindowViewModel
    {
        private Product _product;

        public vmWinProduct(object argProduct = null)
        {
            if (argProduct != null)
                Product = argProduct as Product;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Product = new Product();
        }

        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                ContextManager.Context.Products.Add(Product);
            SaveContext();

            if (CreatingNew)
                Refresh();
        }
    }
}
