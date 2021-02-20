using UnityEngine;

namespace Hole
{
    internal class UnitRB : Unit, IUnitComponentRBMove, IRotate
    {
        protected Rigidbody _rb;
        protected float _camRotate;
        protected float _h;
        protected float _v;
        protected bool _jmp;
        protected float _maxSqrSpeed;

        protected override void Awake()
        {
            base.Awake();
            _camRotate = Camera.main.transform.rotation.eulerAngles.y;
            _rb = GetComponent<Rigidbody>();
            _maxSqrSpeed = _unitData.maxSpeed * _unitData.maxSpeed;
        }

        void IUnitComponentRBMove.Update()
        {
            if (_rb != null)
            {
                var control = new Vector3(_h, 0, _v).normalized;
                if (_unitData.isControlSetHorisontal) control = Quaternion.Euler(0, _camRotate, 0) * control;
                _rb.AddForce(control * _unitData.powerMove * Time.deltaTime * _rb.mass);
                var controlJmp = new Vector3(0, _jmp ? _unitData.powerJump * _rb.mass : 0, 0);
                _rb.AddForce(controlJmp);

                if (_rb.velocity.sqrMagnitude > _maxSqrSpeed)
                {
                    _rb.velocity = _rb.velocity.normalized * _unitData.maxSpeed;
                }
                if (_pos.position.y > _unitData.maxY && _rb.velocity.y > 0)
                {
                    _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                }
            }
        }

        void IRotate.Update()
        {
            if (_unitData.isAutoRotate)
            {
                _pos.Rotate(_unitData.rotateVector * Time.deltaTime);
            }
        }
    }
}