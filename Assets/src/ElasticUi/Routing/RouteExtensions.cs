using ElasticUi.Routing.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElasticUi.Routing
{
    public static class RouteExtensions
    {
        public static RouteComponent FindRoute(this MonoBehaviour self)
        {
            var route = self.GetComponent<RouteComponent>();
            if (route != null)
            {
                return route;
            }

            return self.GetComponentInParent<RouteComponent>();
        }
    }
}
