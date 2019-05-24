using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GCMuc
{
    public class CircleLoader : MonoBehaviour
    {
        private Circle[] _children;

        private void Awake()
        {
            _children = GetComponentsInChildren<Circle>();
        }

        private void Update()
        {
            foreach(var child in _children)
            {
                child.rectTransform.Rotate(new Vector3(0f, 0f, Time.deltaTime * child.RotationSpeed));
            }
        }
    }
}
