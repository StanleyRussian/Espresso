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

        protected aEntityWindowViewModel(object argEntity)
        {
            if (argEntity != null)
                OnOpenEdit(argEntity);
            else
            {
                CreatingNew = true;
                OnOpenNew();
            }

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

        protected void SaveContext()
        {
            try
            {
                ContextManager.Context.SaveChanges();
                IsFlySuccessOpened = true;
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

        protected virtual void cmdSave_Execute()
        {
            try
            {
                OnSaveValidation();
            }
            catch (Exception ex)
            {
                FlyErrorMsg = ex.Message;
                IsFlyErrorOpened = true;
                return;
            }

            if (CreatingNew)
                OnSaveNew();
            else OnSaveEdit();

            SaveContext();
            if (CreatingNew)
                OnOpenNew();
        }

        protected abstract void OnOpenNew();
        protected abstract void OnSaveNew();
        protected abstract void OnOpenEdit(object argEntity);
        protected abstract void OnSaveEdit();
        protected abstract void OnSaveValidation();

        public ICommand cmdOnClosing { get; private set; }
        protected void cmdOnClosing_Execute()
        {
            foreach (DbEntityEntry entry in ContextManager.Context.ChangeTracker.Entries())
                if (entry.State == EntityState.Deleted || entry.State == EntityState.Modified)
                    entry.State = EntityState.Unchanged;
        }

        #endregion
    }
}
