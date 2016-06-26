using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class MixesViewModel: aSubjectsListingViewModel
    {
        protected override void Refresh()
        {
            var query = IsActiveSelected ? 
                ContextManager.LocalMixes.Where(p => p.Active) : 
                ContextManager.LocalMixes.Where(p => p.Active == false);

            Selected = new ObservableCollection<MixViewModel>();
            foreach (var x in query)
                Selected.Add(new MixViewModel(x));
            OnPropertyChanged("Selected");
        }

        #region Binding Properties

        private ObservableCollection<MixViewModel> _selected;
        public ObservableCollection<MixViewModel> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        #endregion

        #region Commands

        protected override void cmdSearch_Execute()
        {
            var query = Selected.Where(p => p.Name.Contains(FilterName));
            _selected.Clear();
            foreach (var x in query)
                _selected.Add(x);
            OnPropertyChanged("Selected");
        }

        protected override void cmdSave_Execute()
        {
            foreach (var p in Selected)
                p.SaveDetails();
            base.cmdSave_Execute();
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            MixViewModel selected = argSelected as MixViewModel;
            foreach (var detail in selected.Details)
                _context.Mix_Details.Remove(_context.Mix_Details.Find(detail.Id));
            _context.Mixes.Remove(_context.Mixes.Find(selected.Id));
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            MixViewModel selected = argSelected as MixViewModel;
            selected.Active = !selected.Active;
            SaveContext();
        }

        protected override void cmdSelectActive_Execute()
        {
            var query = _context.Mixes.Local.Where(p => p.Active);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));

            _selected.Clear();
            foreach (var x in query)
                _selected.Add(new MixViewModel(x));
            OnPropertyChanged("MixesSelected");

            base.cmdSelectActive_Execute();
        }

        protected override void cmdSelectInactive_Execute()
        {
            var query = _context.Mixes.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));

            _selected.Clear();
            foreach (var x in query)
                _selected.Add(new MixViewModel(x));
            OnPropertyChanged("MixesSelected");

            base.cmdSelectInactive_Execute();
        }
        #endregion
    }


    //Helper class
    public class MixViewModel
    {
        private Mix _mix;

        public MixViewModel(Mix argMix)
        {
            _mix = argMix;

            Details = new ObservableCollection<Mix_Details>();
            foreach (var x in _mix.Mix_Details)
            {
                Details.Add(x);
            }
        }

        public void SaveDetails()
        {
            var context_Details = _mix.Mix_Details.ToList();
            foreach (var detail in Details)
            {
                if (!context_Details.Contains(detail))
                    _mix.Mix_Details.Add(detail);
            }
        }

        public int Id => _mix.Id;

        public string Name
        {
            get { return _mix.Name; }
            set { _mix.Name = value; }
        }

        public string Description
        {
            get { return _mix.Description; }
            set { _mix.Description = value; }
        }

        public bool Active
        {
            get { return _mix.Active; }
            set { _mix.Active = value; }
        }

        public ObservableCollection<Mix_Details> Details
        { get; set; }
    }
}
