using System.Collections;
using UnityEngine;

namespace Hole
{
    internal sealed class EnemyBurnerController : IController, IExecute
    {
        private UnitM _unit;
        private UnitView _unitView;
        UnitBurnerExtData _unitBurnerExtData;
        private float _currentFireTime;
        private Transform _posFire;
        private ControlLeak _controlLeak = new ControlLeak("Burner");

        internal EnemyBurnerController(UnitM unit, UnitView unitView,UnitBurnerExtData unitBurnerExtData)
        {
            _unit = unit;
            _unitView = unitView;
            _unitBurnerExtData = unitBurnerExtData;
            _posFire = _unitView.PositionInfo[0];
            _unit.evtKill += Kill;
        }

        public void Execute(float deltaTime)
        {
            if (_currentFireTime < Time.time)
            {
                _currentFireTime = Time.time + _unitBurnerExtData.freqFire;
                 _unitView.StartCoroutine(AttackFire());
            }
        }

        IEnumerator AttackFire()
        {
            var pref = Resources.Load<GameObject>("Enemy/FireGround");

            for (int i = 0; i < _unitBurnerExtData.countFire; i++)
            {
                var go = FabricUnit.inst.CreateUnit(TypeItem.EnemyFire, _posFire.position, Quaternion.identity);
                go.GetComponent<Rigidbody>().AddForce(_unitView.transform.forward * _unitBurnerExtData.forceFire);

                yield return new WaitForSeconds(_unitBurnerExtData.jetFireFreq);
            }

        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }

    }
}