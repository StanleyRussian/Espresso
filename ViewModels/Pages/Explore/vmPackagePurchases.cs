﻿using System.Collections.ObjectModel;
using System.Linq;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmPackagePurchases: aProcessListingViewModel
    {
        public vmPackagePurchases()
        {
            Header = "Закупки упаковки";
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.PackagePurchases.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterSupplier != null)
                query = query.Where(p => p.Supplier.Id == FilterSupplier.Id);
            if (FilterAccount != null)
                query = query.Where(p => p.Account.Id == FilterAccount.Id);
            if (FilterPackage != null)
                query = query.Where(p => p.Package.Id == FilterPackage.Id);
            Tabs = new ObservableCollection<PackagePurchase>(query);
        }

        private ObservableCollection<PackagePurchase> _tabs;
        public ObservableCollection<PackagePurchase> Tabs
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

        private Supplier _filterSupplier;
        public Supplier FilterSupplier
        {
            get { return _filterSupplier; }
            set
            {
                _filterSupplier = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private Account _filterAccount;
        public Account FilterAccount
        {
            get { return _filterAccount; }
            set
            {
                _filterAccount = value;
                OnPropertyChanged();
                Refresh();
            }
        }


        protected override async void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as PackagePurchase;

            var messageDialogResult = await DialogCoordinator.Instance.ShowMessageAsync(this, "Подтверждение",
                    "Вы уверены, что хотите удалить закупку уапковки " + selected.Package.Name + " за " + selected.Date.Date + " число?",
                MessageDialogStyle.AffirmativeAndNegative);
            if (messageDialogResult == MessageDialogResult.Negative) return;

            ContextManager.Context.PackagePurchases.Remove(selected);
            SaveContext();
            Refresh();

        }
    }
}
