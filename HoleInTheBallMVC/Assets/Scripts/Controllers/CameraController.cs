using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hole
{
    internal sealed class CameraController : IExecute, IDisposable, IController
    {
        private Transform _player;
        private Transform _mainCamera;
        private Vector3 _offset;
        private float _currentTimeShake;
        private float _lenghthTimeShake = 1;
        private float _rangeShake=0.1f;
        private UnitM _unit;
        private ControlLeak _controlLeak = new ControlLeak("Camera");


        public CameraController(UnitM unit, Transform player, Transform mainCamera)
        {
            _player = player;
            _mainCamera = mainCamera;
            _offset = _mainCamera.position - _player.position;          
            _currentTimeShake = Time.time;
            _unit = unit;
            _unit.evtDecLives += DecLive;
        }

        public void Execute(float deltaTime)
        {            
            Vector3 vRnd;
            if (_player != null)
            {
                
                if (_currentTimeShake > Time.time)
                {
                    vRnd = new Vector3(Random.Range(-_rangeShake, _rangeShake), Random.Range(-_rangeShake, _rangeShake), Random.Range(-_rangeShake, _rangeShake));
                }
                else vRnd = Vector3.zero;
                _mainCamera.position = _player.position + _offset +vRnd;
            }
        }

        private void DecLive()
        {
                Debug.Log($"Shake");
                _currentTimeShake = Time.time + _lenghthTimeShake;
        }

        public void Dispose()
        {
            //Reference.inst.LstCanvas.UnregisterListener(DecLive);
        }
    }
}
