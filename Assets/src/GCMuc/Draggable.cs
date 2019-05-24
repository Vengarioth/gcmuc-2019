using ElasticUi.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GCMuc
{
    public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Spring _spring;

        private AnimationValues _values;

        private int _xId;
        private int _yId;

        private bool _dragging;
        private Vector3 _dragStart;

        private bool _holdY;
        private bool _showVelocity;

        public void UpdateMass(float mass)
        {
            if (mass < Mathf.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass));

            _spring.Mass = mass;
            _values.UpdateFloatSpring(_xId, _spring);
            _values.UpdateFloatSpring(_yId, _spring);
        }

        public void UpdateTension(float tension)
        {
            if (tension < Mathf.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(tension));

            _spring.Tension = tension;
            _values.UpdateFloatSpring(_xId, _spring);
            _values.UpdateFloatSpring(_yId, _spring);
        }

        public void UpdateFriction(float friction)
        {
            if (friction < Mathf.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(friction));

            _spring.Friction = friction;
            _values.UpdateFloatSpring(_xId, _spring);
            _values.UpdateFloatSpring(_yId, _spring);
        }

        public void UpdateClamp(bool clamp)
        {
            _spring.Clamp = clamp;
            _values.UpdateFloatSpring(_xId, _spring);
            _values.UpdateFloatSpring(_yId, _spring);
        }

        public void UpdateShowVelocity(bool value)
        {
            _showVelocity = value;
        }


        public void UpdateHoldY(bool value)
        {
            _holdY = value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _dragging = true;
            _dragStart = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragging = false;
        }

        private void Awake()
        {
            _spring = ScriptableObject.CreateInstance<Spring>();
            _spring.Clamp = false;

            _values = new AnimationValues();
            _xId = _values.Add(0f, _spring);
            _yId = _values.Add(0f, _spring);
        }

        private void Update()
        {
            if (_dragging)
            {
                var delta = Input.mousePosition - _dragStart;
                _values.SetTarget(_xId, delta.x);
                if(_holdY)
                {
                    _values.SetTarget(_yId, 0f);
                }
                else
                {
                    _values.SetTarget(_yId, -delta.y);
                }
            }
            else
            {
                _values.SetTarget(_xId, 0f);
                _values.SetTarget(_yId, 0f);
            }

            _values.Update(Time.deltaTime);

            var rt = GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _values.GetValue(_xId), rt.rect.width);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, _values.GetValue(_yId), rt.rect.height);

            if(_showVelocity)
            {
                var factor = 2000f;
                var velocity = new Vector2(_values.GetVelocity(_xId), _values.GetVelocity(_yId)).magnitude;
                velocity = Mathf.Min(velocity, factor);
                velocity = Mathf.Max(0f, velocity);
                velocity /= factor;

                var color = Color.Lerp(Color.black, Color.red, velocity);
                GetComponent<Graphic>().color = color;
            }
        }
    }
}
