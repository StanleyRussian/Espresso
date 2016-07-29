using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinRecipient :Abstract.aEntityWindowViewModel
    {
        public vmWinRecipient(object argRecipient = null)
        {
            if (argRecipient != null) 
                Recipient = argRecipient as Recipient;
        }

        protected override void Refresh()
        {
            Recipient = new Recipient();
        }

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

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                ContextManager.Context.Recipients.Add(Recipient);
            SaveContext();
        }

    }
}
