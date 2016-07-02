using System;
using System.Windows.Input;
using Core.Auxiliary;
using MahApps.Metro.Controls.Dialogs;

namespace Core.ViewModels.Listing.Abstract
{
    public abstract class aProcessListingViewModel : aListingViewModel
    {
        protected aProcessListingViewModel()
        {
            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);

            cmdSave = new Command(cmdSave_Execute);
            cmdDelete = new Command(cmdDelete_Execute);
            cmdFilter30Days = new Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Command(cmdFilterAll_Execute);

            Refresh();
        }

        protected DateTime _filterFrom;
        public DateTime FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        protected DateTime _filterTo;
        public DateTime FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        #region Commands

        public ICommand cmdSave
        { get; private set; }
        protected virtual void cmdSave_Execute()
        {
            _context.SaveChanges();
            DialogCoordinator.Instance.ShowMessageAsync(this, "Успех", "Сохранение завершено");
        }

        public ICommand cmdDelete
        { get; private set; }
        protected abstract void cmdDelete_Execute(object argSelected);

        public ICommand cmdFilter30Days
        { get; private set; }
        protected void cmdFilter30Days_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged(nameof(FilterTo));
            FilterFrom = DateTime.Now.AddDays(-30);
        }

        public ICommand cmdFilterAll
        { get; private set; }
        protected void cmdFilterAll_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged(nameof(FilterTo));
            FilterFrom = DateTime.MinValue;
        }

        #endregion

    }
}
