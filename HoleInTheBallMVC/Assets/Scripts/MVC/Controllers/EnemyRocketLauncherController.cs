using UnityEngine;

namespace Hole
{
    internal sealed class EnemyRocketLauncherController : IController, IExecute, IInitialization
    {
        private UnitM _unit;
        private UnitView _unitView;
        private UnitRocketLauncherAddData _rocketLauncherData;
        private float _timeFire=0;

        internal EnemyRocketLauncherController(UnitM unit, UnitView unitView, UnitRocketLauncherAddData rocketLauncherData)
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
                _timeFire = Time.time + _rocketLauncherData.freqFire;
                var go = DataObjects.inst.GetValue<GameObject>("Enemy/Rocket");
                var goInst = GameObject.Instantiate(go, _unitView.PositionInfo[Random.Range(0, _unitView.PositionInfo.Length)].position, _unitView.transform.rotation);
                GameController.SetTrash(goInst);
            }

        }


        void Kill()
        {
            ListControllers.inst.Delete(this);
        }

    }
}