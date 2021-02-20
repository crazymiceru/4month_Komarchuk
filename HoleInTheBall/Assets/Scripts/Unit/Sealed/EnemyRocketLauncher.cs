using UnityEngine;

namespace Hole
{
    public sealed class EnemyRocketLauncher : EnemyController, IEnemyEffects
    {
        [Header("Rocket Setup")]
        [SerializeField] private Transform[] _startFirePos;
        [SerializeField] private GameObject _prefRocket;
        [SerializeField] private float _freqFire = 10;
        private float _timeFire;

        void IEnemyEffects.Update()
        {
            if (_timeFire<Time.time)
            {
                _timeFire = Time.time + _freqFire;
                var pref = Instantiate(_prefRocket, _startFirePos[Random.Range(0,_startFirePos.Length)].position, transform.rotation);
                pref.transform.SetParent(GameController.inst.trash);
            }
        }
    }
}