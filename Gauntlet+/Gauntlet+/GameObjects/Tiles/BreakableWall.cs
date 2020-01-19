using System;
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
        CheckBreak();
    }

    private void HitByShot()
    {
        howBroken -= 1;
    }

    private void CheckBreak()
    {
        if (howBroken <= 0)
        {
            visible = false;
        }
    }
}
