using eStoreMobile.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    /// <summary>
    /// Page to show recent chat list.
    /// </summary>
    [Preserve (AllMembers = true)]
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class RecentChatPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentChatPage" /> class.
        /// </summary>
        public RecentChatPage()
        {
            this.InitializeComponent ();
            this.BindingContext = RecentChatPageViewModel.BindingContext;
        }
    }
}