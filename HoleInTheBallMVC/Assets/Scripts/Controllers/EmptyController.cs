using UnityEngine;

namespace Hole
{
    internal sealed class EmptyCntroller : IController
    {
        UnitM _unit;
        private UnitView _unitView;
        private ControlLeak _controlLeak = new ControlLeak("");

        internal EmptyCntroller(UnitM unit, UnitView unitView)
        {
            _unit = unit;
            _unitView = unitView;
            _unit.evtKill += Kill;
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}