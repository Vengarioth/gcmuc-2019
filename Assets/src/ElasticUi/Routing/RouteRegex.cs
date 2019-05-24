using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElasticUi.Routing
{
    public class RouteRegex
    {
        private readonly Regex _regex;

        public RouteRegex(Regex regex)
        {
            _regex = regex;
        }

        public bool Matches(string path, Dictionary<string, string> parameter)
        {
            if (!_regex.IsMatch(path))
            {
                return false;
            }

            foreach (Match m in _regex.Matches(path))
            {
                foreach (Group g in m.Groups)
                {
                    parameter.Add(g.Name, g.Value);
                }
            }

            return true;
        }
    }
}
