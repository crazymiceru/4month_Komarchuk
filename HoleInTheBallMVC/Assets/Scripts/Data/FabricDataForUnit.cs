using System;

namespace Hole
{
    internal sealed class FabricDataForUnit
    {
        internal Action<IUnit, int, object> addUnitMetod;
        internal String ObjName;
    }
}