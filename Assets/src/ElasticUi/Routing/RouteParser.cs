using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElasticUi.Routing
{
    public static class RouteParser
    {
        private class Settings
        {
            public bool Exact { get; set; }
        }

        public static RouteRegex Parse(string path, bool exact = false)
        {
            var state = new ParseState(path);
            var settings = new Settings
            {
                Exact = exact,
            };

            var sb = new StringBuilder();
            ParseRoot(state, sb, settings);
            var regex = new Regex(sb.ToString());

            return new RouteRegex(regex);
        }

        private static void ParseRoot(ParseState state, StringBuilder sb, Settings settings)
        {
            if (state.EndReached)
                throw new RouteError("Routes cannot be empty");

            if (!state.CompareCurrent('/'))
                throw new RouteError("Routes must start with a forward slash \"/\"");

            sb.Append("^");

            ParseNext(state, sb, settings);

            if(settings.Exact)
            {
                sb.Append("$");
            }
            else
            {
                sb.Append("(/[^/]+)*?$");
            }
        }

        private static void ParseNext(ParseState state, StringBuilder sb, Settings settings)
        {
            while(!state.EndReached)
            {
                if (state.Eat('/'))
                {
                    sb.Append("/");
                    ParseSegment(state, sb, settings);
                }
            }
        }

        private static void ParseSegment(ParseState state, StringBuilder sb, Settings settings)
        {
            var start = state.Position;
            while (!state.CompareCurrent('/') && !state.EndReached)
            {
                if (state.CompareCurrent('{'))
                {
                    ParseParameter(state, sb, settings);
                    continue;
                }
                else
                {
                    ParseStatic(state, sb, settings);
                    continue;
                }
            }
            var until = state.Position;

            var part = state.GetSubString(start, until);
        }

        private static void ParseStatic(ParseState state, StringBuilder sb, Settings settings)
        {
            var start = state.Position;
            while (!state.CompareCurrent('{'))
            {
                if (state.CompareCurrent('/'))
                    break;

                if (state.Next())
                    break;
            }
            var until = state.Position;

            var part = state.GetSubString(start, until);
            sb.Append(part);
        }

        private static void ParseParameter(ParseState state, StringBuilder sb, Settings settings)
        {
            if (!state.Eat('{'))
                throw new RouteError("Parameters must start with an \"{\"");

            var start = state.Position;
            while (!state.CompareCurrent('}'))
            {
                if (state.Next())
                    break;
            }

            var until = state.Position;

            if (!state.Eat('}'))
                throw new RouteError("Parameters must end with an \"}\"");


            var part = state.GetSubString(start, until);
            sb.AppendFormat("(?<{0}>[^/]+)", part);
        }
    }
}
