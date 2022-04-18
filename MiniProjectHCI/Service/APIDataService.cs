using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniProjectHCI.Service
{
    public class APIDataService : IDataService
    {
        public async Task<IEnumerable<DataModel>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = "https://www.alphavantage.co/query?function=CPI&interval=monthly&apikey=3VQ5NWO33Y2HB77U";

                HttpResponseMessage apiResponse = await client.GetAsync(requestUri);

                string jsonResponse = await apiResponse.Content.ReadAsStringAsync();

                APIDataListing listing = new JavaScriptSerializer().Deserialize<APIDataListing>(jsonResponse);

                return listing.data.Select(d => new DataModel()
                {
                    Date = DateOnly.Parse(d.date),
                    Value = Double.Parse(d.value)
                });
            }
        }
    }
}
