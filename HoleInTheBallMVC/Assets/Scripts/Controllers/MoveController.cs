using UnityEngine;

namespace Hole
{

    internal sealed class MoveController : IController, IExecute, IInitialization
    {
        private UnitM _unit;
        private UnitView _unitView;
        private UnitData _unitData;
        private Rigidbody _rigidBody;
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
            _rigidBody = _unitView.GetComponent<Rigidbody>();
        }

        public void Execute(float deltaTime)
        {
            if (_unit.control != null)
            {
                if (_rigidBody != null)
                {
                    _rigidBody.AddForce(_unit.control * _unitData.powerMove * deltaTime * _rigidBody.mass);
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
            if (_rigidBody != null)
            {
                var maxSqrSpeed = maxSpeed * maxSpeed;
                if (_rigidBody.velocity.sqrMagnitude > maxSqrSpeed)
                {
                    _rigidBody.velocity = _rigidBody.velocity.normalized * maxSpeed;
                }
                if (_unitView.transform.position.y > maxY && _rigidBody.velocity.y > 0)
                {
                    _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, _rigidBody.velocity.z);
                }
            }
        }
    }
}