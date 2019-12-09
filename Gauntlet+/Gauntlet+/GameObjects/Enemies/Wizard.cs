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
        if (this.health < 21)
            strength = 8;
        if (this.health < 11)
            strength = 5;
        if (this.health < 1)
        {
            //Delete instance
        }
        visibilityTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (visibilityTimer > 500)
        {
            visible = !visible;
            visibilityTimer = 0;
        }
        if (timer == 0f)
        {
            Wizarding();
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        else if (timer > 500f)
        {
            timer = 0f;
        }
    }

    private void Wizarding()
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player) && visible)
        {
            player.health -= strength;
        }
    }
}

