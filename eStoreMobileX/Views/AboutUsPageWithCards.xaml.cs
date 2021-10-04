using eStoreMobileX.ViewModels.About;
using Syncfusion.ListView.XForms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace eStoreMobileX.Views
{
    /// <summary>
    /// About us with cards page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPageWithCards
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:eStoreMobileX.Views.AboutUsPageWithCards"/> class.
        /// </summary>
        public AboutUsPageWithCards()
        {
            this.InitializeComponent();
            this.BindingContext = AboutUsViewModel.BindingContext;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width < height)
            {
                if (this.employeesList.LayoutManager is GridLayout)
                {
                    (this.employeesList.LayoutManager as GridLayout).SpanCount = 2;
                }
            }
            else
            {
                if (this.employeesList.LayoutManager is GridLayout)
                {
                    (this.employeesList.LayoutManager as GridLayout).SpanCount = 4;
                }
            }
        }
    }
}