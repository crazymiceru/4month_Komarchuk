using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Hole
{
    internal class SaveDataBinary : ISave
    {
        private BinaryFormatter _formatter;

        internal SaveDataBinary()
        {
            _formatter = new BinaryFormatter();
            SurrogateSelector surrogateSelector = new SurrogateSelector();
            Vector3SerializationSurrogate vector3SS = new Vector3SerializationSurrogate();
            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3SS);
            _formatter.SurrogateSelector = surrogateSelector;
        }

        public List<T> Load<T>(string name)
        {
            List<T> result;
            if (!File.Exists(name)) return default(List<T>);
            using (var fs = new FileStream(name, FileMode.Open))
            {
                result = (List<T>)_formatter.Deserialize(fs);
            }
            return result;
        }

        public void Save<T>(List<T> data, string name)
        {
            if (data == null && !String.IsNullOrEmpty(name)) return;
            if (!typeof(T).IsSerializable)
            {
                Debug.Log($"Data {typeof(T)} not Serializable for Save");
                return;
            }

            using (var fs = new FileStream(name, FileMode.Create))
            {
                _formatter.Serialize(fs, data);
            }
        }
    }
}