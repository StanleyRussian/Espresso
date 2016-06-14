using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    public class HomeViewModel
    {
        private Entity.ContextContainer _context;

        public HomeViewModel()
        {
            _context = new Entity.ContextContainer();
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        //public ObservableCollection 

        #endregion

        #region Commands

        #endregion
    }
}
