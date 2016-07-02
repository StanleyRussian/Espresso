using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Core.Annotations;
using Core.Auxiliary;
using Core.Entity;
using MahApps.Metro.Controls.Dialogs;

namespace Core.ViewModels.Listing.Abstract
{
    public abstract class aListingViewModel : INotifyPropertyChanged
    {
        protected ContextContainer _context = ContextManager.Context;

        public aListingViewModel()
        {
            cmdOnClosing = new Command(cmdOnClosing_Execute);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Header
        { get; set; }


        protected abstract void Refresh();

        public ICommand cmdOnClosing
        { get; private set; }
        protected void cmdOnClosing_Execute()
        {
            foreach (DbEntityEntry entry in _context.ChangeTracker.Entries())
                if (entry.State == EntityState.Deleted || entry.State == EntityState.Modified)
                    entry.State = EntityState.Unchanged;
        }

        protected bool IsEmpty(object argSelected)
        {
            if (argSelected == null)
            {
                DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", "Вы ничего не выбрали",
                    MessageDialogStyle.Affirmative, new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
                return true;
            }
            return false;
        }

        protected void SaveContext()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DialogCoordinator.Instance.ShowMessageAsync(this, "Ошибка", ex.Message,
                    MessageDialogStyle.Affirmative, new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented });
            }
            finally
            {
                foreach (DbEntityEntry entry in _context.ChangeTracker.Entries())
                    if (entry.State == EntityState.Deleted|| entry.State == EntityState.Modified)
                        entry.State = EntityState.Unchanged;
                Refresh();
            }
        }
    }
}
