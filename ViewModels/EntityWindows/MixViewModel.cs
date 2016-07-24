using System.Collections.ObjectModel;
using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class MixViewModel : Abstract.aEntityWindowViewModel
    {
        public MixViewModel(object argMix = null)
        {
            if (argMix != null)
            {
                Mix = argMix as Mix;
            }
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Mix = new Mix();
            Details = new ObservableCollection<Mix_Details>();
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        private Mix _mix;
        public Mix Mix
        {
            get { return _mix; }
            set
            {
                _mix = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Mix_Details> _details;
        public ObservableCollection<Mix_Details> Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            double total = 0;
            foreach (var x in Details)
                total += x.Ratio;
            if (total != 100)
            {
                FlyErrorMsg = "Неправильные пропорции, общая сумма не равна 100%";
                IsFlyErrorOpened = true;
                return;
            }
            if (CreatingNew)
            {
                _mix.Mix_Details.Clear();
                foreach (var x in Details)
                    _mix.Mix_Details.Add(x);
                ContextManager.Context.Mixes.Add(_mix);
            }
            SaveContext();
        }
    }

    #endregion
}