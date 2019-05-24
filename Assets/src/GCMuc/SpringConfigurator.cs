using ElasticUi.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace GCMuc
{
    public class SpringConfigurator : MonoBehaviour
    {
        [Serializable]
        public class SpringUpdatedEvent : UnityEvent<Spring> {}

        [SerializeField]
        private SpringUpdatedEvent _springUpdated = new SpringUpdatedEvent();

        private Spring _spring;
        public Spring Spring => _spring;

        public SpringUpdatedEvent SpringUpdated
        {
            get => _springUpdated;
            set => _springUpdated = value;
        }

        private void Awake()
        {
            _spring = ScriptableObject.CreateInstance<Spring>();
        }

        public void UpdateMass(float mass)
        {
            if (mass < Mathf.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(mass));

            _spring.Mass = mass;
            _springUpdated.Invoke(_spring);
        }

        public void UpdateTension(float tension)
        {
            if (tension < Mathf.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(tension));

            _spring.Tension = tension;
            _springUpdated.Invoke(_spring);
        }

        public void UpdateFriction(float friction)
        {
            if (friction < Mathf.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(friction));

            _spring.Friction = friction;
            _springUpdated.Invoke(_spring);
        }

        public void UpdateClamp(bool clamp)
        {
            _spring.Clamp = clamp;
            _springUpdated.Invoke(_spring);
        }
    }
}
