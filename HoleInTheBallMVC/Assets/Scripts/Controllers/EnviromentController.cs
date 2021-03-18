using UnityEngine;

namespace Hole
{
    internal sealed class EnviromentController : IController
    {
        private UnitM _unit;
        private ControlLeak _controlLeak = new ControlLeak("EnviromentController");

        internal EnviromentController(UnitM unit, EnvironmentData environmentData)
        {
            _unit = unit;
            _unit.evtKill += Kill;
            _unit.packInteractiveData.obj = environmentData;
            //Debug.Log($"EnvironmentData: {((EnvironmentData)_unit.packInteractiveData.obj).maxSqrSlowSpeed}");
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}