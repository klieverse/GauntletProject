using Microsoft.Xna.Framework;
using System.Collections.Generic;

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

class Potion : Item
{
    

    public Potion(PotionType pot, int layer, string id, Vector2 position)
        : base(layer, id, position)
    {
        PotType = pot;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        CheckPlayerCollision();
    }

    private void CheckPlayerCollision()
    {
        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                    visible = false;
                    GameEnvironment.AssetManager.PlaySound("Key", position.X);
                    switch (PotType)
                    {
                        case PotionType.Normal:
                        case PotionType.Orange:
                            player.AddPotion(PotType);
                            break;
                        case PotionType.Armor:
                            player.ArmorUp();
                            break;
                        case PotionType.Magic:
                            player.MagicUp();
                            break;
                        case PotionType.Melee:
                            player.MeleeUp();
                            break;
                        case PotionType.ShotPower:
                            player.ShotPowerUp();
                            break;
                        case PotionType.ShotSpeed:
                            player.ShotSpeedUP();
                            break;
                        case PotionType.Speed:
                            player.SpeedUp();
                            break;
                        default: break;
                    }

                }
    }

    public PotionType PotType
    {
        get;

    }
}
