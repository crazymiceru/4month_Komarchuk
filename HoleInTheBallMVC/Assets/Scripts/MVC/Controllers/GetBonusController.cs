using UnityEngine;

namespace Hole
{
    internal sealed class GetBonusController : IController, IExecute
    {
        private UnitM _unit;
        private UnitView _unitView;
        private UnitData _unitData;
      

        internal GetBonusController(UnitM unit, UnitView unitView, UnitData unitData)
        {
            _unit = unit;
            _unitView = unitView;
            _unitData = unitData;
            _unit.evtKill += Kill;
            _unitView.evtInInteractive += InInteractive;
            _unit.isInvulnerability = false;

        }

        public void Execute(float deltaTime)
        {
        }

        private void InInteractive(PackInteractiveData pack)
        {
            switch (pack.typeItem)
            {
                case TypeItem.BonusHeart:
                    _unit.HP += 1;
                    break;
                case TypeItem.BonusPoison:                    
                    _unit.HP -= 1;
                    break;
                case TypeItem.BonusInv:
                    _unit.startTimeInvulnerability = Time.time + 10;
                    _unit.isInvulnerability = true;
                    //_unit.GOInvulnerability.SetActive(true);
                    Debug.Log($"Invulnerability Bonus");
                    break;
            }
        }

        private void Kill()
        {
            ListControllers.inst.Delete(this);
        }

    }
}