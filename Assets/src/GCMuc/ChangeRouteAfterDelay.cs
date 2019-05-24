using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticUi.Routing;
using UnityEngine;

namespace GCMuc
{
    public enum ChangeRouteMode
    {
        Push,
        Replace,
        ReplaceAll,
    }

    public class ChangeRouteAfterDelay : RouteBehaviour
    {
        [SerializeField]
        private float _time = 1f;

        [SerializeField]
        private string _route = "/";

        [SerializeField]
        private ChangeRouteMode _mode = ChangeRouteMode.Push;

        protected override void OnRouteEnter(Route route)
        {
            StartCoroutine(SwitchRouteIn(_time, _route, _mode));
        }

        private IEnumerator SwitchRouteIn(float time, string route, ChangeRouteMode mode)
        {
            yield return new WaitForSeconds(time);
            switch(mode)
            {
                case ChangeRouteMode.Push:
                    this.FindRouter().Push(route);
                    break;

                case ChangeRouteMode.Replace:
                    this.FindRouter().Replace(route);
                    break;

                case ChangeRouteMode.ReplaceAll:
                    this.FindRouter().ReplaceAll(route);
                    break;
            }
        }
    }
}
