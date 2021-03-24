using UnityEngine;

namespace Hole
{
    [CreateAssetMenu(menuName = "My/UnitSelfGuidedData")]
    public class UnitSelfGuidedData : ScriptableObject
    {
        public float isSGX
        {
            get => _isSGX;
        }
        [SerializeField] private float _isSGX = 0;
        public float isSGZ
        {
            get => _isSGZ;
        }
        [SerializeField] private float _isSGZ = 0;
    }
}
