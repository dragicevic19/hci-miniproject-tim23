using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        Dictionary<string, string> dataTypeToFunction;
           
        public APIDataService()
        {
            dataTypeToFunction = new Dictionary<string, string>();
            dataTypeToFunction.Add("CPI", "CPI");
            dataTypeToFunction.Add("Inflation", "INFLATION");
            dataTypeToFunction.Add("Retail Sales", "RETAIL_SALES"); 
        }

        public IEnumerable<DataModel> GetData(string function, string interval="")
        {
            using (WebClient client = new WebClient())
            {

                string intervalQuery = (function == "CPI") ? ("&interval=" + interval) : "";

                string requestUri = "https://www.alphavantage.co/query?function=" + dataTypeToFunction[function] + intervalQuery + "&apikey=3VQ5NWO33Y2HB77U";

                string apiResponse = client.DownloadString(requestUri);
                
                APIDataListing listing = new JavaScriptSerializer().Deserialize<APIDataListing>(apiResponse);
                
                return
                listing.data.Select(d => new DataModel()
                {
                    Label = d.date,
                    Value = double.Parse(d.value, CultureInfo.InvariantCulture)
                });

                


            }
        }
    }
}
