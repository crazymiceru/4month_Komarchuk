using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    internal sealed class LivesController : IController
    {
        private UnitM _playerData;
        private GameObject _pref;
        private Transform _posLive;
        private List<GameObject> _massLives = new List<GameObject>();
        private int _posX=-80;
        private int _posY=0;
        private int _step=30;
        private ControlLeak _controlLeak = new ControlLeak("Lives");

        internal LivesController(UnitM playerData)
        {
            _playerData = playerData;
            _playerData.evtLives += UpdateLives;
            
            //_pref = Resources.Load<GameObject>("Util/ImgHeart");

            _posLive = GameObject.FindGameObjectWithTag ("LivesPos").transform;
        }

        private void UpdateLives()
        {
            //var go = GameObject.Instantiate(pref);
            while (_massLives.Count < _playerData.HP)
            {
                var prefTmp = GameObject.Instantiate(DataObjects.inst.GetValue<GameObject>("Util/ImgHeart"), _posLive);
                //prefTmp.transform.SetParent(Reference.inst.canvas);
                //prefTmp.transform.localScale = Vector3.one;
                var rt = prefTmp.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(_posX + _step * _massLives.Count, _posY, 0);
                _massLives.Add(prefTmp);
            }
            while (_massLives.Count > _playerData.HP)
            {
                GameObject.Destroy(_massLives[_massLives.Count - 1]);
                _massLives.RemoveAt(_massLives.Count - 1);
            }
        }

    }
}
