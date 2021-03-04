using UnityEngine;

namespace Hole
{
    internal sealed class MoveTrackController : IController,IExecute,IInitialization
    {
        UnitM _unit;
        UnitViewTraectory _unitView;
        private UnitData _unitData;
        private int _numTraectory;        

        internal MoveTrackController(UnitM unit,UnitViewTraectory unitView, UnitData unitData)
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

                var d = Util.SqrDist(Util.ClearVectorY(_unitView.transform.position), Util.ClearVectorY(_unitView.Track[_numTraectory].transform.position));
                if (Util.SqrDist(Util.ClearVectorY(_unitView.transform.position), Util.ClearVectorY(_unitView.Track[_numTraectory].transform.position)) < _unitData.minSqrDistance)
                {
                    _numTraectory++;
                    if (_numTraectory == _unitView.Track.Length) _numTraectory = 0;
                }
            }
        }

        void Kill()
        {
            ListControllers.inst.Delete(this);
        }
    }
}