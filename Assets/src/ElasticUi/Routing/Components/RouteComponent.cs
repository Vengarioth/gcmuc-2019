using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace ElasticUi.Routing.Components
{
    public class RouteComponent : MonoBehaviour
    {
        [Serializable]
        public class RouteChangedEvent : UnityEvent<Route> { }

        public RouteChangedEvent RouteChanged => _routeChanged;
        public Route Route => _route;

#pragma warning disable CS0649
        [SerializeField]
        private RouteChangedEvent _routeChanged = new RouteChangedEvent();

        [SerializeField]
        private string _path = "/";

        [SerializeField]
        private bool _exact = false;
#pragma warning restore CS0649

        private Route _route;
        private bool _wasActive;

        public string GetParameter(string parameterName)
        {
            return _route.GetParameter(parameterName);
        }

        private void Awake()
        {
            var regex = RouteParser.Parse(_path, _exact);
            _route = new Route(_path, regex);

            var router = this.FindRouter();
            if (router == null)
                throw new Exception("No RouterComponent found in parents");

            router.OnLocationChanged += OnLocationChanged;

            OnLocationChanged(router.CurrentLocation);
        }

        private void OnLocationChanged(Location location)
        {
            if (location == null)
                return;

            _route.Update(location);
            _routeChanged.Invoke(_route);
        }
    }
}
