using UnityEngine;

namespace Hole
{
    internal sealed class SelfGuidedController : IController, IExecute, IInitialization
    {
        private UnitM _unit;
        private UnitSelfGuidedData _unitSelfGuidedData;
        private UnitView _unitView;
        private ControlLeak _controlLeak = new ControlLeak("SelfGuided");
        private Transform _player;

        internal SelfGuidedController(UnitM unit, UnitView unitView, UnitSelfGuidedData unitSelfGuidedData)
        {
            _unit = unit;
            _unitSelfGuidedData = unitSelfGuidedData;
            _unitView = unitView;
            _unit.evtKill += Kill;
        }

        public void Initialization()
        {
            _player = Reference.inst.Player.transform;
        }

        public void Execute(float deltaTime)
        {
            if (_player != null)
            {
                if (_unitView.transform.position.x < _player.position.x) _unit.control.x = _unitSelfGuidedData.isSGX;
                if (_unitView.transform.position.x > _player.position.x) _unit.control.x = -_unitSelfGuidedData.isSGX;
                if (_unitView.transform.position.z < _player.position.z) _unit.control.z = _unitSelfGuidedData.isSGZ;
                if (_unitView.transform.position.z > _player.position.z) _unit.control.z = -_unitSelfGuidedData.isSGZ;
            }
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}