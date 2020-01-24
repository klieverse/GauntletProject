using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Gnome : EnemyObject
{
    float timer = 0f;

    public Gnome(Vector2 startPosition) : base(2, "Gnome", 500)
    {
        position = startPosition;
        strength = 3;
    }

    public override void Update(GameTime gameTime)
    {
        if (!isDead)
        {
            base.Update(gameTime);
            if (this.health < 21)
                strength = 8;
            if (this.health < 11)
                strength = 5;
            if (this.health < 1)
            {
                visible = false;
                isDead = true;
            }
            //cooldown in which the enemy attacks
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > 1000f)
            {
                Attack();
                timer = 0f;
            }
        }
        
    }

    //enemy creates shooting object that shoots in the direction in which the gnome is facing.
    private void Attack()
    {
       // GnomeShoot gnomeShoot = new GnomeShoot(this.position, this.velocity, strength);
        (GameWorld.Find("enemieShot") as GameObjectList).Add(new GnomeShoot(this.position, new Vector2(speedHori, speedVert), strength));
    }
}
