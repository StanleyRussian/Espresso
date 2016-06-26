namespace Core.ViewModels.Creational
{
    public class NewRecipientViewModel :Abstract.aCreationalViewModel
    {
        public NewRecipientViewModel() : base() { }

        protected override void Refresh()
        {
            NewRecipient = new Entity.Recipient();
        }

        private Entity.Recipient _newRecipient;
        public Entity.Recipient NewRecipient
        {
            get { return _newRecipient; }
            set
            {
                _newRecipient = value;
                OnPropertyChanged("NewRecipient");
            }
        }

        protected override void cmdSave_Execute()
        {
            _context.Recipients.Add(NewRecipient);
            SaveContext();
        }

    }
}
