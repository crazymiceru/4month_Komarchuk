using UnityEngine;

namespace Hole
{
    [CreateAssetMenu(menuName = "My/UnitData")]
    public class UnitData : ScriptableObject
    {
        [Header("Move")]
        public float powerMove = 500f;
        public float powerJump = 300f;
        public bool isControlSetHorisontal=false;
        public Vector3 rotateVector;
        public bool isAutoRotate=false;
        [Header("Limits")]
        public float maxSpeed = 10;
        public float maxY = 2.17f;
        [Header("Live")]
        public int maxLive=0;
        public GameObject destroyEffects;
        public float timeInvulnerability;
    }
}
