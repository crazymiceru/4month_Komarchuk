using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hole
{
    internal sealed class DataObjects
    {
        private Dictionary<string, object> _data = new Dictionary<string, object>();
        public static DataObjects inst;

        internal DataObjects()
        {
            if (inst == null) inst = this;
        }

        public void Add<T>(string key, T value) where T : Object
        {
            _data.Add(key, value);
        }

        public T GetValue<T>(string key) where T : Object
        {
            if (!_data.ContainsKey(key))
            {
                var keyLoad=Load<T>(key);
                Add(key, keyLoad);
            }
            return _data[key] as T;
        }

        private T Load<T>(string key) where T: Object
        {
            var path = key;
            var l= Resources.Load<T>(Path.ChangeExtension(path, null));
            
            

            if (l==null) Debug.Log($"Resources dont load: {path}");
            return l;
        }

    }
}