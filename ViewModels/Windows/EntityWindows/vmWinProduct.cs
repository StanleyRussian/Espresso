using Model;
using Model.Entity;
using ViewModels.Windows.EntityWindows.Abstract;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinProduct : aEntityWindowViewModel
    {
        public vmWinProduct(object argEntity) : base(argEntity) { }

        protected override void OnOpenNew()
        {
            Product = new Product();
        }

        private Product _product;
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        protected override void OnSaveEdit() { }

        protected override void OnSaveValidation() { }

        protected override void OnSaveNew()
        {
            ContextManager.Context.Products.Add(Product);
            ContextManager.Context.dProductStocks.Add(new dProductStock {Product = Product});
        }

        protected override void OnOpenEdit(object argEntity)
        {
            Product = argEntity as Product;
        }
    }
}
