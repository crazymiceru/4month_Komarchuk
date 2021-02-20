using UnityEngine;

namespace Hole
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private float _startSpeed;
        private Rigidbody _rb;
        [SerializeField] private GameObject _destroyEffects;
        [SerializeField] private float _timeDetectObstacles = 1;
        private float _timeStart;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rb.velocity = transform.forward * _startSpeed;
            _timeStart = Time.time;
        }

        private void Update()
        {
            _rb.AddForce(transform.forward * _force * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_timeStart + _timeDetectObstacles < Time.time)
            {
                if (other.gameObject.TryGetComponent(out Unit unit))
                {
                    unit.Attack();
                }
                var pref = Instantiate(_destroyEffects, transform.position, Quaternion.identity);
                pref.transform.SetParent(GameController.inst.trash);
                Destroy(pref, 10);
                Destroy(gameObject);
            }
        }
    }
}