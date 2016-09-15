﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinMix : Abstract.aEntityWindowViewModel
    {
        public vmWinMix(object argMix = null)
        {
            if (argMix != null)
            {
                Mix = argMix as Mix;
                //Details = new ObservableCollection<Mix_Details>(Mix.Mix_Details);
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

        protected override void OnSaveValidation()
        {
            double total = Details.Sum(x => x.Ratio);
            if (total != 100)
                throw new Exception("Неправильные пропорции, общая сумма не равна 100%");
        }

        protected override void OnSaveCreate()
        {
            _mix.Mix_Details.Clear();
            foreach (var x in Details)
            {
                _mix.Mix_Details.Add(x);
                x.Ratio /= 100;
            }
            ContextManager.Context.Mixes.Add(_mix);
        }

        #endregion
    }
}