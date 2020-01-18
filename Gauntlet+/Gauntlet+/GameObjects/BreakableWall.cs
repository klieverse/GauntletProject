using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
   
class BreakableWall : Tile
{
    public int howBroken = 3;
    public BreakableWall(int layer, string id, Vector2 position)
    : base("BreakableWall", TileType.BreakableWall, layer, id)
    {
        this.position = position;
    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    /*
    public void WallBreaker()
    {
        if (WallCounter <= 0)
        {
            invisibility = true;

            return;
        }
        return;

    }

    */


}
