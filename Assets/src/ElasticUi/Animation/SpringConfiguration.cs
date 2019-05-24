using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticUi.Animation
{
    public struct SpringConfiguration
    {
        public float Mass;
        public float Tension;
        public float Friction;
        public float Precision;
        public bool Clamp;
    }
}
