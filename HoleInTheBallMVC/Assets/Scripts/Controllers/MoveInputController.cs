using System;
using UnityEngine;

namespace Hole
{
    internal sealed class MoveInputController : IController, IExecute
    {
        private UnitM _unit;
        private float _angleCam;
        private ControlLeak _controlLeak = new ControlLeak("MoveInput");
        private Vector2 v2z = Vector2.zero;
        private Vector2 deltaTouch = Vector2.zero;

        internal MoveInputController(UnitM unit)
        {
            _unit = unit;
            _angleCam=Reference.inst.MainCamera.transform.rotation.eulerAngles.y;
            _unit.evtKill += Kill;
        }

        public void Execute(float deltaTime)
        {
            float v, h;

#if UNITY_ANDROID && !UNITY_EDITOR

            (v,h)=GetAxisAndroid();

#else
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
#endif

            if (v != 0 || h != 0)
            {
                var control = new Vector3(h, 0, v);
                control = Quaternion.Euler(0, _angleCam, 0) * control;
                _unit.control = control;
            }
            else
            {
                _unit.control = Vector3.zero;
            }
        }

        private (float v, float h) GetAxisAndroid()
        {
            Vector2 deltaTouch=v2z;
            //bool isScreenTouch;

            foreach (var touch in Input.touches)
            {
                //if (touch.phase == TouchPhase.Began)
                //{
                //    pos = touch.position;
                //}
                //if (touch.phase == TouchPhase.Ended)
                //{
                //    deltaTouch = v2z;
                //}
                if (touch.phase == TouchPhase.Moved)
                {
                    deltaTouch = touch.deltaPosition.normalized;
                    //isScreenTouch = true;
                }
            }
            if (Input.touches.Length == 0)
            {
                deltaTouch = v2z;
            }


            return (deltaTouch.y, deltaTouch.x);
        }

        private void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}