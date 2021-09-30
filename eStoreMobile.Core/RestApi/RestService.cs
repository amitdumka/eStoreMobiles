using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStoreMobile.Core.RestApi
{
    public class RestService<T>
    {
        private JsonSerializerOptions serializerOptions;
        private HttpClient client;
        private string restUrl;
        private string APIName;

        public RestService(string url, string name)
        {
            restUrl = url;
            APIName = name;
            client = new HttpClient ();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task DeleteAsync(int id)
        {
            Uri uri = new Uri (string.Format (restUrl, id));

            try
            {
                HttpResponseMessage response = await client.DeleteAsync (uri);

                if ( response.IsSuccessStatusCode )
                {
                    Debug.WriteLine ($@"\t{APIName} successfully deleted.");
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine ($@"\t{APIName}:\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteAsync(string id)
        {
            Uri uri = new Uri (string.Format (restUrl, id));

            try
            {
                HttpResponseMessage response = await client.DeleteAsync (uri);

                if ( response.IsSuccessStatusCode )
                {
                    Debug.WriteLine ($@"\t{APIName} successfully deleted.");
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine ($@"\t{APIName}:\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<T>> RefreshDataAsync()
        {
            List<T> Items = new List<T> ();

            Uri uri = new Uri (string.Format (restUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync (uri);
                if ( response.IsSuccessStatusCode )
                {
                    string content = await response.Content.ReadAsStringAsync ();
                    Items = JsonSerializer.Deserialize<List<T>> (content, serializerOptions);
                }
                else
                {
                    Debug.WriteLine ("Error occured!, No Data has returned");
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine (@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task SaveAsync(T item, bool isNewItem)
        {
            Uri uri = new Uri (string.Format (restUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<T> (item, serializerOptions);
                StringContent content = new StringContent (json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if ( isNewItem )
                {
                    response = await client.PostAsync (uri, content);
                }
                else
                {
                    response = await client.PutAsync (uri, content);
                }

                if ( response.IsSuccessStatusCode )
                {
                    Debug.WriteLine ($@"\t{APIName} successfully saved.");
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine ($@"\t{APIName}: \tERROR {0}", ex.Message);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            Uri uri = new Uri (string.Format (restUrl + $"/{id}", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync (uri);
                if ( response.IsSuccessStatusCode )
                {
                    string content = await response.Content.ReadAsStringAsync ();
                    return JsonSerializer.Deserialize<T> (content, serializerOptions);
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine (@"\tERROR {0}", ex.Message);
            }

            return default (T);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            Uri uri = new Uri (string.Format (restUrl + $"/{id}", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync (uri);
                if ( response.IsSuccessStatusCode )
                {
                    string content = await response.Content.ReadAsStringAsync ();
                    return JsonSerializer.Deserialize<T> (content, serializerOptions);
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine (@"\tERROR {0}", ex.Message);
            }

            return default (T);
        }

        public async Task<List<T>> FindAsync(string queryParams)
        {
            List<T> Items = new List<T> ();

            Uri uri = new Uri (string.Format (restUrl, string.Empty));
            StringContent content = new StringContent (queryParams, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync (uri, content);
                if ( response.IsSuccessStatusCode )
                {
                    string resContent = await response.Content.ReadAsStringAsync ();
                    Items = JsonSerializer.Deserialize<List<T>> (resContent, serializerOptions);
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine ($@"\t{APIName}:\tFind:\tERROR {0}", ex.Message);
            }

            return Items;
        }
    }

    public interface IRestService<T>
    {
        Task<List<T>> RefreshDataAsync();

        Task SaveAsync(T item, bool isNewItem);

        Task DeleteAsync(int id);

        Task DeleteAsync(string id);
    }
}