using UnityEngine;

namespace Hole
{
    internal class Unit : UnitInit
    {
        #region Interactive


        private void OnTriggerEnter(Collider other)
        {
                Interaction(other);
        }


        protected virtual void Interaction(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Unit unit) && unit.lives>0)
            {                
            }

        }

        internal void Invulnerability(float v)
        {
            Debug.Log($"Invulnerability time: {v}");
            _timeInvulnerability = Time.time + v;
        }

        #endregion


        #region Attack And Destroy

        internal virtual void Attack()
        {
            //Debug.Log($"наносим повреждения {gameObject.name}");
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