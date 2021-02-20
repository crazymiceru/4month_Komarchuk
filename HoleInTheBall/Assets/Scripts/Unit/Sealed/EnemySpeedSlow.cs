using UnityEngine;

namespace Hole
{
    public sealed class EnemySpeedSlow : EnemyController
    {
        [Header("Slow Setup")]
        [SerializeField] private float _MaxSqrSpeed = 1f;

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Unit unit))
            {
                if (other.gameObject.TryGetComponent(out Rigidbody rb))
                {
                    if (rb.velocity.sqrMagnitude > _MaxSqrSpeed) rb.velocity = rb.velocity.normalized * _MaxSqrSpeed;
                }
            }
        }
    }
}