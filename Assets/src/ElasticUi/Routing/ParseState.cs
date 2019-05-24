using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticUi.Routing
{
    public class ParseState
    {
        public int Position { get; private set; }
        public char Current { get => _template[Position]; }
        public bool EndReached { get => Position >= _template.Length; }

        private readonly string _template;

        public ParseState(string template)
        {
            _template = template;
            Position = 0;
        }

        public bool Next()
        {
            Position += 1;
            return EndReached;
        }

        public bool CompareCurrent(char compareTo)
        {
            if (EndReached)
                return false;
            return Current == compareTo;
        }

        public bool CompareNext(char compareTo)
        {
            if (Position + 1 >= _template.Length)
                return false;
            return _template[Position + 1] == compareTo;
        }

        public string GetSubString(int start, int end)
        {
            return _template.Substring(start, end - start);
        }

        public bool Eat(char value)
        {
            if (Current == value)
            {
                Position += 1;
                return true;
            }

            return false;
        }

        public bool EatWhitespace()
        {
            if (char.IsWhiteSpace(Current))
            {
                Position += 1;
                return true;
            }

            return false;
        }
    }
}
