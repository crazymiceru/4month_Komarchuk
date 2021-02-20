using System.Collections;
using UnityEngine;

public class BlinkLightPower : MonoBehaviour
{
    private BlinkData _d;

    private void Awake()
    {
        _d = GetComponent<BlinkData>();
    }

    private void Start()
    {    
        StartCoroutine(Light());
    }


    IEnumerator Light()
    {
        while (true) 
        {
            _d.lgt.color = _d.gradient.Evaluate(Random.Range(0f, 1f))* _d.powerLight;
            _d.powerLight -= _d.speedPowerDec;
            if (_d.powerLight <= 0) { Destroy(gameObject); yield break;};
            yield return new WaitForSeconds(_d.freqUpdate);
        }
    }

}
