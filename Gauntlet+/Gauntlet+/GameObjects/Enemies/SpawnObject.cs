using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SpawnObject : SpriteGameObject
{
    public Vector2 spawnLocation;
    Random r;
    float timer = 0;
    TileField tileField;
    readonly string spawnId = "";

    public SpawnObject(Vector2 startPosition, string spawnId ) : base("",2,"Skeleton")
    {
        position = startPosition;
        r = new Random();
        tileField = GameWorld.Find("Tiles") as TileField;
        this.spawnId = spawnId;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //cooldown timer for when an enemy is getting spawned
        timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        if (timer > 500)
        {
            NewLocation();
            Spawn();
            timer = 0;
        }
        
    }

    private void NewLocation()
    {
        //gives a random number from 0-7, where each number has determines the location of spawn from the Spawner
        int spawnPlace = r.Next(8);
        switch (spawnPlace)
        {
            case 0:
                spawnLocation.X = position.X + 60;
                if (CollidesWithObject())
                    NewLocation();
                break;
            case 1:
                spawnLocation.X = position.X + 60;
                spawnLocation.Y = position.Y + 60;
                if (CollidesWithObject())
                    NewLocation();
                break;
            case 2:
                spawnLocation.Y = position.Y + 60;
                if (CollidesWithObject())
                    NewLocation();
                break;
            case 3:
                spawnLocation.X = position.X - 60;
                spawnLocation.Y = position.Y + 60;
                if (CollidesWithObject())
                    NewLocation();
                break;
            case 4:
                spawnLocation.X = position.X - 60;
                if (CollidesWithObject())
                    NewLocation();
                break;
            case 5:
                spawnLocation.X = position.X - 60;
                spawnLocation.Y = position.Y - 60;
                if (CollidesWithObject())
                    NewLocation();
                break;
            case 6:
                spawnLocation.Y = position.Y - 60;
                if (CollidesWithObject())
                    NewLocation();
                break;
            default:
                spawnLocation.X = position.X + 60;
                spawnLocation.Y = position.Y - 60;
                if (CollidesWithObject())
                    NewLocation();
                break;
        }
    }


    public bool CollidesWithObject()
    {

        //check wall collision
        Tile tile = tileField.Get(1, 1) as Tile;
        int Left = (int)(position.X / tile.Width);
        int Right = (int)((position.X + Width) / tile.Width);
        int Top = (int)(position.Y / tile.Height);
        int Bottom = (int)((position.Y + Height) / tile.Height);

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
                if (tileField.GetTileType(x, y) == TileType.Wall || tileField.GetTileType(x, y) == TileType.BreakableWall)
                    return true;
        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (SpriteGameObject player in players)
                if (player != this)
                    if (CollidesWith(player))
                        return true;
        //check enemycollision
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;
        foreach (SpriteGameObject enemy in enemies)
            if (CollidesWith(enemy))
                return true;

        return false;
    }




    //spawn certain enemies according to the spawnId given in Levelloading.cs (not created in levelloading yet)
    public virtual void Spawn()
    {
        if (spawnId == "Wizard")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Wizard(spawnLocation));
        }
        else if(spawnId == "Troll")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Troll(spawnLocation));
        }
        else if (spawnId == "Hellhound")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Hellhound(spawnLocation));
        }
        else if (spawnId == "Ghost")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Ghost(spawnLocation));
        }
    }

    public override void Reset()
    {
        base.Reset();
        timer = 0;
    }
}

