using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Thief : EnemyObject
{
    int thiefSpeed = 500;
    public Thief(Vector2 startPosition) : base(2, "Thief", false, false, false, false, false, false, true)
    {
        speed = thiefSpeed;
        position = startPosition;
        strength = 10;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Thieving();
    }

    private void Thieving()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player))
        {
            player.health -= strength;
            //steal item, run away
     
        }
    }
}

