using UnityEngine;

public class Util : MonoBehaviour
{

    /// <summary>
    /// Converting a value with a certain step to the resulting value
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="endValue">End Value</param>
    /// <param name="stepDec">Step Substaction</param>
    /// <param name="stepInc">Step Inc</param>
    /// <returns></returns>
    static public float StepFloat(float value,float endValue,float stepDec,float stepInc)
    {
        if (value < endValue)
        {            
            value += stepInc*Time.deltaTime;
            value = Mathf.Clamp(value, float.MinValue, endValue);

        }
        if (value > endValue)
        {            
            value -= stepDec * Time.deltaTime;            
            value = Mathf.Clamp(value, endValue, float.MaxValue);            
        }
        return value;
    }
    static public float StepFloat(float value, float endValue, float step)
    {
        return StepFloat(value, endValue, step, step);
    }

    static public Vector3 ClearVectorY(Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }

    static public void PlaySoundRandomPitch(AudioSource snd,float pitch)
    {
        snd.pitch= 1+ Random.Range(-pitch, pitch);
        snd.Play();
    }

    static public float SqrDist(Vector3 v1, Vector3 v2)
    {
        return (v1 - v2).sqrMagnitude;
    }

}
