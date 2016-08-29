using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;
using Model.Properties;
using ViewModels.Auxiliary;

namespace ViewModels.Windows.EntityWindows.Abstract
{
    public abstract class aEntityWindowViewModel: INotifyPropertyChanged
    {
        protected bool CreatingNew = false;

        protected aEntityWindowViewModel()
        {
            cmdSave = new Command(cmdSave_Execute);
            cmdOnClosing = new Command(cmdOnClosing_Execute);
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
                ContextManager.Context.SaveChanges();
                IsFlySuccessOpened = true;
                //if (CreatingNew)
                //    Refresh();
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
                FlyErrorMsg = ex.Message + '\n';
                IsFlyErrorOpened = true;
            }
        }

        #region Commands

        public ICommand cmdSave { get; private set; }

        protected abstract void cmdSave_Execute();

        public ICommand cmdOnClosing { get; private set; }
        protected void cmdOnClosing_Execute()
        {
            //ContextManager.ReloadContext();
            foreach (DbEntityEntry entry in ContextManager.Context.ChangeTracker.Entries())
                if (entry.State == EntityState.Deleted || entry.State == EntityState.Modified)
                    entry.State = EntityState.Unchanged;
        }

        #endregion
    }
}
