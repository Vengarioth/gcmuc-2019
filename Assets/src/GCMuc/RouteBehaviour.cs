using ElasticUi.Routing;
using ElasticUi.Routing.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GCMuc
{
    public class RouteBehaviour : MonoBehaviour
    {
        private RouteComponent _routeComponent;

        protected Route GetRoute()
        {
            return _routeComponent.Route;
        }

        protected virtual void Initialize()
        {
        }

        private void OnEnable()
        {
            Subscribe();
        }

        protected virtual void Awake()
        {
            Subscribe();
        }

        protected virtual void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            Unsubscribe();

            _routeComponent = this.FindRoute();
            _routeComponent.RouteChanged.AddListener(OnRouteChangedInternal);

            Initialize();

            if (_routeComponent.Route != null)
            {
                OnRouteChangedInternal(_routeComponent.Route);
            }
        }

        private void Unsubscribe()
        {
            if (_routeComponent != null)
            {
                _routeComponent.RouteChanged.RemoveListener(OnRouteChangedInternal);
                _routeComponent = null;
            }
        }

        private void OnRouteChangedInternal(Route route)
        {
            if(route.Active)
            {
                OnRouteEnter(route);
            }
            else
            {
                OnRouteExit(route);
            }

            OnRouteChanged(route);
        }

        protected virtual void OnRouteChanged(Route route)
        {
        }

        protected virtual void OnRouteEnter(Route route)
        {
        }

        protected virtual void OnRouteExit(Route route)
        {
        }
    }
}
