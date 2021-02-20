using UnityEngine;

namespace Hole
{
    public class SetPosCam : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _pos;
        private Camera _cam;
        private Vector3 _posAddCam;
        private void Awake()
        {
            _cam = Camera.main;
            _posAddCam = _cam.transform.position;
        }

        private void LateUpdate()
        {
            _cam.transform.position = _pos.Value+_posAddCam;
        }
    }
}
