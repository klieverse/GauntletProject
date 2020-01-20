using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

//Moeten nog even kijken voor de sprites of we twee losse classes nodig hebben of niet
class Door : Tile
{
    public Door(int layer, string id, Vector2 position, TileType type)
    : base("Tiles/" + id, type, layer, id)
    {
        this.position = position;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public void DeleteDoors()
    {
        int x = (int)position.X / Size;
        int y = (int)position.Y / Size;
        DeleteDoors1(x, y);
        DeleteDoors2(x, y);
        visible = false;
        type = TileType.Background;
    }

    void DeleteDoors1(int x, int y)// deletes doors right and down
    {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        TileType type = tiles.GetTileType(x, y);
        List<GameObject> doors = (GameWorld.Find("Doors") as GameObjectList).Children;
        if (type != this.type)
        {
            return;
        }
        else
        {
            if (this.type == TileType.HorizontalDoor)
                DeleteDoors1(x + 1, y);
            if (this.type == TileType.VerticalDoor)
                DeleteDoors1(x, y + 1);
            if (doors != null)
                foreach (Door door in doors)
                {
                    if(door != this)
                        if (door.position.X / Size == x && door.position.Y / Size == y)
                        {
                            door.visible = false;
                            door.type = TileType.Background;
                        }
                }
        }
    }
    void DeleteDoors2(int x, int y) // deletes doors left and up
    {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        TileType type = tiles.GetTileType(x, y);
        List<GameObject> doors = (GameWorld.Find("Doors") as GameObjectList).Children;
        if (type != this.type)
        {
            return;
        }
        else
        {
            if (this.type == TileType.HorizontalDoor)
                DeleteDoors2(x - 1, y);
            if (this.type == TileType.VerticalDoor)
                DeleteDoors2(x, y - 1);
            if (doors != null)
                foreach (Door door in doors)
                {
                    if(door != this)
                        if (door.position.X / Size == x && door.position.Y / Size == y)
                        {
                            door.visible = false;
                            door.type = TileType.Background;
                        }
                }
        }
    }
}

