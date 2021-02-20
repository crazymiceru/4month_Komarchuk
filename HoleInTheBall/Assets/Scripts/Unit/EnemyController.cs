using System.Collections.Generic;
using UnityEngine;

namespace Hole
{
    internal class EnemyController : UnitRB, IUnitControlTraectory
    {
        [Header ("Traectory Move")]
        [SerializeField] private List<Traectory> _traectory;
        private int _numTraectory = 0;
        [SerializeField] private float _minSqrDistance = 1;

        void IUnitControlTraectory.Update()
        {
            if (_traectory.Count > 0)
            {
                _h = Mathf.Sign(_traectory[_numTraectory].transform.position.x - transform.position.x) * _traectory[_numTraectory].powerMove;
                _v = Mathf.Sign(_traectory[_numTraectory].transform.position.z - transform.position.z) * _traectory[_numTraectory].powerMove;

                var d = Util.SqrDist(Util.ClearVectorY(transform.position), Util.ClearVectorY(_traectory[_numTraectory].transform.position));
                if (Util.SqrDist(Util.ClearVectorY(transform.position), Util.ClearVectorY(_traectory[_numTraectory].transform.position)) < _minSqrDistance)
                {
                    _numTraectory++;
                    if (_numTraectory == _traectory.Count) _numTraectory = 0;
                }
            }
        }
    }
}