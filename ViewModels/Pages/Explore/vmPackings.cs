using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmPackings: aProcessListingViewModel
    {
        public vmPackings()
        {
            Header = "Фасовки";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.Packings.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterMix != null)
                query = query.Where(p => p.Mix.Id == FilterMix.Id);
            if (FilterPackedCategory != null)
                query = query.Where(p => p.PackedCategory.Id == FilterPackedCategory.Id);
            if (FilterPackage != null)
                query = query.Where(p => p.Package.Id == FilterPackage.Id);
            Tabs = new ObservableCollection<Packing>(query);
        }

        private ObservableCollection<Packing> _tabs;
        public ObservableCollection<Packing> Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        private Package _filterPackage;
        public Package FilterPackage
        {
            get { return _filterPackage; }
            set
            {
                _filterPackage = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private Mix _filterMix;
        public Mix FilterMix
        {
            get { return _filterMix; }
            set
            {
                _filterMix = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private PackedCategory _filterPackedCategory;
        public PackedCategory FilterPackedCategory
        {
            get { return _filterPackedCategory; }
            set
            {
                _filterPackedCategory = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        protected override void cmdDelete_Execute(object argSelected)
        { }
    }
}
