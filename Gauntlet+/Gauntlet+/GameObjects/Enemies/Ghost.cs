using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Ghost : EnemyObject
{
    float counts = 0f;
    public Ghost(Vector2 startPosition) : base(2, "Ghost", false, false, false, false, true, false, false)
    {
        this.position = startPosition;
        strength = 30;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (health < 21)
            strength = 20;
        if (health < 11)
            strength = 10;
        if (health < 1)
        {
            //Delete instance
        }
        Ghosting();
    }

    private void Ghosting()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player))
        {
            player.health -= strength;
            GameWorld.Remove(this);
        } 
        
//        if (CollidesWithObject())
//        {
            //Assuming it can differentiate wall and player
            //Player.Health -= strength;
            //Yeetus deletus instance;
//        }
    }
}

