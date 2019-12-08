
using Microsoft.Xna.Framework;

enum PotionType
{
    Normal,
    Orange,
    Armor,
    Magic,
    ShotPower,
    ShotSpeed,
    Speed,
    Melee
}

class Potion: SpriteGameObject
{
    public Potion(PotionType pt, string assetname = "", int layer = 2, string id = "")
        : base(assetname, layer, id)
    {

    }
}
