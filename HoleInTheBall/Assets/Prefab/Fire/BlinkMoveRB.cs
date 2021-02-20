using System.Collections;
using UnityEngine;


public class BlinkMoveRB : MonoBehaviour
{
    private Rigidbody _rb;
    private BlinkData _d;
    [SerializeField] private GameObject _particles;

    private void Awake()
    {
        _d = GetComponent<BlinkData>();
        _rb = GetComponent<Rigidbody>();
        _particles.SetActive(true);
    }

    private void Start()
    {    
        StartCoroutine(MoveObject());    
    }


    IEnumerator MoveObject()
    {
        while (true)
        {
            _rb.AddForce(Random.onUnitSphere*_d.posRandom);
            _particles.transform.localScale = Vector3.one * _d.powerLight;
            if (_rb.velocity.sqrMagnitude>_d.maxSpeed* _d.maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _d.maxSpeed;
            }
            yield return new WaitForSeconds(_d.freqUpdate);
        }
    }
}
