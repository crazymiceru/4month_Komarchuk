using UnityEngine;

namespace Hole
{
    internal sealed class Reference
    {
        private GameObject _player;
        private Camera _mainCamera;
        private Canvas _canvas;
        internal static Reference inst;
        private UnitM _playerData;
        private GameObject _goInvulnerAbility;
        private ControlLeak _controlLeak = new ControlLeak("Reference");

        internal SaveController saveController;

        internal Reference()
        {
            inst = this;
        }

        internal Camera MainCamera
        {
            get => _mainCamera != null ? _mainCamera : _mainCamera = Camera.main;
        }

        internal GameObject Player
        {
            get => _player != null ? _player : _player = GameObject.FindGameObjectWithTag("Player");
        }

        internal Canvas canvas
        {
            get => _canvas != null ? _canvas : _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        }

        internal GameObject GoInvulnerAbility
        {
            get => _goInvulnerAbility != null ? _goInvulnerAbility : _goInvulnerAbility = GameObject.FindGameObjectWithTag("Invulnerability");
        }

        public RadarController radarController
        {
            get => _radar != null ? _radar : _radar = new RadarController();
        }
        private RadarController _radar;

        public UnitM playerData
        {
            get => _playerData != null ? _playerData : _playerData = new UnitM();
        }


    }
}