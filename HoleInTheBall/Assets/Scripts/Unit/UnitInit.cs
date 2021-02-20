using UnityEngine;

namespace Hole
{
    public class UnitInit : MonoBehaviour
    {
        [SerializeField] protected UnitData _unitData;
        [SerializeField] protected bool _isInteractive = false;
        //Explosion position on unit
        [SerializeField] protected Transform _posExp;
        protected int _lives;
        protected int _scores;

        protected Transform _pos;
        public int lives
        {
            get
            {
                return _lives;
            }

            protected set
            {
                _lives = value;
                UpdateParameters();
            }
        }
        public int scores
        {
            get
            {
                return _scores;
            }
            set
            {
                _scores = value;
                UpdateParameters();
            }
        }

        protected float _timeInvulnerability = 0;

        protected virtual void Awake()
        {
            _pos = transform;
        }

        private void Start()
        {
            lives = _unitData.maxLive;
            scores = 0;
            UpdateParameters();
        }

        protected virtual void UpdateParameters()
        {
            //Debug.Log($"Live Changed {gameObject.name} : {live}");
        }

    }
}