using System;

namespace Hole
{
    [Serializable]
    public enum TypeItem
    {
        None=0,
        Player=1,
        Coin=2,
        BonusHeart=100,
        BonusPoison=101,
        BonusInv=102,
        EnemyLaser=200,
        EnemyRocketLauncher=201,
        EnemyRocket = 202,
        EnemyFire=203,
        EnemyBurner=204,
        EnvSlow=500,
        EnvCollapse = 501,
    }

    
}