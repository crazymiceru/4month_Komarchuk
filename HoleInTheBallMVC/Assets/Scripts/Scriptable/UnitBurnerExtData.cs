using UnityEngine;

namespace Hole
{
    [CreateAssetMenu(menuName = "My/Addition/BurnerExt")]
    public class UnitBurnerExtData : ScriptableObject
    {
        public float freqFire
        {
            get => _freqFire;
        }
        [SerializeField] private float _freqFire = 10;

        public float countFire
        {
            get => _countFire;
        }
        [SerializeField] private float _countFire = 3;

        public float jetFireFreq
        {
            get => _jetFireFreq;
        }
        [SerializeField] private float _jetFireFreq = 1;

        public float forceFire
        {
            get => _forceFire;
        }
        [SerializeField] private float _forceFire = 333f;
    }
}
