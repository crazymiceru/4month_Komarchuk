using UnityEngine;

namespace Hole
{
    internal sealed class SaveController : IController, ICntrSave
    {
        UnitM _unit;
        private UnitView _unitView;
        private ControlLeak _controlLeak = new ControlLeak("SaveController");

        internal SaveController(UnitM unit, UnitView unitView, DataGameForSave data = null)
        {
            _unit = unit;
            _unitView = unitView;
            _unit.evtKill += Kill;
            if (data != null) Load(data);
        }

        public void Load(DataGameForSave data)
        {
            //Debug.Log($"Load Data {data.name} for: {_unitView.name}");
            _unitView.SetTypeItem(data.type, data.numCfg);
            _unit.HPSetInvis = data.hp;
            _unitView.transform.position = data.pos;
            _unitView.transform.rotation = data.angle;
            _unit.Scores = data.scores;
        }

        public DataGameForSave Save()
        {
            var data = new DataGameForSave();
            (data.type, data.numCfg) = _unitView.GetTypeItem();
            data.name = _unitView.name;
            data.hp = _unit.HP;
            data.pos = _unitView.transform.position;
            data.angle = _unitView.transform.rotation;
            data.scores = _unit.Scores;
            return data;
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}