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
    public class SizeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private float _normalSize;
        [SerializeField]
        private float _expandedSize;

        private Spring _spring;
        private AnimationValues _values;
        private int _heightId;
        private RectTransform _rt;

        private void Awake()
        {
            _rt = GetComponent<RectTransform>();
            _spring = ScriptableObject.CreateInstance<Spring>();
            _values = new AnimationValues();
            _heightId = _values.Add(_rt.rect.height, _spring);
        }

        private void Update()
        {
            _values.Update(Time.deltaTime);
            var size = _rt.sizeDelta;
            size.y = _values.GetValue(_heightId);
            _rt.sizeDelta = size;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _values.SetTarget(_heightId, _expandedSize);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _values.SetTarget(_heightId, _normalSize);
        }

        public void UpdateSpring(Spring spring)
        {
            _values.UpdateFloatSpring(_heightId, spring);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var velocity = _values.GetVelocity(_heightId);
            velocity += 5000f;
            _values.SetVelocity(_heightId, velocity);
        }
    }
}
