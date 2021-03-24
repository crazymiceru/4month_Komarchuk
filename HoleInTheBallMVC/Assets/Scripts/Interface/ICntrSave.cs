namespace Hole
{
    internal interface ICntrSave
    {
        DataGameForSave Save();
        void Load(DataGameForSave data);
    }
}