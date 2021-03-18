using UnityEngine;

namespace Hole
{
    [CreateAssetMenu(menuName = "My/UnitFireExtData")]
    internal class UnitFireExtData : ScriptableObject
    {
        public Gradient gradient
        {
            get => _gradient;
        }
        [SerializeField] private Gradient _gradient = default;

        public float speedPowerDec
        {
            get => _speedPowerDec;
        }
        [SerializeField] private float _speedPowerDec = 0.1f;

        public float chanceAdd
        {
            get => _chanceAdd;
        }
        [SerializeField] private float _chanceAdd = 0.5f;

        public int maxCountAddFire
        {
            get => _maxCountAddFire;
        }
        [SerializeField] private int _maxCountAddFire = 2;

        public float decLightAdd
        {
            get => _decLightAdd;
        }
        [SerializeField] private float _decLightAdd = 0.1f;

        public float addLightAdd
        {
            get => _addLightAdd;
        }
        [SerializeField] private float _addLightAdd = 0.2f;

        public float powerMoveRandom
        {
            get => _powerMoveRandom;
        }
        [SerializeField] private float _powerMoveRandom = 5f;
        
    }
}
