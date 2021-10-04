using eStoreMobileX.ViewModels;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Xamarin.Forms.Internals;

namespace eStoreMobileX.DataService
{
    /// <summary>
    /// Data service to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CompanyHistoryDataService
    {
        #region fields

        private static CompanyHistoryDataService companyHistoryDataService;

        private CompanyHistoryPageViewModel companyHistoryViewModel;

        #endregion

        #region Properties

        /// <summary>
        /// Gets an instance of the <see cref="CompanyHistoryDataService"/>.
        /// </summary>
        public static CompanyHistoryDataService Instance => companyHistoryDataService ?? (companyHistoryDataService = new CompanyHistoryDataService());

        /// <summary>
        /// Gets or sets the value of company history page view model.
        /// </summary>
        public CompanyHistoryPageViewModel CompanyHistoryPageViewModel =>
            this.companyHistoryViewModel ??
            (this.companyHistoryViewModel = PopulateData<CompanyHistoryPageViewModel>("timeline.json"));

        #endregion

        #region Methods

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            var file = "eStoreMobileX.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        #endregion
    }
}
