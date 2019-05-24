using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElasticUi.Routing.Components
{
    public class GoBackComponent : MonoBehaviour
    {
        public void Use()
        {
            var router = this.FindRouter();
            if (router == null)
                throw new Exception("No RouterComponent found in parents");

            router.Pop();
        }
    }
}
