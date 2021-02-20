using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    public class MakeLivesCanvas : MonoBehaviour
    {
        [SerializeField] private IntVariable _liveGlobal;
        [SerializeField] private AddListener _listenerLive;
        [SerializeField] private GameObject _prefabHeart;
        [SerializeField] private int _posX;
        [SerializeField] private int _posY;
        [SerializeField] private int _step;
        private List<GameObject> _massLives = new List<GameObject>();

        private void OnEnable()
        {
            _listenerLive.RegisterListener(UpdateLive);
        }

        private void OnDisable()
        {
            _listenerLive.UnregisterListener(UpdateLive);
        }

        private void Start()
        {
            UpdateLive();
        }

        private void UpdateLive()
        {
            while (_massLives.Count < _liveGlobal.Value)
            {
                var pref = Instantiate(_prefabHeart);
                pref.transform.SetParent(transform);
                pref.transform.localScale = Vector3.one;
                var rt = pref.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(_posX + _step * _massLives.Count, _posY, 0);
                _massLives.Add(pref);
            }
            while (_massLives.Count > _liveGlobal.Value)
            {
                Destroy(_massLives[_massLives.Count - 1]);
                _massLives.RemoveAt(_massLives.Count - 1);
            }
        }
    }
}
