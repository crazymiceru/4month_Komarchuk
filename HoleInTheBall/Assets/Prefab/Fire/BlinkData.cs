using UnityEngine;

public class BlinkData : MonoBehaviour
{
    public float posRandom = 0.1f;
    public float maxSpeed = 1f;
    public float freqUpdate = 0.01f;
    public Gradient gradient = default;
    public Light lgt;    
    public float powerLight = 1;
    public float speedPowerDec = 0.01f;
    public float chanceAdd = 0.01f;
    public float decLightAdd = 0.1f;
    public float addLightAdd = 0.2f;
}
