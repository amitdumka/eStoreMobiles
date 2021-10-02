using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class AddAttendanceView : ContentPage
    {
        public AddAttendanceView()
        {
            InitializeComponent ();
        }
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if ( await DisplayAlert ("Alert", "Do you want to save!", "Yes", "No") )
            {
                //Go Back
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            if ( await DisplayAlert ("Alert", "Are your sure to go back!", "Yes", "No") )
            {
                //Go Back
            }

        }

        private void isTailoringSwitch_StateChanged(object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {

        }
    }
}