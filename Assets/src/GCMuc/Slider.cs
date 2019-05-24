using ElasticUi.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GCMuc
{
    public class Slider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Spring _spring;

        [SerializeField]
        private Slider _follow;

        private bool _dragging;
        private Vector3 _dragStart;

        private AnimationValues _values;

        private int _xId;

        public void UpdateSpring(Spring spring)
        {
            _values.UpdateFloatSpring(_xId, spring);
        }

        public float GetValue()
        {
            return _values.GetValue(_xId);
        }

        private void Awake()
        {
            _values = new AnimationValues();
            _xId = _values.Add(0f, _spring);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_follow != null)
                return;

            _dragging = true;
            _dragStart = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_follow != null)
                return;

            _dragging = false;
        }

        private void Update()
        {
            if(_follow != null)
            {
                _values.SetTarget(_xId, _follow.GetValue());
            }
            else if(_dragging)
            {
                var delta = Input.mousePosition.x - _dragStart.x;
                _values.SetTarget(_xId, delta);
            }
            else
            {
                _values.SetTarget(_xId, 0f);
            }

            _values.Update(Time.deltaTime);

            var rt = GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _values.GetValue(_xId), 300f);
        }
    }
}
