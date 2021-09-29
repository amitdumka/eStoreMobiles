using eStoreMobile.DataService;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace eStoreMobile.Views
{
    /// <summary>
    /// Page to display Task Notifications list.
    /// </summary>
    [Preserve (AllMembers = true)]
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class TaskNotificationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotificationPage" /> class.
        /// </summary>
        public TaskNotificationPage()
        {
            this.InitializeComponent ();
            this.BindingContext = TaskNotificationDataService.Instance.TaskNotificationPageViewModel;
        }
    }
}