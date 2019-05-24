using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GCMuc
{
    public class Circle : Graphic
    {
        private const float ANTIALIASING_SIZE = 1f;

        [SerializeField]
        private int _segments = 64;

        [SerializeField]
        private float _width = 1f;

        [SerializeField]
        private float _offset = 1f;

        [SerializeField]
        private float _fill = 90f;

        [SerializeField]
        private float _rotationSpeed = 0f;

        public float RotationSpeed => _rotationSpeed;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            var antiAliasingColor = color;
            antiAliasingColor.a = 0f;
            var rad90deg = 90f * Mathf.Deg2Rad;

            var indexBase = vh.currentVertCount;

            var circleStart = Mathf.Deg2Rad * 0f;
            var circleEnd = Mathf.Deg2Rad * _fill;

            var beta = circleStart;
            var direction = new Vector2(Mathf.Cos(beta), Mathf.Sin(beta)).normalized;
            var perpendicularDirection = new Vector2(Mathf.Cos(beta - rad90deg), Mathf.Sin(beta - rad90deg)).normalized;

            vh.AddVert((direction * (_offset - ANTIALIASING_SIZE)) + (perpendicularDirection * ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);
            vh.AddVert((direction * _offset) + (perpendicularDirection * ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);
            vh.AddVert((direction * (_offset + _width)) + (perpendicularDirection * ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);
            vh.AddVert((direction * (_offset + _width + ANTIALIASING_SIZE)) + (perpendicularDirection * ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);

            vh.AddVert(direction * (_offset - ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);
            vh.AddVert(direction * _offset, color, Vector2.zero);
            vh.AddVert(direction * (_offset + _width), color, Vector2.zero);
            vh.AddVert(direction * (_offset + _width + ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);

            // vh.AddTriangle(indexBase + 0, indexBase + 4, indexBase + 1);
            vh.AddTriangle(indexBase + 1, indexBase + 4, indexBase + 5);

            vh.AddTriangle(indexBase + 1, indexBase + 5, indexBase + 2);
            vh.AddTriangle(indexBase + 2, indexBase + 5, indexBase + 6);

            vh.AddTriangle(indexBase + 2, indexBase + 6, indexBase + 7);
            // vh.AddTriangle(indexBase + 2, indexBase + 7, indexBase + 3);

            indexBase += 4;

            for (var i = 0; i < _segments; i++)
            {
                beta += (circleEnd - circleStart) / _segments;
                direction = new Vector2(Mathf.Cos(beta), Mathf.Sin(beta)).normalized;

                vh.AddVert(direction * (_offset - ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);
                vh.AddVert(direction * _offset, color, Vector2.zero);
                vh.AddVert(direction * (_offset + _width), color, Vector2.zero);
                vh.AddVert(direction * (_offset + _width + ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);

                vh.AddTriangle(indexBase + 0, indexBase + 4, indexBase + 1);
                vh.AddTriangle(indexBase + 1, indexBase + 4, indexBase + 5);

                vh.AddTriangle(indexBase + 1, indexBase + 5, indexBase + 2);
                vh.AddTriangle(indexBase + 2, indexBase + 5, indexBase + 6);

                vh.AddTriangle(indexBase + 2, indexBase + 6, indexBase + 7);
                vh.AddTriangle(indexBase + 2, indexBase + 7, indexBase + 3);

                indexBase += 4;
            }

            perpendicularDirection = new Vector2(Mathf.Cos(beta - rad90deg), Mathf.Sin(beta - rad90deg)).normalized;

            vh.AddVert((direction * (_offset - ANTIALIASING_SIZE)) - (perpendicularDirection * ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);
            vh.AddVert((direction * _offset) - (perpendicularDirection * ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);
            vh.AddVert((direction * (_offset + _width)) - (perpendicularDirection * ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);
            vh.AddVert((direction * (_offset + _width + ANTIALIASING_SIZE)) - (perpendicularDirection * ANTIALIASING_SIZE), antiAliasingColor, Vector2.zero);

            // vh.AddTriangle(indexBase + 0, indexBase + 4, indexBase + 5);
            vh.AddTriangle(indexBase + 0, indexBase + 5, indexBase + 1);

            vh.AddTriangle(indexBase + 1, indexBase + 5, indexBase + 2);
            vh.AddTriangle(indexBase + 2, indexBase + 5, indexBase + 6);

            vh.AddTriangle(indexBase + 2, indexBase + 6, indexBase + 3);
            // vh.AddTriangle(indexBase + 3, indexBase + 6, indexBase + 7);
        }
    }
}
