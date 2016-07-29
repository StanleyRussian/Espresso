using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinPacking :Abstract.aEntityWindowViewModel
    {
        public vmWinPacking(object argPacking = null)
        {
            if (argPacking!=null)
                Packing = argPacking as Packing;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Packing = new Packing
            {
                Date = DateTime.Now,
                Mix = ContextManager.ActiveMixes.FirstOrDefault(),
                Package = ContextManager.ActivePackages.FirstOrDefault(),
                PackedCategory = ContextManager.ActivePackedCategories.FirstOrDefault()
            };
        }

        #region Binding Properties 

        private Packing _packing;
        public Packing Packing
        {
            get { return _packing; }
            set
            {
                _packing = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                ContextManager.Context.Packings.Add(Packing);
            SaveContext();
        }

        #endregion
    }
}
