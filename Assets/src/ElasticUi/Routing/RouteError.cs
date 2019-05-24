using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticUi.Routing
{
    public class RouteError : Exception
    {
        public RouteError(string message)
            :base(message)
        {
        }
    }
}
