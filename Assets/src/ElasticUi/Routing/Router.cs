using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElasticUi.Routing
{
    public class Router
    {
        public event Action<Location> OnLocationChanged;

        public Location CurrentLocation => _frames.Count > 0 ? _frames.Peek() : null;

        private Stack<Location> _frames;

        public Router()
        {
            _frames = new Stack<Location>();
        }

        public void ReplaceAll(string path)
        {
            _frames.Clear();
            _frames.Push(new Location(path));
            OnLocationChanged?.Invoke(CurrentLocation);
        }

        public void Replace(string path)
        {
            if (_frames.Count > 0)
            {
                _frames.Pop();
            }

            _frames.Push(new Location(path));
            OnLocationChanged?.Invoke(CurrentLocation);
        }
        
        public void Push(string path)
        {
            _frames.Push(new Location(path));

            OnLocationChanged?.Invoke(CurrentLocation);
        }

        public void Pop()
        {
            if (_frames.Count <= 1)
                return;

            _frames.Pop();
            OnLocationChanged?.Invoke(CurrentLocation);
        }
    }
}
