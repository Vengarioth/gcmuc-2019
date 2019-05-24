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
    public class GoBack : RouteBehaviour
    {
        private void Update()
        {
            if (!GetRoute().Active)
                return;

            if (Input.GetButtonDown("Back"))
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
            this.FindRouter().Pop();
        }
    }
}
