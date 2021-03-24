using System;

namespace Hole
{
    internal sealed class FabricDataForUnit
    {
        internal Action<IUnit, int, object,DataGameForSave> addUnitMetod;
        internal String ObjName;
    }
}