using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    internal sealed class FabricUnit
    {
        #region Init

        private Dictionary<TypeItem, FabricDataForUnit> _fabricRunUnit;
        private ListControllers _listControllers;
        public static FabricUnit inst;

        internal FabricUnit(ListControllers listControllers, IUnit[] units)
        {
            MakeRunForUnit();

            _listControllers = listControllers;
            for (int i = 0; i < units.Length; i++)
            {
                AddUnitComponents(units[i]);
            }
            inst = this;
        }

        private void MakeRunForUnit()
        {
            _fabricRunUnit = new Dictionary<TypeItem, FabricDataForUnit>
            {
                [TypeItem.BonusHeart] = new FabricDataForUnit() { addUnitMetod = AddBonus, ObjName = "" },
                [TypeItem.BonusInv] = new FabricDataForUnit() { addUnitMetod = AddBonus, ObjName = "" },
                [TypeItem.BonusPoison] = new FabricDataForUnit() { addUnitMetod = AddBonus, ObjName = "" },
                [TypeItem.Coin] = new FabricDataForUnit() { addUnitMetod = AddCoin, ObjName = "" },
                [TypeItem.EnemyLaser] = new FabricDataForUnit() { addUnitMetod = AddEnemyLaser, ObjName = "" },
                [TypeItem.EnemyRocket] = new FabricDataForUnit() { addUnitMetod = AddEnemyRocket, ObjName = "Enemy/Rocket" },
                [TypeItem.EnemyRocketLauncher] = new FabricDataForUnit() { addUnitMetod = AddEnemyRocketLauncher, ObjName = "" },
                [TypeItem.Player] = new FabricDataForUnit() { addUnitMetod = AddPlayer, ObjName = "" },
                [TypeItem.EnemyFire] = new FabricDataForUnit() { addUnitMetod = AddEnemyFire, ObjName = "Enemy/Fire" },
                [TypeItem.EnemyBurner] = new FabricDataForUnit() { addUnitMetod = AddEnemyBurner, ObjName = "" },
                [TypeItem.EnvSlow] = new FabricDataForUnit() { addUnitMetod = AddEnvironment, ObjName = "" },
                [TypeItem.EnvCollapse] = new FabricDataForUnit() { addUnitMetod = AddEnvironment, ObjName = "" },
                [TypeItem.None] = new FabricDataForUnit() { addUnitMetod = AddNone, ObjName = "" },
            };
        }

        #endregion


        #region Add

        void AddUnitComponents(IUnit unitFlag, object param = null)
        {
            var (typeItem, numCfg) = unitFlag.GetTypeItem();
            _fabricRunUnit[typeItem].addUnitMetod.Invoke(unitFlag, numCfg, param);
        }

        public GameObject CreateUnit(TypeItem typeItem, Vector3 pos, Quaternion rot, int numCfg = 0, object param = null)
        {
            var go = DataObjects.inst.GetValue<GameObject>($"{_fabricRunUnit[typeItem].ObjName}");
            var goInst = GameObject.Instantiate(go, pos, rot);
            GameController.SetTrash(goInst);

            var unitFlag = goInst.GetComponent<IUnit>();
            (unitFlag as UnitView).NumCfg = numCfg;

            //Debug.Log($"Create Unit: '{(unitFlag as MonoBehaviour).name}'");
            if (unitFlag == null)
            {
                Debug.Assert(true, $"Create Unit false get component IUnit from {goInst.name}");
            }

            AddUnitComponents(unitFlag, param);

            return goInst;
        }

        #endregion


        #region AddUnitsComponent

        void AddPlayer(IUnit unitFlag, int numCfg, object param = null)
        {
            var name = "Player";
            var unit = Reference.inst.playerData;             
            _listControllers.Add(new MoveInputController(unit));
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")),"Player");
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
            _listControllers.Add(new InvulnerabilityController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
            _listControllers.Add(new GetBonusController(unit, unitFlag as UnitView));
            _listControllers.Add(new GetEnvironmentController(unit, unitFlag as UnitView));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).PositionInfo[0], DataObjects.inst.GetValue<UnitRotateData>($"Data/{name}Rotate {numCfg}")));
        }

        void AddEnemyLaser(IUnit unitFlag, int numCfg, object param = null)
        {
            var name = "Enemy/Laser";
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")),"Laser");
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform, DataObjects.inst.GetValue<UnitRotateData>($"Data/{name}Rotate {numCfg}")));
            _listControllers.Add(new EnemyLaserController(unit, unitFlag as UnitView));
            if (unitFlag is ITraectory)
                _listControllers.Add(new MoveTrackController(unit, unitFlag as UnitViewTraectory, DataObjects.inst.GetValue<UnitData>($"Data/Enemy/Laser {numCfg}")));
        }

        void AddEnemyBurner(IUnit unitFlag, int numCfg, object param = null)
        {
            var name = "Enemy/Burner";
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")), "Burner");
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform, DataObjects.inst.GetValue<UnitRotateData>($"Data/{name}Rotate {numCfg}")));
            _listControllers.Add(new EnemyBurnerController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitBurnerExtData>($"Data/{name}Ext {numCfg}")));
            if (unitFlag is UnitViewTraectory)
                _listControllers.Add(new MoveTrackController(unit, unitFlag as UnitViewTraectory, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
        }

        void AddNone(IUnit unitFlag, int numCfg, object param = null)
        {
            Debug.Log($"Unit of the { (unitFlag as MonoBehaviour).name} is given a type of None");
        }

        void AddEnemyRocketLauncher(IUnit unitFlag, int numCfg, object param = null)
        {
            var name = "Enemy/RocketLauncher";
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")), name);
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform, DataObjects.inst.GetValue<UnitRotateData>($"Data/{name}Rotate {numCfg}")));
            _listControllers.Add(new EnemyRocketLauncherController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitRocketLauncherExtData>($"Data/{name}Ext {numCfg}")));
            if (unitFlag is UnitViewTraectory)
                _listControllers.Add(new MoveTrackController(unit, unitFlag as UnitViewTraectory, DataObjects.inst.GetValue<UnitData>($"Data/Enemy/RocketLauncher {numCfg}")));
        }

        void AddEnemyRocket(IUnit unitFlag, int numCfg, object param = null)
        {
            var name = "Enemy/Rocket";
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")), "Rocket1");
            _listControllers.Add(new AccelerationController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitAccelerationData>($"Data/{name}Acceleration {numCfg}")), "Rocket2");
        }

        void AddEnemyFire(IUnit unitFlag, int numCfg, object param = null)
        {
            var name = "Enemy/Fire";
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")), "Fire");
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
            _listControllers.Add(new EnemyFireController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitFireExtData>($"Data/{name}Ext {numCfg}"), param));
        }

        void AddCoin(IUnit unitFlag, int numCfg, object param= null )
        {
            var name = "Coin";
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")), name);
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform, DataObjects.inst.GetValue<UnitRotateData>($"Data/{name}Rotate {numCfg}")));
            if (unitFlag is UnitViewTraectory)
                _listControllers.Add(new MoveTrackController(unit, unitFlag as UnitViewTraectory, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
        }

        void AddBonus(IUnit unitFlag, int numCfg, object param = null)
        {
            var name = "Bonus";
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")), name);
            _listControllers.Add(new MoveController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/Bonus {numCfg}")));
            _listControllers.Add(new RotateController(unit, (unitFlag as UnitView).transform, DataObjects.inst.GetValue<UnitRotateData>($"Data/{name}Rotate {numCfg}")));
            if (unitFlag is ITraectory)
                _listControllers.Add(new MoveTrackController(unit, unitFlag as UnitViewTraectory, DataObjects.inst.GetValue<UnitData>($"Data/{name} {numCfg}")));
            if (unitFlag is ISelfGuided)
                _listControllers.Add(new SelfGuidedController(unit, unitFlag as UnitViewSelfGuided, DataObjects.inst.GetValue<UnitSelfGuidedData>($"Data/{name}SelfGuided {numCfg}")));
        }

        void AddEnvironment(IUnit unitFlag, int numCfg, object param = null)
        {
            var unit = new UnitM();
            _listControllers.Add(new UnitController(unit, unitFlag as UnitView, DataObjects.inst.GetValue<UnitData>($"Data/UnitDataDefault")), "Environment");
            _listControllers.Add(new EnviromentController(unit,  DataObjects.inst.GetValue<EnvironmentData>($"Data/Enemy/Environment {numCfg}")), "");

        }

        #endregion

    }
}
