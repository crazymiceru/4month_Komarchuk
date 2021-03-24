using System;

namespace Hole
{
    [Serializable]
    public class DataGameForSave
    {
        public string name;
        public TypeItem type;
        public int numCfg;
        public Vector3Serializable pos;
        public Vector3Serializable angle;
        public int hp;
        public int scores;
    }
}