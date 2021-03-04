using UnityEngine;

namespace Hole
{
    internal sealed class EmptyCntroller : IController
    {
        UnitM _unit;        
        internal EmptyCntroller(UnitM unit)
        {
            _unit = unit;
            _unit.evtKill += Kill;
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}