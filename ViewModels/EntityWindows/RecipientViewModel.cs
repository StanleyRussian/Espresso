using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class RecipientViewModel :Abstract.aEntityWindowViewModel
    {
        public RecipientViewModel(object argRecipient = null)
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
                _context.Recipients.Add(Recipient);
            SaveContext();
        }

    }
}
