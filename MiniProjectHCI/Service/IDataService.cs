using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectHCI.Service
{
    public interface IDataService
    {
        string Unit { get; set; }
        IEnumerable<DataModel> GetData(string function, string interval);
    }
}
