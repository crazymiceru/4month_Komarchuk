using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hole
{
    internal sealed class SaveDataJsonCrypto : ISave
    {
        private bool _isCrypto;
        private const char split = (char)1;
        internal SaveDataJsonCrypto(bool isCrypto = true)
        {
            _isCrypto = isCrypto;
        }

        public List<T> Load<T>(string name)
        {
            var dataMas = new List<T>();
            var str = File.ReadAllText(name);
            var strSplit = str.Split(split);
            Debug.Log($"Разбили строку на массив: {strSplit.Length}");

            for (int i = 0; i < strSplit.Length-1; i++)
            {
                Debug.Log($"Строка {i}:{strSplit[i]}");
                dataMas.Add(_isCrypto ? JsonUtility.FromJson<T>(Crypto.DeCryptoXOR(str)) : JsonUtility.FromJson<T>(strSplit[i]));
            }

            return dataMas;
        }

        public void Save<T>(List<T> data, string name)
        {
            string str="";
            for (int i = 0; i < data.Count; i++)
            {
                str += JsonUtility.ToJson(data[i])+ split;
            }
            //Debug.Log($"Save JSon:{str}");
            File.WriteAllText(name, _isCrypto ? Crypto.CryptoXOR(str) : str);
        }
    }
}