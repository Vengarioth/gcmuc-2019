using ElasticUi.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GCMuc
{
    public class GoUp : RouteBehaviour
    {
        [SerializeField]
        private string _nextRoute;

        private void Update()
        {
            if (!GetRoute().Active)
                return;

            if (Input.GetButtonDown("Up"))
            {
                StartCoroutine(EndOfFrame());
            }
        }

        private IEnumerator EndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            this.FindRouter().Push(_nextRoute);
        }
    }
}
