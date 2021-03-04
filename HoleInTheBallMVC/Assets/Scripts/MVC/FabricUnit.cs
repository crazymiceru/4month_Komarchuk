
using UnityEngine;

namespace Hole
{

    internal sealed class FabricUnit
    {
        private ListControllers _listControllers;

        internal FabricUnit(ListControllers listControllers, IUnit[] units)
        {
            _listControllers = listControllers;
            for (int i = 0; i < units.Length; i++)
            {
                AddUnitComponents(units[i]);
            }
        }

        void AddUnitComponents(IUnit unitFlag)
        {
            var (typeItem, numCfg) = unitFlag.GetTypeItem();
            switch (typeItem)
            {
                case TypeItem.None:
                    break;
                case TypeItem.Player:
                    AddPlayer(unitFlag, numCfg);
                    break;
                case TypeItem.Coin:
                    AddCoin(unitFlag, numCfg);
                    break;
                case TypeItem.BonusHeart:
                case TypeItem.BonusInv:
                case TypeItem.BonusPoison:
                    AddBonus(unitFlag, numCfg);
                    break;
                case TypeItem.EnemyLaser:
                    AddEnemyLaser(unitFlag, numCfg);
                    break;
                case TypeItem.EnemyRocketLauncher:
                    AddEnemyRocketLauncher(unitFlag, numCfg);
                    break;
                case TypeItem.EnemyRocket:
                    AddEnemyRocket(unitFlag, numCfg);
                    break;
                default:
                    break;
            }
        }


        void AddPlayer(IUnit unitFlag, int numCfg)
        {
            var unit = new UnitM();
            Reference.inst.playerData = unit;
            _listControllers.Add(new MoveInputController(unit));
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Player {numCfg}")));
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Player {numCfg}")));
            _listControllers.Add(new InvulnerabilityController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Player {numCfg}")));
            _listControllers.Add(new GetBonusController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Player {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform.Find("Invulnerability"), DataObjects.inst.GetValue<UnitRotateData>($"Data/PlayerRotate {numCfg}")));
        }

        void AddEnemyLaser(IUnit unitFlag, int numCfg)
        {
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Enemy/Laser {numCfg}")));
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Enemy/Laser {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform, DataObjects.inst.GetValue<UnitRotateData>($"Data/Enemy/LaserRotate {numCfg}")));
            _listControllers.Add(new EnemyLaserController(unit, unitFlag as UnitView));
            if (unitFlag is UnitViewTraectory)
                _listControllers.Add(new MoveTrackController(unit, unitFlag as UnitViewTraectory, DataObjects.inst.GetValue<UnitData>($"Data/Enemy/Laser {numCfg}")));
        }

        void AddEnemyRocketLauncher(IUnit unitFlag, int numCfg)
        {
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Enemy/RocketLauncher {numCfg}")));
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Enemy/RocketLauncher {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform, DataObjects.inst.GetValue<UnitRotateData>($"Data/Enemy/RocketLauncherRotate {numCfg}")));
            _listControllers.Add(new EnemyRocketLauncherController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitRocketLauncherAddData>($"Data/Enemy/RocketLauncherAdd {numCfg}")));
            if (unitFlag is UnitViewTraectory)
                _listControllers.Add(new MoveTrackController(unit, unitFlag as UnitViewTraectory, DataObjects.inst.GetValue<UnitData>($"Data/Enemy/RocketLauncher {numCfg}")));
        }

        void AddEnemyRocket(IUnit unitFlag, int numCfg)
        {
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Enemy/Rocket {numCfg}")));
            _listControllers.Add(new AccelerationController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitAccelerationData>($"Data/Enemy/RocketAcceleration {numCfg}")));
        }

        void AddCoin(IUnit unitFlag, int numCfg)
        {
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Coin {numCfg}")));
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Coin {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform, DataObjects.inst.GetValue<UnitRotateData>($"Data/CoinRotate {numCfg}")));
            if (unitFlag is UnitViewTraectory)
                _listControllers.Add(new MoveTrackController(unit, unitFlag as UnitViewTraectory, DataObjects.inst.GetValue<UnitData>($"Data/Coin {numCfg}")));
        }

        void AddBonus(IUnit unitFlag, int numCfg)
        {
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Bonus {numCfg}")));
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Bonus {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform, DataObjects.inst.GetValue<UnitRotateData>($"Data/BonusRotate {numCfg}")));
            if (unitFlag is UnitViewTraectory)
                _listControllers.Add(new MoveTrackController(unit, unitFlag as UnitViewTraectory, DataObjects.inst.GetValue<UnitData>($"Data/Bonus {numCfg}")));
        }

    }
}