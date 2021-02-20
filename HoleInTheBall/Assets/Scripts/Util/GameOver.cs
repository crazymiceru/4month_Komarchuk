using System;
using TMPro;
using UnityEngine;

public sealed class GameOver : MonoBehaviour
{
    [SerializeField] private IntVariable _liveGlobal;
    [SerializeField] private AddListener _listenerLive;

    private void OnEnable()
    {
        _listenerLive.RegisterListener(UpdateLive);
    }

    private void OnDisable()
    {
        _listenerLive.UnregisterListener(UpdateLive);
    }

    private void UpdateLive()
    {
        if (_liveGlobal.Value==0)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
}
