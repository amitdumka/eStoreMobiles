using eStoreMobileX.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace eStoreMobileX.Views
{
    /// <summary>
    /// Page to show the health profile.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeProfilePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeProfilePage" /> class.
        /// </summary>
        public EmployeeProfilePage()
        {
            this.InitializeComponent();
            this.BindingContext = EmployeeProfilePageViewModel.BindingContext;
        }
    }
}