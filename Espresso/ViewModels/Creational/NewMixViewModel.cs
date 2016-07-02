using System.Collections.ObjectModel;

namespace Core.ViewModels.Creational
{
    public class NewMixViewModel : Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewMix = new Entity.Mix();
            Details = new ObservableCollection<Entity.Mix_Details>();
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        private Entity.Mix _newMix;
        public Entity.Mix NewMix
        {
            get { return _newMix; }
            set
            {
                _newMix = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Entity.Mix_Details> _details;
        public ObservableCollection<Entity.Mix_Details> Details
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

            _newMix.Mix_Details.Clear();
            foreach (var x in Details)
                _newMix.Mix_Details.Add(x);
            _context.Mixes.Add(_newMix);

            SaveContext();
        }
    }

    #endregion
}