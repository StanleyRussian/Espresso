using System;

namespace Core.ViewModels.Creational
{
    public class NewPackingViewModel :Abstract.aCreationalViewModel
    {
        public NewPackingViewModel() : base() { }

        protected override void Refresh()
        {
            NewPacking = new Entity.Packing();
            NewPacking.Date = DateTime.Now;
        }

        #region Binding Properties 

        private Entity.Packing _newPacking;
        public Entity.Packing NewPacking
        {
            get { return _newPacking; }
            set
            {
                _newPacking = value;
                OnPropertyChanged("NewPacking");
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            _context.Packings.Add(NewPacking);
            SaveContext();
        }

        #endregion
    }
}
