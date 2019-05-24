using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElasticUi.Animation
{
    public struct AnimationTrigger
    {
        public int Id;
        public float Trigger;

        public bool IsActive;
        public bool BecameActiveThisFrame;
        public bool BecameInactiveThisFrame;

        public AnimationTrigger(int id, float trigger)
        {
            Id = id;
            Trigger = trigger;

            IsActive = false;
            BecameActiveThisFrame = false;
            BecameInactiveThisFrame = false;
        }
    }
}
