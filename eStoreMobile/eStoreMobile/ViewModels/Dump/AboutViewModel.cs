using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
 //Default
namespace eStoreMobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command (async () => await Browser.OpenAsync ("https://www.aprajitaretails.in"));
        }

        public ICommand OpenWebCommand { get; }
    }
}