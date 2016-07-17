using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.Properties;

namespace ViewModels.Statistics.Abstract
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
            }
        }

        protected abstract void Load();

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}