using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Wizard : EnemyObject
{
    float timer = 0f;
    float visibilityTimer = 0f;
    public Wizard(Vector2 startPosition) : base(2, "Wizard")
    {
        this.position = startPosition;
        strength = 10;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //strength based on current health
        if (this.health < 21)
            strength = 8;
        if (this.health < 11)
            strength = 5;
        //removes this object when health is less than 1
        if (this.health < 1)
        {
            GameWorld.Remove(this);
        }
        visibilityTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        //wizard can get invisible, timer which switches state of visibility
        if (visibilityTimer > 500)
        {
            visible = !visible;
            visibilityTimer = 0;
        }
        //calculates when the enemy can attack
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

    //find player, if collides, then player loses health according to this' strength;
    private void Attack()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player) && visible)
        {
            player.health -= strength;
        }
    }
}

