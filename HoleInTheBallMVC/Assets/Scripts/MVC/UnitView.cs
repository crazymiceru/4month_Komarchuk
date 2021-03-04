using System;
using UnityEngine;

namespace Hole
{

    public class UnitView : MonoBehaviour, IInteractive, IUnit
    {
        internal event Action<IInteractive> evtOutInteractive = delegate { };
        internal event Action<PackInteractiveData> evtInInteractive = delegate { };
        internal event Action evtAnyCollision = delegate { };

        private Rigidbody _rb;
        [SerializeField] private TypeItem _typeItem;
        [SerializeField] private int _numCfg=0;
        [SerializeField] private Transform[] _positionInfo;

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
            if (other.gameObject.TryGetComponent(out IInteractive unitInteractive))
            {
                evtOutInteractive.Invoke(unitInteractive);
            }
            evtAnyCollision.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            evtAnyCollision.Invoke();
        }

        internal void Kill()
        {
            Destroy(gameObject);
        }

        public (TypeItem,int) GetTypeItem()
        {
            return (_typeItem,_numCfg);
        }

        void IInteractive.InInteractive(PackInteractiveData data)
        {
            evtInInteractive(data);
        }
    }
}