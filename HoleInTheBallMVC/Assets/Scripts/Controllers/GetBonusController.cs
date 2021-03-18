using UnityEngine;

namespace Hole
{
    internal sealed class GetBonusController : IController
    {
        private UnitM _unit;
        private UnitView _unitView;
        private ControlLeak _controlLeak = new ControlLeak("GetBonus");

        internal GetBonusController(UnitM unit, UnitView unitView)
        {
            _unit = unit;
            _unitView = unitView;
             _unit.evtKill += Kill;
            _unitView.evtInInteractive += InInteractive;
            _unit.isInvulnerability = false;

        }

        private void InInteractive(PackInteractiveData pack,bool isEnter)
        {
            if (isEnter)
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
                        Debug.Log($"Invulnerability Bonus");
                        break;
                }
            }
        }

        private void Kill()
        {
            ListControllers.inst.Delete(this);
        }

    }
}