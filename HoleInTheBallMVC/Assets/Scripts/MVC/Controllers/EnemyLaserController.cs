using UnityEngine;

namespace Hole
{
    internal sealed class EnemyLaserController : IController, IExecute, IInitialization
    {
        private UnitM _unit;
        private UnitView _unitView;
        private LineRenderer _line;
        private Transform _startPos;
        private float _addLenghtLine = 0.2f;
        internal EnemyLaserController(UnitM unit, UnitView unitView)
        {
            _unit = unit;
            _unitView = unitView;                  
            _unit.evtKill += Kill;
        }

        public void Initialization()
        {
            _line = _unitView.transform.Find("Line").gameObject.GetComponent<LineRenderer>();
            if (_line==null) Debug.LogError($"Dont find Line in {_unitView.name}");
            _startPos = _unitView.transform.Find("StartPos");
            if (_startPos == null) Debug.LogError($"Dont find StartPos in {_unitView.name}");
        }

        public void Execute(float deltaTime)
        {
            RaycastHit hit;
            Ray ray = new Ray(_startPos.position, _unitView.transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                _line.SetPosition(1, new Vector3(0, 0, hit.distance + _addLenghtLine));
                if (hit.transform.gameObject.TryGetComponent(out IInteractive unit))
                {
                    //Debug.Log($"атака {gameObject.name}");
                    unit.InInteractive(_unit.packInteractiveData);
                }
            }

        }


        void Kill()
        {
            ListControllers.inst.Delete(this);
        }

    }
}