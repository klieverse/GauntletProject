using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Teleport : Tile
{
    Teleport closestPortal;
    float distance = 9999999999f;
    bool teleportAllowed = true;

    public Teleport(Vector2 startPosition, int layer = 0) : base("Tiles/Teleport", TileType.Teleporter, layer, id: "teleport")
    {
        //position is equal to given position in LevelLoading.cs
        this.position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        FindClosestPortal();
        Teleporting();
    }

    private void FindClosestPortal()
    {
        //check for all the portals in the level
        List<GameObject> portals = (GameWorld.Find("teleport") as GameObjectList).Children;
        foreach (Teleport portal in portals)
        {
            //determines the closest portal to this portal, other than this portal itself
            if (portal != this)
            {
                float opposite = portal.Position.Y - position.Y;
                float adjacent = portal.Position.X - position.X;
                float hypotenuse = (float)Math.Sqrt(Math.Pow(opposite, 2) + Math.Pow(adjacent, 2));
                if (hypotenuse < distance)
                {
                    distance = hypotenuse;
                    this.closestPortal = portal;
                }
            }

        }
    }

    private void Teleporting()
    {
        //if the portal collides with the player, the position of the player becomes the same as the closest portal
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (SpriteGameObject player in players)
                if (teleportAllowed == true)
                {
                    if (CollidesWith(player))
                    {
                        //if the player teleported, this boolean prevents it from immediately teleporting again
                        closestPortal.teleportAllowed = false;
                        player.Position = new Vector2(closestPortal.Position.X + Tile.Size/2, closestPortal.Position.Y + Tile.Size);

                    }
                }
        else
        {
            //this allows it to teleport again if the player doesn't collide with the portal anymore
            if (!(CollidesWith(player)))
            {
                teleportAllowed = true;
            }
        }

    }
}
