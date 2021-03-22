using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    internal class RadarController : IExecute, IController, IInitialization
    {
        private Dictionary<int, RadarPoint> _points = new Dictionary<int, RadarPoint>();
        private Transform _player;
        [SerializeField] private float scaleRadar;
        [SerializeField] private float radiusRadar = 50;
        private float _angleCam;
        [HideInInspector]
        private Transform _posRadar;
        private RectTransform _posBackGround;
        private float _camScale = 0;
        private float _imgSize = 1024 / 2;
        private GameObject _cam2go;

        public void Initialization()
        {
            Debug.Log($"Init Radar");
            _player = Reference.inst.Player.transform;

            _angleCam = Reference.inst.MainCamera.transform.rotation.eulerAngles.y;
            _posRadar = GameObject.FindGameObjectWithTag("Radar").transform;
            _posBackGround = GameObject.FindGameObjectWithTag("RadarBackGround").GetComponent<RectTransform>();
            _cam2go = GameObject.FindGameObjectWithTag("Camera2");
            _camScale = _cam2go.GetComponent<Camera>().orthographicSize;
            scaleRadar = _imgSize / _camScale * _posBackGround.localScale.x;

            _cam2go.GetComponent<Camera>().Render();
            _cam2go.SetActive(false);
        }

        public void AddPoint(GameObject obj, GameObject imgIn)
        {
            var goRadar = GameObject.Instantiate(imgIn, _posRadar);
            goRadar.GetComponent<RectTransform>().localScale = _posBackGround.localScale * 2;
            var rp = new RadarPoint { posWord = obj.transform, ico = goRadar.transform };
            _points.Add(obj.GetInstanceID(), rp);
            SetPoint(rp);
        }

        public void DelPoint(GameObject obj)
        {
            var id = obj.GetInstanceID();
            GameObject.Destroy(_points[id].ico.gameObject);
            _points.Remove(id);
        }

        public void Execute(float deltaTime)
        {
            if (_player != null)
            {
                foreach (var point in _points)
                {
                    SetPoint(point.Value);
                }
                _posBackGround.localPosition = Quaternion.Euler(0, 0, _angleCam) * (new Vector3(-_player.position.x, -_player.position.z, 0) * scaleRadar);
            }
        }

        private void SetPoint(RadarPoint point)
        {
            var v = (point.posWord.position - _player.position) * scaleRadar;
            var vImg = new Vector3(v.x, v.z, 0);

            vImg = Quaternion.Euler(0, 0, _angleCam) * vImg;

            if (vImg.x > -radiusRadar && vImg.x < radiusRadar && vImg.y > -radiusRadar && vImg.y < radiusRadar)
            {
                if (!point.ico.gameObject.activeSelf) point.ico.gameObject.SetActive(true);
                point.ico.localPosition = vImg;
            }
            else
            {
                if (point.ico.gameObject.activeSelf) point.ico.gameObject.SetActive(false);
            }
        }
    }
}