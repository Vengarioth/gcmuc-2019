using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace ElasticUi.Animation
{
    [CreateAssetMenu(fileName = "Spring", menuName = "Ui/Spring", order = 1)]
    public class Spring : ScriptableObject
    {
        public float Mass = 1f;
        public float Tension = 170f;
        public float Friction = 26f;
        public float Precision = 0.01f;
        public bool Clamp = true;

        public SpringConfiguration GetConfiguration()
        {
            return new SpringConfiguration
            {
                Mass = Mass,
                Tension = Tension,
                Friction = Friction,
                Precision = Precision,
                Clamp = Clamp,
            };
        }
    }
}
