using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TempleTrolls : AnimatedGameObject
{
    Vector2 spawnLocation;

    float timer = 0;
    public TempleTrolls(Vector2 startPosition) : base(2, "TempleTrolls")
    {
        this.position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        if (timer > 500)
        {
            Spawn();
            timer = 0;
        }

        Random r = new Random();
        int spawnPlace = r.Next(8);

        switch (spawnPlace)
        {
            case 0:
                if (!Collision)
                {
                    spawnLocation.X = position.X + 60;
                }
                break;
            case 1:
                if (!Collision)
                {
                    spawnLocation.X = position.X + 60;
                    spawnLocation.Y = position.Y + 60;
                }
                break;
            case 2:
                if (!Collision)
                {
                    spawnLocation.Y = position.Y + 60;
                }
                break;
            case 3:
                if (!Collision)
                {
                    spawnLocation.X = position.X - 60;
                    spawnLocation.Y = position.Y + 60;
                }
                break;
            case 4:
                if (!Collision)
                {
                    spawnLocation.X = position.X - 60;
                }
                break;
            case 5:
                if (!Collision)
                {
                    spawnLocation.X = position.X - 60;
                    spawnLocation.Y = position.Y - 60;
                }
                break;
            case 6:
                if (!Collision)
                {
                    spawnLocation.Y = position.Y - 60;
                }
                break;
            default:
                if (!Collision)
                {
                    spawnLocation.X = position.X + 60;
                    spawnLocation.Y = position.Y - 60;
                }
                break;
        }

    }

    private void Spawn()
    {
        Troll troll = new Troll(spawnLocation);
    }
}

