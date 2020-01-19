﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


class Exit : Tile
{

    public Exit(int layer, string id, Vector2 position)
    : base("Exit", TileType.Exit, layer, id)
    {
        this.position = position;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        CheckCollision();
    }

    private void CheckCollision()
    {
        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                    PlayingState.NextLevel();
                }
    }
}