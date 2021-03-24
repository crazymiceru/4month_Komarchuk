using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hole
{
    public sealed class SaveDataRepository
    {
        public static SaveDataRepository inst;
        private ISave save;

        public SaveDataRepository()
        {
            inst = this;
            save = new SaveDataJsonCrypto(false);
        }

        private string MakeFullName(string name)
        {
            var path =Path.Combine(Application.dataPath,"SaveGame");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fullName = Path.Combine(path,name);
            return fullName;
        }

        public void Save<T>(List<T> data, string name = null)
        {
            var fullName = MakeFullName(name);
            Debug.Log($"save path: {fullName}");
            save.Save(data, fullName);
        }

        public List<T> Load<T>(string name)
        {
            var fullName = MakeFullName(name);
            Debug.Log($"load path: {fullName}");
            return save.Load<T>(fullName);
        }
    }
}