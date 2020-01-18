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
    : base(type.ToString(), type, layer, id)
    {
        this.position = position;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
            {
                //Als de player de deur raakt en een sleutel heeft moet de deur verdwijnen + eventuele deuren ernaast of erboven

                if (CollidesWith(player) && player.Key > 0)
                {
                    player.UseKey();

                    int x = (int)position.X / Size;
                    int y = (int)position.Y / Size;
                    DeleteDoors(x, y);
                }
            }
    }
    void DeleteDoors(int x,int y)
    {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        TileType type = tiles.GetTileType(x, y);
        List<GameObject> doors = (GameWorld.Find("Doors") as GameObjectList).Children;
        if (type != this.type)
        {
            return;
        }
        if(this.type == TileType.HorizontalDoor)
        {
            DeleteDoors(x, y);
            if(doors != null)
                foreach (Door door in doors)
                {

                }
            visible = false;
        }
    }
}

