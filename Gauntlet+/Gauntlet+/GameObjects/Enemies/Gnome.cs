using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Gnome : EnemyObject
{
    float timer = 0f;

    public Gnome(Vector2 startPosition) : base(2, "Gnome")
    {
        //position equal to position given in LevelLoading.cs
        this.position = startPosition;
        strength = 3;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //cooldown in which the enemy attacks
        timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        if (timer > 1000f)
        {
            Attack();
            timer = 0f;
        }
    }

    //enemy creates shooting object that shoots in the direction in which the gnome is facing.
    private void Attack()
    {
       // GnomeShoot gnomeShoot = new GnomeShoot(this.position, this.velocity, strength);
        (GameWorld.Find("enemieShot") as GameObjectList).Add(new GnomeShoot(this.position, this.velocity, strength));
    }
}
