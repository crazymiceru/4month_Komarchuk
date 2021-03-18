using System;
using UnityEngine;

namespace Hole
{

    public sealed class UnitViewTraectory : UnitView,ITraectory
    {
        [SerializeField] private Traectory[] _track;

        public Traectory[] Track
        {
            get => _track;
        }
    }
}