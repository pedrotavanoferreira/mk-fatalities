//---------------------------------------------------------------------------
//
// <copyright file="FatalitiesDetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>4/18/2017 4:16:30 PM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.LocalStorage;
using MortalKombatFatalities.Sections;
using MortalKombatFatalities.Navigation;
using MortalKombatFatalities.ViewModels;
using AppStudio.Uwp;

namespace MortalKombatFatalities.Pages
{
    public sealed partial class FatalitiesDetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public FatalitiesDetailPage()
        {
            ViewModel = ViewModelFactory.NewDetail(new FatalitiesSection());
            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
        }

        public DetailViewModel ViewModel { get; set; }        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadStateAsync(e.Parameter as NavDetailParameter);

            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }
    }
}
