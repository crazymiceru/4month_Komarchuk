using UnityEngine;

namespace Hole
{
    [CreateAssetMenu(menuName = "My/Environment")]
    public class EnvironmentData : ScriptableObject
    {
        public float maxSqrSlowSpeed
        {
            get => _maxSqrSlowSpeed;
        }
        [SerializeField] private float _maxSqrSlowSpeed = 1;

        public float powerCollapseSpeed
        {
            get => _powerCollapseSpeed;
        }
        [SerializeField] private float _powerCollapseSpeed = 5;
        
    }
}
