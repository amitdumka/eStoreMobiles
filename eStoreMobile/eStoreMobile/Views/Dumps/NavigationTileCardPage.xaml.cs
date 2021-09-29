﻿using eStoreMobile.DataService;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    [Preserve (AllMembers = true)]
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class NavigationTileCardPage
    {
        public NavigationTileCardPage()
        {
            this.InitializeComponent ();
            this.BindingContext = NavigationDataService.Instance.NavigationViewModel;
        }
    }
}