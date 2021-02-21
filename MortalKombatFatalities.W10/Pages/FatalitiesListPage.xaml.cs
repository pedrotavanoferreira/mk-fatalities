//---------------------------------------------------------------------------
//
// <copyright file="FatalitiesListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>4/18/2017 4:16:30 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.LocalStorage;
using MortalKombatFatalities.Sections;
using MortalKombatFatalities.ViewModels;
using AppStudio.Uwp;

namespace MortalKombatFatalities.Pages
{
    public sealed partial class FatalitiesListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public FatalitiesListPage()
        {
			ViewModel = ViewModelFactory.NewList(new FatalitiesSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("f109e908-c022-4015-9015-9679b0b69a70");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
