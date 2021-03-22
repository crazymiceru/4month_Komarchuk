using UnityEngine;

namespace Hole
{
    internal sealed class RotateController : IController, IExecute
    {
        UnitM _unit;        
        private UnitRotateData _unitRotateData;
        private Transform _goTransform;
        private ControlLeak _controlLeak = new ControlLeak("Rotate");

        internal RotateController(UnitM unit, Transform goTrasform, UnitRotateData unitData)
        {
            _unit = unit;
            
            _unitRotateData = unitData;
            _unit.evtKill += Kill;
            _goTransform = goTrasform;
        }

        public void Execute(float deltaTime)
        {            
            if (_goTransform!=null) _goTransform.Rotate(_unitRotateData.rotateVector * deltaTime);
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}