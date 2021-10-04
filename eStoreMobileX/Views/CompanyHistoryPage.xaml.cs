using eStoreMobileX.DataService;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace eStoreMobileX.Views
{
    /// <summary>
    /// Page to show Company History.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyHistoryPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyHistoryPage" /> class.
        /// </summary>
        public CompanyHistoryPage()
        {
            this.InitializeComponent();
            this.BindingContext = CompanyHistoryDataService.Instance.CompanyHistoryPageViewModel;
        }
    }
}