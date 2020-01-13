using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Teleport : SpriteGameObject
{
    Teleport closestPortal;
    float distance = 0f;
    bool left = false;

    public Teleport(Vector2 startPosition, int layer = 0) : base(assetName: "Tiles/Teleport", layer, id: "teleport", sheetIndex: 0)
    {

        this.position = startPosition;

        //go through all portals in level (missing: list of all portals
/*        List<GameObject> portals = (GameWorld.Find("Teleport") as GameObjectList).Children;
        foreach(Teleport portal in portals)
        {
            float opposite = portal.Position.Y - position.Y;
            float adjacent = portal.Position.X - position.X;
            float hypotenuse = (float)Math.Sqrt(Math.Pow(opposite, 2) + Math.Pow(adjacent, 2));
            if (hypotenuse > distance)
            {
                this.closestPortal = portal;
            }
        }*/
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //check
        List<GameObject> portals = (GameWorld.Find("teleport") as GameObjectList).Children;
        foreach (Teleport portal in portals)
        {
            float opposite = portal.Position.Y - position.Y;
            float adjacent = portal.Position.X - position.X;
            float hypotenuse = (float)Math.Sqrt(Math.Pow(opposite, 2) + Math.Pow(adjacent, 2));
            if (hypotenuse > distance)
            {
                this.closestPortal = portal;
            }
        }
        Teleporting();
    }

    private void Teleporting()
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

