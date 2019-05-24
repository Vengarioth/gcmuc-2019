using ElasticUi.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GCMuc
{
    public class SlideToUnlock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Serializable]
        public class SlideToUnlockEvent : UnityEvent<bool> { }

        [Serializable]
        public class SlideToUnlockColorEvent : UnityEvent<Color> { }

        [SerializeField]
        private SlideToUnlockEvent _unlockChanged = new SlideToUnlockEvent();

        [SerializeField]
        private SlideToUnlockColorEvent _unlockColorChanged = new SlideToUnlockColorEvent();

        public SlideToUnlockEvent UnlockChanged
        {
            get => _unlockChanged;
            set => _unlockChanged = value;
        }

        public SlideToUnlockColorEvent UnlockColorChanged
        {
            get => _unlockColorChanged;
            set => _unlockColorChanged = value;
        }

        private Spring _spring;
        private AnimationValues _values;

        private int _xId;

        private bool _dragging;
        private Vector3 _dragStart;

        private bool _unlocked;

        public void OnPointerDown(PointerEventData eventData)
        {
            _dragging = true;
            _dragStart = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragging = false;
            var x = _values.GetValue(_xId);

            if(_unlocked && x < -100f)
            {
                _unlocked = false;
            }
            else if(!_unlocked && x >= 280f)
            {
                _unlocked = true;
                UnlockChanged.Invoke(true);
                UnlockColorChanged.Invoke(Color.green);
            }
        }

        private void Awake()
        {
            _spring = ScriptableObject.CreateInstance<Spring>();
            _values = new AnimationValues();
            _xId = _values.Add(0f, _spring);
        }

        private void Update()
        {
            if (_dragging)
            {
                var delta = Input.mousePosition - _dragStart;
                _values.SetTarget(_xId, delta.x);
            }
            else
            {
                _values.SetTarget(_xId, 0f);
            }

            _values.Update(Time.deltaTime);
            var x = _values.GetValue(_xId);

            var rt = GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, x, rt.rect.width);

            if (!_unlocked)
            {
                var c = Mathf.Min(Mathf.Max(x, 0f), 300f) / 300f;
                var color = Color.Lerp(Color.red, Color.green, c);
                UnlockColorChanged.Invoke(color);
            }
        }

        public void UpdateSpring(Spring spring)
        {
            _values.UpdateFloatSpring(_xId, spring);
        }
    }
}
