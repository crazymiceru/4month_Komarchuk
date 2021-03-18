using UnityEngine;

namespace Hole
{
    internal sealed class UnitController :IController,IInitialization
    {
        private UnitM _unit;
        private UnitView _unitView;
        private UnitData _unitData;

        ControlLeak _controlLeak = new ControlLeak("UnitController");

        #region Init

        internal UnitController(UnitM unit, UnitView unitView, UnitData unitData)
        {
            _unit = unit;
            _unitView = unitView;
            _unitData = unitData;

            _unit.evtKill += _unitView.Kill;            
            _unit.evtKill += Kill;
            _unitView.evtInInteractive += InInteractive;
            _unitView.evtOutInteractive += OutInteractive;
            _unit.evtDecLives += DecLive;

            _unit.packInteractiveData.attackPower = _unitData.AttackPower;
            _unit.packInteractiveData.scores = _unitData.addScores;            
            var (typeItem,b)= _unitView.GetTypeItem();
            _unit.packInteractiveData.typeItem = typeItem;
        }


        public void Initialization()
        {
            _unit.Scores = 0;
            _unit.HP = _unitData.maxLive;
            if (_unitData.imgIco!=null) Reference.inst.radarController.AddPoint(_unitView.gameObject, _unitData.imgIco);
        }

        void Kill()
        {
            if (_unitData.imgIco != null) Reference.inst.radarController.DelPoint(_unitView.gameObject);
            ListControllers.inst.Delete(this);
        }

        #endregion

        private void DecLive()
        {
            if (_unitData.destroyEffects!=null)
            {
                var go = GameObject.Instantiate(_unitData.destroyEffects, _unitView.transform.position+_unitData.addPosDestroyEffects,Quaternion.identity);
                GameController.SetTrash(go);
                GameObject.Destroy(go, _unitData.timeViewDestroyEffects);
            }
        }

        private void InInteractive(PackInteractiveData pack,bool isEnter)
        {
            if (isEnter)
            {
                if (!_unit.isInvulnerability) _unit.HP -= pack.attackPower;
                _unit.Scores += pack.scores;
            }
        }

        private void OutInteractive(IInteractive ui,bool isEnter)
        {                        
            ui.InInteractive(_unit.packInteractiveData, isEnter);
        }
    }
}