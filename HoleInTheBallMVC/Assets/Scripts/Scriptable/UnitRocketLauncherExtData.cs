using UnityEngine;

namespace Hole
{
    [CreateAssetMenu(menuName = "My/Addition/RocketLauncherExt")]
    public class UnitRocketLauncherExtData : ScriptableObject
    {
        public float FreqFire
        {
            get=>_freqFire;
        }
        [SerializeField] private float _freqFire = 5;

    }
}
