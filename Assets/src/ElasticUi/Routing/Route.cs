using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace ElasticUi.Routing
{
    public class Route
    {
        public bool Active { get; private set; }
        public string Path { get; }

        private readonly Dictionary<string, string> _parameter;
        private readonly RouteRegex _regex;

        public Route(string path, RouteRegex regex)
        {
            Path = path;
            _regex = regex;
            _parameter = new Dictionary<string, string>();
        }

        public string GetParameter(string parameterName)
        {
            if (!_parameter.ContainsKey(parameterName))
                return null;
            return _parameter[parameterName];
        }

        public void Update(Location location)
        {
            _parameter.Clear();
            Active = _regex.Matches(location.Path, _parameter);
        }
    }
}
