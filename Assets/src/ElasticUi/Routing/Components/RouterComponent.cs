using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElasticUi.Routing.Components
{
    public class RouterComponent : MonoBehaviour
    {
        public event Action<Location> OnLocationChanged;

        public Location CurrentLocation => _router.CurrentLocation;

        [SerializeField]
        private string _initialPath = "/";

        private Router _router = new Router();

        private void Start()
        {
            _router.OnLocationChanged += OnLocationChangedInternal;
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            yield return new WaitForEndOfFrame();
            _router.Push(_initialPath);
        }

        private void OnLocationChangedInternal(Location location)
        {
            OnLocationChanged?.Invoke(location);
        }

        public void Replace(string path)
        {
            _router.Replace(path);
        }

        public void ReplaceAll(string path)
        {
            _router.ReplaceAll(path);
        }

        public void Push(string path)
        {
            _router.Push(path);
        }

        public void Pop()
        {
            _router.Pop();
        }
    }
}
