using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Core.ViewModels
{
    public class EditingTabsViewModel: INotifyPropertyChanged
    {
        public EditingTabsViewModel()
        {
            TabViewModels = new ObservableCollection<iTabViewModel>();
            //TabViewModels.Add(new Listing.AccountsViewModel { Header = "Счета" });
            //TabViewModels.Add(new Listing.CoffeePurchasesViewModel { Header = "Закупки" });

            //SelectedTabViewModel = TabViewModels[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<iTabViewModel> _tabViewModels;
        public ObservableCollection<iTabViewModel> TabViewModels
        {
            get { return _tabViewModels; }
            set
            {
                _tabViewModels = value;
                OnPropertyChanged("TabViewModels");
            }
        }

        private iTabViewModel _selectedViewModel;
        public iTabViewModel SelectedTabViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged("SelectedTabViewModel");
            }
        }
    }

    public interface iTabViewModel
    {
        string Header { get; set; }
    }
}
