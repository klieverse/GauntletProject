﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
   
class BreakableWall : Tile
{
    public int howBroken = 2;
    public BreakableWall(int layer, string id, Vector2 position)
    : base(assetname: "Tiles/BreakableWall", TileType.BreakableWall, layer, id)
    {
        this.position = position;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public void HitByShot()
    {
        howBroken -= 1;
        CheckBreak();
    }

    private void CheckBreak()
    {
        if (howBroken <= 0)
        {
            type = TileType.Background;
            if (visible)
            {
                GameEnvironment.AssetManager.PlaySound("Ghoblin attack", position.X);
                (GameWorld as Level).secretValue1++;
            }
            visible = false;

        }
    }

    public override void Reset()
    {
        base.Reset();
        howBroken = 2;
        type = TileType.BreakableWall;
    }
}
