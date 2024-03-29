﻿using eStoreMobile.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    /// <summary>
    /// Page to show the no internet connection error
    /// </summary>
    [Preserve (AllMembers = true)]
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class NoInternetConnectionPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoInternetConnectionPage" /> class.
        /// </summary>
        public NoInternetConnectionPage()
        {
            this.InitializeComponent ();
            this.BindingContext = NoInternetConnectionPageViewModel.BindingContext;
        }
    }
}