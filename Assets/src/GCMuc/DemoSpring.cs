using ElasticUi.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GCMuc
{
    public class DemoSpring : MonoBehaviour
    {
        [SerializeField]
        private Spring _spring;

        [SerializeField]
        private float _target;

        private AnimationValues _values;
        private int _id;

        private void Awake()
        {
            _values = new AnimationValues();
            _id = _values.Add(_target, _spring);
        }

        private void Update()
        {
            // Update spring every frame to make it configurable via editor
            _values.UpdateFloatSpring(_id, _spring);
            _values.SetTarget(_id, _target);
            _values.Update(Time.deltaTime);
        }

        public void SetTarget(float value)
        {
            _target = value;
        }

        public float GetSpringValue()
        {
            return _values.GetValue(_id);
        }
    }
}
