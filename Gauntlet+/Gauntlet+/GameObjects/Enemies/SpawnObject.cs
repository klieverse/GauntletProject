using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SpawnObject : Tile
{
    public int enemies = 0;
    Player closestPlayer; 
    int distance;
    bool isDead = false;
    public Vector2 spawnLocation;
    //Random r;
    float timer = 0, colorTimer = 200f;
    //   TileField tileField;
    readonly string spawnId = "";
    Level level;

    public SpawnObject(Vector2 startPosition, string spawnId, Level level) : base(spawnId, TileType.Temple, 2, "Spawn")
    {
        position = startPosition;
        //r = new Random();
        //        tileField = GameWorld.Find("Tiles") as TileField;
        this.spawnId = spawnId;
        this.level = level;
    }
    public override void Update(GameTime gameTime)
    {
        if (!isDead)
        {
            base.Update(gameTime);
            //cooldown timer for when an enemy is getting spawned

            CheckIfDead();

            PlayerCalculation();
            HandleTimers(gameTime);
        }
    }

    private void CheckIfDead()
    {
        if (Health <= 0)
        {
            visible = false;
            isDead = true;
            type = TileType.Background;
        }
    }

    private void HandleTimers(GameTime gameTime)
    {
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (distance < 1000 && timer > 7f && enemies < 8) // keeps a lock on the total allowed enemies to spawn
        {
            NewLocation();
            Spawn();
            timer = 0f;
            enemies += 1;
        }

        colorTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if(colorTimer <= 0)
        {
            color = Color.White;
            colorTimer = 200f;
        }
    }

    private void PlayerCalculation()
    {
        FindClosestPlayer();
        if (closestPlayer != null)
        {
            DistanceToClosestPlayer();
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
                break;
            case 1:
                spawnLocation.X = position.X + 30 + this.Width;
                spawnLocation.Y = position.Y + 30 + this.Height;
                break;
            case 2:
                spawnLocation.Y = position.Y + 30 + this.Height;
                break;
            case 3:
                spawnLocation.X = position.X - 30;
                spawnLocation.Y = position.Y + 30 + this.Height;
                break;
            case 4:
                spawnLocation.X = position.X - 30;
                break;
            case 5:
                spawnLocation.X = position.X - 30;
                spawnLocation.Y = position.Y - 30;
                break;
            case 6:
                spawnLocation.Y = position.Y - 30;

                break;
            default:
                spawnLocation.X = position.X + 30 + this.Width;
                spawnLocation.Y = position.Y - 30;
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

        return false;
    }




    //spawn certain enemies according to the spawnId given in Levelloading.cs (not created in levelloading yet)
    public virtual void Spawn()
    {
        if (spawnId == "Temple/Wizard")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Wizard(spawnLocation, this, true));
        }
        else if (spawnId == "Temple/Troll")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Troll(spawnLocation, this, true));
        }
        else if (spawnId == "Temple/Hellhound")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Hellhound(spawnLocation, this, true));
        }
        else if (spawnId == "Temple/Skeleton")
        {
            (GameWorld.Find("enemies") as GameObjectList).Add(new Ghost(spawnLocation, this, true));
        }
    }

    public void HitByPlayer(float damage)
    {
        Health -= (int)damage;
        color = Color.IndianRed;
        colorTimer = 200f;
    }

    public override void Reset()
    {
        base.Reset();
        timer = 0;
        Health = 50;
        isDead = false;
    }

    public int Health { get; private set; } = 50;
}

