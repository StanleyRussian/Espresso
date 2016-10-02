using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinMix : Abstract.aEntityWindowViewModel
    {
        public vmWinMix(object argEntity) : base(argEntity) { }

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

        protected override void OnOpenEdit(object argEntity)
        {
            Mix = argEntity as Mix;
        }

        protected override void OnOpenNew()
        {
            Mix = new Mix();
            Details = new ObservableCollection<Mix_Details>();
        }

        protected override void OnSaveValidation()
        {
            if (!CreatingNew) return;
            double total = Details.Sum(x => x.Ratio);
            if (total != 100)
                throw new Exception("Неправильные пропорции, общая сумма не равна 100%");
        }

        protected override void OnSaveEdit() { }

        protected override void OnSaveNew()
        {
            _mix.Mix_Details.Clear();
            foreach (var x in Details)
            {
                _mix.Mix_Details.Add(x);
                x.Ratio /= 100;
            }
            ContextManager.Context.Mixes.Add(_mix);
        }
    }
}