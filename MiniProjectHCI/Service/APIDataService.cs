using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniProjectHCI.Service
{
    public class APIDataService : IDataService
    {
        public IEnumerable<DataModel> GetData(int numOfData)
        {
            using (WebClient client = new WebClient())
            {
                string requestUri = "https://www.alphavantage.co/query?function=CPI&interval=monthly&apikey=3VQ5NWO33Y2HB77U";

                string apiResponse = client.DownloadString(requestUri);

                APIDataListing listing = new JavaScriptSerializer().Deserialize<APIDataListing>(apiResponse);

                return (numOfData == 0) ?
                listing.data.Select(d => new DataModel()
                {
                    Label = d.date,
                    Value = double.Parse(d.value)
                }) :
                listing.data.Take(numOfData).Select(d => new DataModel()
                {
                    Label = d.date,
                    Value = double.Parse(d.value)
                });
            }
        }
    }
}
