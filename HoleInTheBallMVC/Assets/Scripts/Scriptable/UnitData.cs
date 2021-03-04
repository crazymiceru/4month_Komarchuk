using UnityEngine;

namespace Hole
{

    [CreateAssetMenu(menuName = "My/UnitData")]
    public class UnitData : ScriptableObject
    {
        [Header("Move")]
        public float powerMove = 500f;
        public float powerJump = 300f;
        public float minSqrDistance = 0.1f;
        [Header("Limits")]
        public float maxSpeed = 10;
        public float maxY = 2.17f;
        [Header("Live")]
        public int maxLive=1;
        public GameObject destroyEffects;
        public Vector3 addPosDestroyEffects;
        public float timeViewDestroyEffects=10;
        public float timeInvulnerability;
        [Header("Attack")]
        public int AttackPower = 1;
        public int addScores = 0;
    }
}
