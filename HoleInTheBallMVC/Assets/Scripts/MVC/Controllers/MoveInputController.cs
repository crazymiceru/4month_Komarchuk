using UnityEngine;

namespace Hole
{
    internal sealed class MoveInputController : IController, IExecute
    {
        private UnitM _unit;
        private float _angleCam;

        internal MoveInputController(UnitM unit)
        {
            _unit = unit;
            _angleCam=Reference.inst.MainCamera.transform.rotation.eulerAngles.y;
            _unit.evtKill += Kill;
        }

        public void Execute(float deltaTime)
        {
            var v = Input.GetAxis("Vertical");
            var h = Input.GetAxis("Horizontal");

            if (v != 0 || h != 0)
            {
                var control = new Vector3(h, 0, v);
                control = Quaternion.Euler(0, _angleCam, 0) * control;
                _unit.control = control;
            }
            else
            {
                _unit.control = Vector3.zero;
            }
        }

        private void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}