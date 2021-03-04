using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    #region Fields

    [SerializeField] private int _freqUpdate = 50;
    private Text _fpstxt=default;
    private static Fps _f; 
    public string  addTxt;
    private float _fpszam;
    private float _deltaTime = 0.0f;
    private int _countFps;

    #endregion


    #region Init

    private void Awake()
    {
        if (_f == null)
        {
            _f = this;
        }
        else if (_f != this)
        {
            Destroy(gameObject);
        }
        _fpstxt = GetComponent<Text>();
    }

    private void Start()
    {
        _countFps = 0;
    }

    #endregion


    #region Fps

    private void Update()
    {
        _countFps++;
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        _fpszam = 1.0f / _deltaTime;

        if ((int)(_countFps / _freqUpdate) * _freqUpdate == _countFps)
            _fpstxt.text = $"{addTxt} fps:{_fpszam:N1} quality: {QualitySettings.GetQualityLevel()} tier: {Graphics.activeTier}";
    }

    #endregion

}
