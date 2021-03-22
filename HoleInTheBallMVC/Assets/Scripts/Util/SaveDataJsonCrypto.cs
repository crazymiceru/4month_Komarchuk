using System.IO;
using UnityEngine;

namespace Hole
{
    internal sealed class SaveDataJsonCrypto : ISave
    {
        private bool _isCrypto;
        internal SaveDataJsonCrypto(bool isCrypto = true)
        {
            _isCrypto = isCrypto;
        }

        public T Load<T>(string name)
        {
            var str = File.ReadAllText(name);
            //_isCrypto  JsonUtility.FromJson<T>(Crypto.DeCryptoXOR(str));
            return _isCrypto ? JsonUtility.FromJson<T>(Crypto.DeCryptoXOR(str)) : JsonUtility.FromJson<T>(str);
        }

        public void Save<T>(T data, string name)
        {
            var str = JsonUtility.ToJson(data);
            Debug.Log($"Save JSon:{str}");
            File.WriteAllText(name, _isCrypto ? Crypto.CryptoXOR(str) : str);
        }
    }
}