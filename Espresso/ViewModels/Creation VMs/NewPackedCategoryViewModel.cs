using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    class NewPackedCategoryViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewPackedCategoryViewModel()
        {
            _context = new Entity.ContextContainer();
            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            Refresh();
        }

        private void Refresh()
        {
            NewPackedCategory = new Entity.PackedCategory();
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.PackedCategory _newPackedCategory;
        public Entity.PackedCategory NewPackedCategory
        {
            get { return _newPackedCategory; }
            set
            {
                _newPackedCategory = value;
                OnPropertyChanged("NewPackedCategory");
            }
        }

        #endregion

        #region Commands

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.PackedCategories.Add(NewPackedCategory);
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

    #endregion
}
