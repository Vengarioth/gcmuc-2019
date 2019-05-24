using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticUi.Routing;
using UnityEngine;

namespace GCMuc
{
    public class GoNext : RouteBehaviour
    {
        [SerializeField]
        private string _nextRoute;

        private void Update()
        {
            if (!GetRoute().Active)
                return;

            if (Input.GetButtonDown("Next"))
            {
                StartCoroutine(EndOfFrame());
            }
        }

        public void Go()
        {
            if (!GetRoute().Active)
                return;

            StartCoroutine(EndOfFrame());
        }

        private IEnumerator EndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            this.FindRouter().Push(_nextRoute);
        }
    }
}
