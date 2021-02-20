using UnityEngine;

namespace Hole
{
    public class GetPosPlayerForCam : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _pos;
        private Vector3 _startPosPlayer;

        private void Awake()
        {
            _startPosPlayer = transform.position;
        }

        private void LateUpdate()
        {
            _pos.Value = transform.position - _startPosPlayer;
        }

    }
}
