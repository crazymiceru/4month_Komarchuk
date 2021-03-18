using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hole
{

    public class UnitView : MonoBehaviour, IInteractive, IUnit
    {
        internal event Action<IInteractive,bool> evtOutInteractive = delegate { };
        internal event Action<PackInteractiveData,bool> evtInInteractive = delegate { };
        internal event Action evtAnyCollision = delegate { };

        private Rigidbody _rb;
        [SerializeField] private TypeItem _typeItem;
        [SerializeField] internal int NumCfg=0;
        [SerializeField] private Transform[] _positionInfo;
        private Dictionary<int, int> _listOntriggerEnter = new Dictionary<int, int>();

        internal Transform[] PositionInfo
        {
            get => _positionInfo;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var ID = other.gameObject.GetInstanceID();
//            _listOntriggerEnter.Add(other.gameObject.GetInstanceID(), 1);
            _listOntriggerEnter[ID] = _listOntriggerEnter.ContainsKey(ID) ? _listOntriggerEnter[ID]+1 : 1;

            if (_listOntriggerEnter[ID] == 1)
            {
                if (other.gameObject.TryGetComponent(out IInteractive unitInteractive))
                {
                    //Debug.Log($"Object {gameObject.name} OnTriggerEnter:{other.gameObject.name}");
                    evtOutInteractive.Invoke(unitInteractive, true);
                }
                evtAnyCollision.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var ID = other.gameObject.GetInstanceID();
            _listOntriggerEnter[ID]--;

            if (_listOntriggerEnter[ID] == 0)
            {
                if (other.gameObject.TryGetComponent(out IInteractive unitInteractive))
                {
                    //Debug.Log($"Object {gameObject.name} OnTriggerExit:{other.gameObject.name}");
                    evtOutInteractive.Invoke(unitInteractive, false);
                }
                evtAnyCollision.Invoke();
            }
        }

        internal void Kill()
        {
            Destroy(gameObject);
        }

        public (TypeItem,int) GetTypeItem()
        {
            return (_typeItem,NumCfg);
        }

        void IInteractive.InInteractive(PackInteractiveData data,bool isEnter)
        {
            evtInInteractive(data,isEnter);
        }
    }
}