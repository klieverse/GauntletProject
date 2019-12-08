using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Hellhound : EnemyObject
{
    float timer = 0f;
    public Hellhound(Vector2 startPosition) : base(2, "Hellhound", true, false, false, false, false, false, false)
    {
        position = startPosition;
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
        if (timer == 0f)
        {
            Hellhounding();
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        else if (timer > 500f)
        {
            timer = 0f;
        }
    }

    private void Hellhounding()
    {
        HellhoundShoot hellhoundShoot = new HellhoundShoot(this.position, this.velocity, strength);
    }
}

