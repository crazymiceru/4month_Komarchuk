using UnityEngine;

namespace Hole
{
    internal sealed class AccelerationController : IController, IInitialization, IExecute
    {
        private UnitM _unit;
        private UnitAccelerationData _unitAccelerationData;
        private UnitView _unitView;
        private Rigidbody _rb;
        private float startTime;
        private Collider[] _col;
        private bool isCollidersEnable = true;

        private ControlLeak _controlLeak = new ControlLeak("AccelerationController");

        internal AccelerationController(UnitM unit, UnitView unitView, UnitAccelerationData unitAccelerationData)
        {

            

        _unit = unit;
            _unitView = unitView;
            _unitAccelerationData = unitAccelerationData;
            _unit.evtKill += Kill;
            _unitView.evtAnyCollision += Collision;
        }

        public void Initialization()
        {
            _rb = _unitView.GetComponent<Rigidbody>();
            if (_rb == null)
            {
                Debug.Assert(true, $"Missing Rigidbody on {_unitView.name}");
            }

            _col = _unitView.GetComponentsInChildren<Collider>();
            if (_unitAccelerationData.timeWithoutCollision > 0) EnableColliders(false);

            _rb.AddForce(_unitView.transform.forward * _unitAccelerationData.startSpeed);
            startTime = Time.time;            
        }

        private void EnableColliders(bool isEnable)
        {
            isCollidersEnable = isEnable;
            for (var index = 0; index < _col.Length; index++)
            {
                var colliderItem = _col[index];
                colliderItem.enabled = isEnable;
            }
        }

        private void Collision()
        {
            _unit.HP = 0;
        }

        public void Execute(float deltaTime)
        {
            //Debug.Log($"Rocket Execute");
            _rb.AddForce(_unitView.transform.forward * _unitAccelerationData.addSpeed * deltaTime);
            if (!isCollidersEnable && startTime + _unitAccelerationData.timeWithoutCollision < Time.time) EnableColliders(true);
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}