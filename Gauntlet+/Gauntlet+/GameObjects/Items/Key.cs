﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;


class Key : Item
{
    public Key(int layer, string id, Vector2 position)
        : base(layer: 0, id, position)
    {
        visible = true;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player) && !player.InventoryFull) 
                {
                    player.AddKey();
                    visible = false;
                    GameEnvironment.AssetManager.PlaySound("Key", position.X);
                }
    }
}

