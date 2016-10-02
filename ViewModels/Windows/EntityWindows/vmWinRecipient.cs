using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinRecipient :Abstract.aEntityWindowViewModel
    {
        private Recipient _recipient;
        public Recipient Recipient
        {
            get { return _recipient; }
            set
            {
                _recipient = value;
                OnPropertyChanged();
            }
        }

        protected override void OnOpenEdit(object argEntity)
        {
            Recipient = argEntity as Recipient;
        }

        protected override void OnOpenNew()
        {
            Recipient = new Recipient();
        }

        protected override void OnSaveValidation() { }

        protected override void OnSaveEdit() { }

        protected override void OnSaveNew()
        {
            ContextManager.Context.Recipients.Add(Recipient);
        }


        public vmWinRecipient(object argEntity) : base(argEntity)
        {
        }
    }
}
