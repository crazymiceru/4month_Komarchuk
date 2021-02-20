using UnityEngine;

namespace Hole
{
    public sealed class EnemySpeedCollapse : EnemyController
    {
        [Header("Slow Setup")]
        [SerializeField] private float _powerSpeed = 100f;

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Unit unit))
            {
                if (other.gameObject.TryGetComponent(out Rigidbody rb))
                {
                    rb.velocity = rb.velocity + rb.velocity.normalized * _powerSpeed * Time.deltaTime;
                }
            }
        }
    }
}