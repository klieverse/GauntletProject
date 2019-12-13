using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Hellhound : EnemyObject
{
    float timer = 0f;
    public Hellhound(Vector2 startPosition) : base(2, "Hellhound")
    {
        //starting position equal to what is determined in SpawnObject.cs
        position = startPosition;
        strength = 10;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //strength of enemy reduces with current health
        if (this.health < 21)
            strength = 8;
        if (this.health < 11)
            strength = 5;
        if (this.health < 1)
        {
            GameWorld.Remove(this);
        }
        //cooldown for when the enemy attacks
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

    //attacks by creating a shooting object, where the object shoots in the direction in which the enemy is moving
    private void Attack()
    {
        HellhoundShoot hellhoundShoot = new HellhoundShoot(this.position, this.velocity, strength);
    }
}

