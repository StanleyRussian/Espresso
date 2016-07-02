using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Core.Annotations;

namespace Core.ViewModels.Creational.Abstract
{
    public abstract class aCreationalViewModel: INotifyPropertyChanged
    {
        protected Entity.ContextContainer _context = ContextManager.Context;

        protected aCreationalViewModel()
        {
            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isFlySuccessOpened;
        public bool IsFlySuccessOpened
        {
            get { return _isFlySuccessOpened; }
            set
            {
                _isFlySuccessOpened = value;
                OnPropertyChanged();
            }
        }

        private bool _isFlyErrorOpened;
        public bool IsFlyErrorOpened
        {
            get { return _isFlyErrorOpened; }
            set
            {
                _isFlyErrorOpened = value;
                OnPropertyChanged();
            }
        }

        private string _flyErrorMsg;
        public string FlyErrorMsg
        {
            get { return _flyErrorMsg; }
            set
            {
                _flyErrorMsg = value;
                OnPropertyChanged();
            }
        }

        protected abstract void Refresh();
        protected void SaveContext()
        {
            try
            {
                _context.SaveChanges();
                IsFlySuccessOpened = true;
                _context.Accounts.Load();
                Refresh();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                FlyErrorMsg = "";
                foreach (var x in ex.EntityValidationErrors)
                    foreach (var y in x.ValidationErrors)
                    {
                        FlyErrorMsg += y.ErrorMessage;
                        FlyErrorMsg += '\n';
                    }
                IsFlyErrorOpened = true;
            }
            catch (Exception ex)
            {
                FlyErrorMsg = ex.Message;
                IsFlyErrorOpened = true;
            }
        }

        #region Commands

        public ICommand cmdSave
        { get; private set; }

        protected abstract void cmdSave_Execute();

        #endregion
    }
}
