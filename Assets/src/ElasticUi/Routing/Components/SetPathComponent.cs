using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElasticUi.Routing.Components
{
    public class SetPathComponent : MonoBehaviour
    {
#pragma warning disable CS0649
        [SerializeField]
        private UnityEngine.UI.Text _target;
#pragma warning restore CS0649

        private RouterComponent _router;

        public void Start()
        {
            _router = this.FindRouter();
            _router.OnLocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(Location location)
        {
            _target.text = location.Path;
        }
    }
}
