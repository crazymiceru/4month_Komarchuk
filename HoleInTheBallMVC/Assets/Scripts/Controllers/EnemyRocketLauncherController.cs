using UnityEngine;

namespace Hole
{
    internal sealed class EnemyRocketLauncherController : IController, IExecute, IInitialization
    {
        private UnitM _unit;
        private UnitView _unitView;
        private UnitRocketLauncherExtData _rocketLauncherData;
        private float _timeFire=0;
        private ControlLeak _controlLeak = new ControlLeak("RocketLauncher");

        internal EnemyRocketLauncherController(UnitM unit, UnitView unitView, UnitRocketLauncherExtData rocketLauncherData)
        {
            _unit = unit;
            _unitView = unitView;
            _rocketLauncherData = rocketLauncherData;
            _unit.evtKill += Kill;
        }

        public void Initialization()
        {
        }

        public void Execute(float deltaTime)
        {
            if (_timeFire < Time.time)
            {
                _timeFire = Time.time + _rocketLauncherData.FreqFire;
                FabricUnit.inst.CreateUnit(TypeItem.EnemyRocket, _unitView.PositionInfo[Random.Range(0, _unitView.PositionInfo.Length)].position, _unitView.transform.rotation);
            }

        }


        void Kill()
        {
            ListControllers.inst.Delete(this);
        }

    }
}