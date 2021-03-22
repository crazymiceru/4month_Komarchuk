using UnityEngine;

namespace Hole
{
    internal sealed class EnemyFireController : IController, IInitialization, IExecute
    {
        private UnitM _unit;
        private UnitFireExtData _unitFireData;
        private UnitView _unitView;
        private Rigidbody _rb;
        private Light _lgt;
        private GameObject _particles;
        private float _powerLight = 1;
        private int maxCountAddFire;
        private ControlLeak _controlLeak = new ControlLeak("Fire!");

        internal EnemyFireController(UnitM unit, UnitView unitView, UnitFireExtData unitFireData, object powerLight)
        {
            _unit = unit;
            _unitView = unitView;
            _unitFireData = unitFireData;
            _unit.evtKill += Kill;
            _powerLight = powerLight == null ? 1 : (float)powerLight;
            maxCountAddFire = _unitFireData.maxCountAddFire;
        }

        public void Initialization()
        {
            _rb = _unitView.GetComponent<Rigidbody>();
            if (_rb == null) Debug.Assert(true, $"Missing Rigidbody on {_unitView.name}");

            _lgt = _unitView.PositionInfo[0].gameObject.GetComponent<Light>();
            if (_lgt == null) Debug.Assert(true, $"Missing Get Light on {_unitView.name}");

            _particles = _unitView.PositionInfo[1].gameObject;
            if (_particles == null) Debug.Assert(true, $"Missing Get Particles on {_unitView.name}");

            Execute(Time.deltaTime);
        }

        public void Execute(float deltaTime)
        {
            _particles.transform.localScale = Vector3.one * _powerLight;

            var r = Random.Range(0f, 1f);
            if (maxCountAddFire > 0 && r < _unitFireData.chanceAdd * _powerLight * _powerLight * deltaTime)
            {
                maxCountAddFire--;
                var powerLight = Mathf.Clamp(_powerLight + Random.Range(-_unitFireData.decLightAdd, _unitFireData.addLightAdd), 0, 1);
                FabricUnit.inst.CreateUnit(TypeItem.EnemyFire, _unitView.transform.position, Quaternion.identity, 0, powerLight);
            }

            _unit.control = new Vector3(
                Random.Range(-_unitFireData.powerMoveRandom, _unitFireData.powerMoveRandom),
                0,
                Random.Range(-_unitFireData.powerMoveRandom, _unitFireData.powerMoveRandom)
                );

            _lgt.color = _unitFireData.gradient.Evaluate(Random.Range(0f, 1f)) * _powerLight;
            _powerLight -= _unitFireData.speedPowerDec * Time.deltaTime;
            if (_powerLight <= 0) _unit.HP = 0;

        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}