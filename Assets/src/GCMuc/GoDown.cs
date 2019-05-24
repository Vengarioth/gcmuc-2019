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
    public class GoDown : RouteBehaviour
    {
        [SerializeField]
        private string _nextRoute;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if (!GetRoute().Active)
                return;

            if (Input.GetButtonDown("Down"))
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
