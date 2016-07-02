using System;
using System.Linq;

namespace Core.ViewModels.Creational
{
    public class NewPackingViewModel :Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewPacking = new Entity.Packing
            {
                Date = DateTime.Now,
                Mix = ContextManager.ActiveMixes.FirstOrDefault(),
                Package = ContextManager.ActivePackages.FirstOrDefault(),
                PackedCategory = ContextManager.ActivePackedCategories.FirstOrDefault()
            };
        }

        #region Binding Properties 

        private Entity.Packing _newPacking;
        public Entity.Packing NewPacking
        {
            get { return _newPacking; }
            set
            {
                _newPacking = value;
                OnPropertyChanged();
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
