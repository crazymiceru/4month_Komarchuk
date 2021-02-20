using UnityEngine;

namespace Hole
{
    internal sealed class Bonus : EnemyController
    {
        [Header("Bonus")]
        public TypeBonus typeBonus;

        protected override void Interaction(Collider other)
        {            
            if (lives > 0 && other.gameObject.TryGetComponent(out Unit unit) && unit.isInteractive)
            {   base.Interaction(other);
                Debug.Log($"{gameObject.name} take {other.gameObject.name}");
                switch (typeBonus)
                {
                    case TypeBonus.None:
                        break;
                    case TypeBonus.Heart:
                        unit.lives++;
                        break;
                    case TypeBonus.Coin:
                        unit.scores++;
                        break;
                    case TypeBonus.Poison:
                        unit.Attack();
                        break;
                    case TypeBonus.Invulnerability:
                        unit.Invulnerability(10);
                        break;
                    default:
                        break;
                }
                //Debug.Log($"Бонус устраняется, так как подействовал");
                Attack();
            }
        }
    }

}