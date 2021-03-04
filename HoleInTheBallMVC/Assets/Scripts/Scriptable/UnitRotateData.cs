using UnityEngine;

namespace Hole
{
    [CreateAssetMenu(menuName = "My/UnitRotateData")]
    public class UnitRotateData : ScriptableObject
    {
        [Header("Move")]
        public Vector3 rotateVector;
    }
}
