using ElasticUi.Routing.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElasticUi.Routing
{
    public static class RouterExtensions
    {
        public static RouterComponent FindRouter(this MonoBehaviour self)
        {
            return self.GetComponentInParent<RouterComponent>();
        }

        public static Location FindCurrentLocation(this MonoBehaviour self)
        {
            return self.FindRouter().CurrentLocation;
        }
    }
}