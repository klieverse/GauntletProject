using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Portal : SpriteGameObject
{
    Portal closestPortal;
    float distance = 0f;
    bool left = false;

    public Portal(Vector2 startPosition, string id) : base(assetName: "Teleport", layer: 0, id, sheetIndex: 0)
    {
        this.position = startPosition;

        //go through all portals in level (missing: list of all portals
        List<GameObject> portals = (GameWorld.Find("Teleport") as GameObjectList).Children;
        foreach(Portal portal in portals)
        {
            float opposite = portal.Position.Y - position.Y;
            float adjacent = portal.Position.X - position.X;
            float hypotenuse = (float)Math.Sqrt(Math.Pow(opposite, 2) + Math.Pow(adjacent, 2));
            if (hypotenuse > distance)
            {
                this.closestPortal = portal;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    private void Teleport()
    {
        Player player = GameWorld.Find("Elf") as Player;
        if (left == false)
        {
            if (CollidesWith(player))
            {
                closestPortal.left = true;
                player.Position = closestPortal.Position;

            }
        }
        else
        {
            if (!CollidesWith(player))
            {
                left = false;
            }
        }
        
    }
}

