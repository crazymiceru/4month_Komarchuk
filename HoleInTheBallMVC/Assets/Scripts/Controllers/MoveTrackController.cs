using UnityEngine;

namespace Hole
{
    internal sealed class MoveTrackController : IController, IExecute, IInitialization
    {
        UnitM _unit;
        UnitViewTraectory _unitView;
        private UnitData _unitData;
        private int _numTraectory;
        private ControlLeak _controlLeak = new ControlLeak("MoveTrack");

        internal MoveTrackController(UnitM unit, UnitViewTraectory unitView, UnitData unitData)
        {
            _unit = unit;
            _unitData = unitData;
            _unitView = unitView;
            _unit.evtKill += Kill;

        }

        public void Initialization()
        {
            _numTraectory = 0;
        }

        public void Execute(float deltaTime)
        {
            if (_unitView.Track.Length > 0)
            {
                _unit.control.x = Mathf.Sign(_unitView.Track[_numTraectory].transform.position.x - _unitView.transform.position.x) * _unitView.Track[_numTraectory].powerMove;
                _unit.control.z = Mathf.Sign(_unitView.Track[_numTraectory].transform.position.z - _unitView.transform.position.z) * _unitView.Track[_numTraectory].powerMove;
                if (Mathf.Abs(_unitView.Track[_numTraectory].transform.position.x - _unitView.transform.position.x)
                    <= _unitView.Track[_numTraectory].powerMove * deltaTime) _unit.control.x = 0;
                if (Mathf.Abs(_unitView.Track[_numTraectory].transform.position.z - _unitView.transform.position.z)
                    <= _unitView.Track[_numTraectory].powerMove * deltaTime) _unit.control.z = 0;

                var d = Util.SqrDist(_unitView.transform.position.ClearVectorY(), _unitView.Track[_numTraectory].transform.position.ClearVectorY());
                if (d < _unitData.minSqrDistance * deltaTime)
                {
                    _numTraectory++;
                    if (_numTraectory == _unitView.Track.Length) _numTraectory = 0;
                    //Debug.Log($"Next Traectory {_unitView.Track[_numTraectory].transform.gameObject.name}");
                }
            }
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}