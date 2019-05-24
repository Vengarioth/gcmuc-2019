using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticUi.Routing
{
    public class Location
    {
        public string Path { get; }

        public Location(string path)
        {
            Path = path;
        }
    }
}
