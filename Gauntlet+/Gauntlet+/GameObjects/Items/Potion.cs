﻿using Microsoft.Xna.Framework;
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
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                }
    }
}
