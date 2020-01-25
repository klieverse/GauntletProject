using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SpawnObject : Tile
{
    Player closestPlayer; 
    int distance, health = 50;
    bool isDead = false;
    public Vector2 spawnLocation;
    //Random r;
    float timer = 0;
    //   TileField tileField;
    readonly string spawnId = "";

    public SpawnObject(Vector2 startPosition, string spawnId) : base(spawnId, TileType.Temple, 2, "Spawn")
    {
        position = startPosition;
        //r = new Random();
        //        tileField = GameWorld.Find("Tiles") as TileField;
        this.spawnId = spawnId;
    }
    public override void Update(GameTime gameTime)
    {
        if (!isDead)
        {
            base.Update(gameTime);
            //cooldown timer for when an enemy is getting spawned

            if (health <= 0)
            {
                visible = false;
                isDead = true;
                type = TileType.Background;
            }

            FindClosestPlayer();
            if (closestPlayer != null)
            {
                DistanceToClosestPlayer();
            }


            if (distance < 1000)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer > 2000)
                {
                    NewLocation();
                    Spawn();
                    timer = 0;
                }
            }
        }
        
        

    }

    private void DistanceToClosestPlayer()
    {
        float opposite = (closestPlayer.Position.Y + closestPlayer.Height / 4) - position.Y;
        float adjacent = closestPlayer.Position.X - position.X;
        distance = (int)Math.Sqrt((opposite * opposite) + (adjacent * adjacent));
    }

    private void FindClosestPlayer()
    {
        //check for all the portals in the level
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        foreach (Player player in players)
        {
            //determines the closest portal to this portal, other than this portal itself           
            float opposite = player.Position.Y - position.Y;
            float adjacent = player.Position.X - position.X;
            float hypotenuse = (float)Math.Sqrt(Math.Pow(opposite, 2) + Math.Pow(adjacent, 2));
            if (closestPlayer != null)
            {
                float opposite2 = closestPlayer.Position.Y - position.Y;
                float adjacent2 = closestPlayer.Position.X - position.X;
                float hypotenuse2 = (float)Math.Sqrt(Math.Pow(opposite2, 2) + Math.Pow(adjacent2, 2));
                if (hypotenuse < hypotenuse2)
                {
                    //maxDistance = hypotenuse;
                    this.closestPlayer = player;
                }
            }
            else
            {
                this.closestPlayer = player;
            }

        }
    }

    

    private void NewLocation()
    {
        //gives a random number from 0-7, where each number has determines the location of spawn from the Spawner
        int spawnPlace = GameEnvironment.Random.Next(0,7);
        switch (spawnPlace)
        {
            case 0:
                spawnLocation.X = position.X + 30 + this.Width;
                //if (CollidesWithObject())
                //{
                    //spawnLocation.X = position.X;
                    //NewLocation();
                //}

                break;
            case 1:
                spawnLocation.X = position.X + 30 + this.Width;
                spawnLocation.Y = position.Y + 30 + this.Height;
                //if (CollidesWithObject())
                //{
                    //spawnLocation.X = position.X;
                    //spawnLocation.Y = position.Y;
                    //NewLocation();
                //}

                break;
            case 2:
                spawnLocation.Y = position.Y + 30 + this.Height;
                /*if (CollidesWithObject())
                {
                    spawnLocation.Y = position.Y;
                    //NewLocation();
                }*/

                break;
            case 3:
                spawnLocation.X = position.X - 30;
                spawnLocation.Y = position.Y + 30 + this.Height;
                /*if (CollidesWithObject())
                {
                    spawnLocation.X = position.X;
                    spawnLocation.Y = position.Y;
                    //NewLocation();
                }*/

                break;
            case 4:
                spawnLocation.X = position.X - 30;
                /*if (CollidesWithObject())
                {
                    spawnLocation.X = position.X;
                    //NewLocation();
                }*/

                break;
            case 5:
                spawnLocation.X = position.X - 30;
                spawnLocation.Y = position.Y - 30;
                /*if (CollidesWithObject())
                {
                    spawnLocation.X = position.X;
                    spawnLocation.Y = position.Y;
                    //NewLocation();
                }*/

                break;
            case 6:
                spawnLocation.Y = position.Y - 30;
                /*if (CollidesWithObject())
                {
                    spawnLocation.Y = position.Y;
                    //NewLocation();
                }*/

                break;
            default:
                spawnLocation.X = position.X + 30 + this.Width;
                spawnLocation.Y = position.Y - 30;
                /*if (CollidesWithObject())
                {
                    spawnLocation.X = position.X;
                    spawnLocation.Y = position.Y;
                    //NewLocation();
                }*/

                break;
        }
    }


    public bool CollidesWithObject()
    {

        //check wall collision
        TileField tileField = GameWorld.Find("tiles") as TileField;
        Tile tile = tileField.Get(1, 1) as Tile;
        int Left = (int)(spawnLocation.X / tile.Width);
        int Right = (int)((spawnLocation.X + Width) / tile.Width);
        int Top = (int)(spawnLocation.Y / tile.Height);
        int Bottom = (int)((spawnLocation.Y + Height) / tile.Height);

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

        /*List<GameObject> spawns = (GameWorld.Find("spawns") as GameObjectList).Children;
        if (spawns != null)
        {
            foreach (SpriteGameObject spawn in spawns)
                if (CollidesWith(spawn))
                    return true;
        } */


        return false;
    }




    //spawn certain enemies according to the spawnId given in Levelloading.cs (not created in levelloading yet)
    public virtual void Spawn()
    {
        if (spawnId == "Temple/Wizard")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Wizard(spawnLocation, true));
        }
        else if (spawnId == "Temple/Troll")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Troll(spawnLocation, true));
        }
        else if (spawnId == "Temple/Hellhound")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Hellhound(spawnLocation, true));
        }
        else if (spawnId == "Temple/Skeleton")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Ghost(spawnLocation, true));
        }
    }

    public void HitByPlayer(float damage)
    {
        health -= (int)damage;
    }

    public override void Reset()
    {
        base.Reset();
        timer = 0;
        health = 50;
        isDead = false;
    }
}

