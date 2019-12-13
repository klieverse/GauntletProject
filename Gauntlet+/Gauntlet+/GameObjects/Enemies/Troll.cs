using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Troll : EnemyObject
{
    float timer = 0f;
    public Troll(Vector2 startPosition) : base(2, "Troll")
    {
        this.position = startPosition;
        strength = 10;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //strength is based on current health
        if (this.health < 21)
            strength = 8;
        if (this.health < 11)
            strength = 5;
        //dies if health is less than 1
        if (this.health < 1)
        {
            GameWorld.Remove(this);
        }
        //timer for when it can attack
        if (timer == 0f)
        {
            Attack();
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        else if (timer > 500f)
        {
            timer = 0f;
        }
        
    }

    //finds player, if collides, then reduce player's health according to this' strength
    private void Attack()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player))
        {
            player.health -= strength;
        }
    }
}

