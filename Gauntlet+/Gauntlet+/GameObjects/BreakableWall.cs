using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
   
class BreakableWall : SpriteGameObject
{
    
    public BreakableWall(int layer, string id, Vector2 position)
    : base("BreakableWall", layer, id)
    {
        this.position = position;
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
