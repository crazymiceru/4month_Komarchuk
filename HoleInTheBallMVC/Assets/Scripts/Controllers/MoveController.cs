using UnityEngine;

namespace Hole
{

    internal sealed class MoveController : IController, IExecute, IInitialization
    {
        private UnitM _unit;
        private UnitView _unitView;
        private UnitData _unitData;
        private Rigidbody _rb;
        private ControlLeak _controlLeak = new ControlLeak("Move");

        internal MoveController(UnitM unit, UnitView unitView, UnitData unitData)
        {
            _unit = unit;
            _unitView = unitView;
            _unitData = unitData;

            _unit.evtKill += Kill;
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }

        public void Initialization()
        {
            _rb = _unitView.GetComponent<Rigidbody>();
        }

        public void Execute(float deltaTime)
        {
            if (_unit.control != null)
            {
                if (_rb != null)
                {
                    _rb.AddForce(_unit.control * _unitData.powerMove * deltaTime * _rb.mass);
                }
                else
                {
                    _unitView.transform.position += _unit.control *  deltaTime;
                }
            }

            LimitMove(_unitData.maxSpeed, _unitData.maxY);
        }

        internal void LimitMove(float maxSpeed, float maxY)
        {
            if (_rb != null)
            {
                var maxSqrSpeed = maxSpeed * maxSpeed;
                if (_rb.velocity.sqrMagnitude > maxSqrSpeed)
                {
                    _rb.velocity = _rb.velocity.normalized * maxSpeed;
                }
                if (_unitView.transform.position.y > maxY && _rb.velocity.y > 0)
                {
                    _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                }
            }
        }
    }
}