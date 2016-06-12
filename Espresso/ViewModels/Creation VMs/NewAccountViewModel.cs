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
    public class NewAccountViewModel: INotifyPropertyChanged
    {
        protected Entity.ContextContainer _context;

        public NewAccountViewModel()
        {
            _context = new Entity.ContextContainer();
            NewAccount = new Entity.Account();

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.Account _newAccount;
        public Entity.Account NewAccount
        {
            get { return _newAccount; }
            set
            {
                _newAccount = value;
                OnPropertyChanged("NewAccount");
            }
        }

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.Accounts.Add(NewAccount);
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно");
                NewAccount = new Entity.Account();
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
