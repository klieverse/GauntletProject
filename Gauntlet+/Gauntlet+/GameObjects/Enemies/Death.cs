using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Death : EnemyObject  
{
    float timer = 0f;
    public Death(Vector2 startPosition) : base(2, "Death")
    {
        this.position = startPosition;
        strength = 4;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (timer == 0f)
        {
            Deathing();
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        else if (timer > 50f)
        {
            timer = 0f;
        }
    }
    private void Deathing()
    {
        Player player = GameWorld.Find("player") as Player;
        if(CollidesWith(player))
        {
            player.HitByEnemy(strength);
        }
    }
}

