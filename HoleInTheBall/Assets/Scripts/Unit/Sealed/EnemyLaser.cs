using UnityEngine;

namespace Hole
{
    public sealed class EnemyLaser : EnemyController, IEnemyEffects
    {
        [Header("Laser Setup")]
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Transform _startPos;
        [SerializeField] private float _addLenghtLine=0;

        void IEnemyEffects.Update()
        {
            RaycastHit hit;
            Ray ray = new Ray(_startPos.position, transform.forward);
            
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                _line.SetPosition(1, new Vector3(0, 0, hit.distance+ _addLenghtLine));
                if (hit.transform.gameObject.TryGetComponent(out Unit unit))
                {
                    Debug.Log($"атака {gameObject.name}");
                    unit.Attack();
                }
            }

        }
    }
}