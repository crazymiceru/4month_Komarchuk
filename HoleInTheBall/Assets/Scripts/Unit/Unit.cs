using UnityEngine;

namespace Hole
{
    public class Unit : UnitInit
    {
        #region Interactive

        private void OnTriggerEnter(Collider other)
        {
            if (_isInteractive)
            {
                Interaction(other);
            }
        }

        protected void Interaction(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Bonus bonus) && bonus.lives>0)
            {                
                Debug.Log($"{gameObject.name} take {other.gameObject.name}");
                switch (bonus.typeBonus)
                {
                    case TypeBonus.None:
                        break;
                    case TypeBonus.Heart:
                        lives++;
                        break;
                    case TypeBonus.Coin:
                        scores++;
                        break;
                    case TypeBonus.Poison:
                        Attack();
                        break;
                    case TypeBonus.Invulnerability:
                        Invulnerability(10);
                        break;
                    default:
                        break;
                }
                Debug.Log($"Attack on {gameObject.name}");
                bonus.Attack();                
            }

        }

        protected void Invulnerability(float v)
        {
            Debug.Log($"Invulnerability time: {v}");
            _timeInvulnerability = Time.time + v;
        }

        #endregion


        #region Attack And Destroy

        internal virtual void Attack()
        {
            Debug.Log($"наносим повреждения {gameObject.name}");
            if (_timeInvulnerability < Time.time)
            {
                if (_unitData.timeInvulnerability > 0)
                {
                    Invulnerability(_unitData.timeInvulnerability);                    
                }

                if (_unitData.destroyEffects != null)
                {
                    Transform pos;
                    if (_posExp == null) pos = transform;
                    else pos = _posExp;
                    var obj = Instantiate(_unitData.destroyEffects, pos.position, Quaternion.identity);
                    obj.transform.SetParent(GameController.inst.trash);
                    Destroy(obj, 10);
                }
                if (lives>=0) lives--; 
                if (lives == 0)
                {                    
                    Destroy(gameObject);
                    Debug.Log($"Уничтожаем {gameObject.name}");                    
                }
            }
        }

        #endregion
    }
}