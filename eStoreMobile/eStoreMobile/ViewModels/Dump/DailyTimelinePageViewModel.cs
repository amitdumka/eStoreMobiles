using eStoreMobile.Models;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace eStoreMobile.ViewModels
{
    /// <summary>
    /// ViewModel for Daily Timeline page.
    /// </summary>
    [Preserve (AllMembers = true)]
    [DataContract]
    public class DailyTimelinePageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="DailyTimelinePageViewModel"/> class.
        /// </summary>
        public DailyTimelinePageViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a collction of value to be displayed in Daily timeline page.
        /// </summary>
        [DataMember (Name = "dailyTimeline")]
        public ObservableCollection<Timeline> DailyTimeline { get; set; }

        #endregion
    }
}
