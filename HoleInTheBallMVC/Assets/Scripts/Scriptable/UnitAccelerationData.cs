using UnityEngine;

namespace Hole
{
    [CreateAssetMenu(menuName = "My/UnitAccelerationData")]
    public class UnitAccelerationData : ScriptableObject
    {
        public float startSpeed;
        public float addSpeed;
        public float timeWithoutCollision;
    }
}
