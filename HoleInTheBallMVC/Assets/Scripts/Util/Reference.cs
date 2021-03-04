using UnityEngine;

namespace Hole
{
    internal sealed class Reference
    {
        private GameObject _player;
        private Camera _mainCamera;
        private Canvas _canvas;
        internal static Reference inst;
        public UnitM playerData;
        private GameObject _goInvulnerAbility;

        internal Reference()
        {
            if (inst == null) inst = this;
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
            get=> _goInvulnerAbility!=null ? _goInvulnerAbility: _goInvulnerAbility=GameObject.FindGameObjectWithTag("Invulnerability");
        }

    }
}