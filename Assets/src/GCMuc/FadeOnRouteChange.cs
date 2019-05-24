using ElasticUi.Animation;
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
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeOnRouteChange : RouteBehaviour
    {
        [SerializeField]
        private Spring _spring;

        private CanvasGroup _canvasGroup;
        private AnimationValues _values;
        private int _leaveTrigger;
        private int _opacityId;

        protected override void Initialize()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _values = new AnimationValues(1);
            _opacityId = _values.Add(0f, _spring);
            _leaveTrigger = _values.AddTrigger(_opacityId, 0f);
        }

        private void Update()
        {
            _values.Update(Time.deltaTime);

            if(_values.BecameActiveThisFrame(_leaveTrigger))
            {
                for(var i = 0; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i);
                    child.gameObject.SetActive(false);
                }
            }

            if (_values.BecameInactiveThisFrame(_leaveTrigger))
            {
                for (var i = 0; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i);
                    child.gameObject.SetActive(true);
                }
            }

            _canvasGroup.alpha = _values.GetValue(_opacityId);
        }

        protected override void OnRouteEnter(Route route)
        {
            _values.SetTarget(_opacityId, 1f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        protected override void OnRouteExit(Route route)
        {
            _values.SetTarget(_opacityId, 0f);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
