using UnityEngine;

namespace Hole
{
    [CreateAssetMenu(menuName = "My/UnitAccelerationData")]
    public class UnitAccelerationData : ScriptableObject
    {
        public float startSpeed
        {
            get => _startSpeed;
        }
        [SerializeField] private float _startSpeed = 30;

        public float addSpeed
        {
            get => _addSpeed;
        }
        [SerializeField] private float _addSpeed = 200;


        public float timeWithoutCollision
        {
            get => _timeWithoutCollision;
        }
        [SerializeField] private float _timeWithoutCollision = 0.5f;
    }
}
