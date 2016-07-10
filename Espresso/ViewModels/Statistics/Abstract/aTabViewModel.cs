using System.ComponentModel;
using System.Runtime.CompilerServices;
using Core.Annotations;

namespace Core.ViewModels.Statistics.Abstract
{
    public abstract class aTabViewModel: INotifyPropertyChanged
    {
        public string Header { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (_isSelected)
                    Load();
                OnPropertyChanged();
            }
        }

        protected abstract void Load();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}