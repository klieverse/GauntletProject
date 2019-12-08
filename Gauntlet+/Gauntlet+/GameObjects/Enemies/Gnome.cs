using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Gnome : EnemyObject
{
    float timer = 0f;

    public Gnome(Vector2 startPosition) : base(2, "Gnome", false, true, false, false, false, false, false)
    {
        this.position = startPosition;
        strength = 3;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timer > 1f)
        {
            Gnoming();
            timer = 0f;
        }
    }

    private void Gnoming()
    {
        GnomeShoot gnomeShoot = new GnomeShoot(this.position, this.velocity, strength);
        GameWorld.Add(gnomeShoot);
    }
}
