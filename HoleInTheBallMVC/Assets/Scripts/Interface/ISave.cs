using System.Collections.Generic;

namespace Hole
{
    internal interface ISave
    {
        public void Save<T>(List<T> data, string name);
        public List<T> Load<T>(string name);
    }
}