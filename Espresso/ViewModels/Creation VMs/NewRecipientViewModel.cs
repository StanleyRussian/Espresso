using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    public class NewRecipientViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewRecipientViewModel()
        {
            _context = new Entity.ContextContainer();
            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            Refresh();
        }
        private void Refresh()
        {
            NewRecipient = new Entity.Recipient();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.Recipients.Add(NewRecipient);
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно");
                Refresh();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var x in ex.EntityValidationErrors)
                    foreach (var y in x.ValidationErrors)
                        MessageBox.Show(y.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
