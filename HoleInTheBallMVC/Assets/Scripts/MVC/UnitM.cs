using System;
using UnityEngine;

namespace Hole
{

    internal sealed class UnitM
    {
        internal int _hp=-1;
        internal event Action evtKill=delegate { };
        internal event Action evtScores = delegate { };
        internal event Action evtLives = delegate { };
        internal event Action evtDecLives = delegate { };

        internal PackInteractiveData packInteractiveData=new PackInteractiveData();


        internal bool isInvulnerability;
        internal float startTimeInvulnerability;
        internal float addTimeInvulnerability;

        public int HP
        {
            get => _hp;
            set
            {
                    if (_hp != value && (_hp > -1 || value > 0))
                    {                        
                        if (_hp < value)
                        {
                            _hp = value;
                            evtLives.Invoke();
                        }

                        if (_hp > value)
                        {
                            if (!isInvulnerability)
                            {
                                isInvulnerability = true;
                                startTimeInvulnerability = Time.time + addTimeInvulnerability;
                                _hp = value;
                                evtDecLives.Invoke();
                                evtLives.Invoke();
                            }
                        }

                        if (_hp <= 0)
                        {
                            _hp = 0;
                            evtKill();
                        }

                    }
            }
        }

        internal int _scores=0;
        public int Scores
        {
            get => _scores;
            set
            {
                _scores = value;
                evtScores();
            }
        }

        internal Vector3 control=Vector3.zero;
    }

}