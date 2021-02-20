using TMPro;
using UnityEngine;

public class MakeScoresCanvas : MonoBehaviour
{
    [SerializeField] private IntVariable _scoresGlobal;
    [SerializeField] private AddListener _listener;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        _listener.RegisterListener(UpdateScores);
    }

    private void UpdateScores()
    {
        text.text = _scoresGlobal.Value.ToString();
    }
    private void OnDisable()
    {
        _listener.UnregisterListener(UpdateScores);
    }
}
