using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

class EnemyObject : AnimatedGameObject
{
    [JsonIgnore]
    Player closestPlayer;

    [JsonIgnore]
    public Level level
    {
        get;
        set;
    }

    protected float speedVert, speedHori;
    protected int health = 30, strength, speed = 175, chaseDistance;
    //TileField tileField;
    protected float meleeTimer = 1f, colorTimer = 200f, maxDistance = 99999999999f, distance, despawnTimer = 10f;
    protected Vector2 previousPosition;
    protected bool lastLookedLeft = false, isDead = false, canBeInvisible, noInvisible, wasSpawned, beginCollision = false/*, collisionAtSpawn*/;

    public bool canBeMeleed = true;

    public EnemyObject(int layer, string id, Level level, int chaseDistance = 0, bool canBeInvisible = false/*, bool spawnCollision = false*/, bool sent = false) : base(layer, id)
    {
        this.canBeInvisible = canBeInvisible;
        this.chaseDistance = chaseDistance;
        this.level = level;
        //collisionAtSpawn = spawnCollision;
        //if(sprite != null)
        {
            LoadAnimations();
            PlayAnimation("idle");
        }
        
        Sent = sent;
    }
    void LoadAnimations()
    {
        LoadAnimation("Sprites/Enemies/spr_" + id + "idle@4", "idle", true);
        LoadAnimation("Sprites/Enemies/spr_" + id + "run@4", "run", true);
    }
    public override void Update(GameTime gameTime)
    {
        previousPosition = position;
        base.Update(gameTime);
        
        /*if (collisionAtSpawn)
        {
            if (CollidesWithObject())
            {
                visible = false;
            }
        }*/


        //tileField = GameWorld.Find("tiles") as TileField;
        /*if (CollidesWithObject())
            position = previousPosition;*/

        FindClosestPlayer();
        MoveToCLosestPlayer();
        HandleCollision();


        /*if (CollidesWithObject())
            position = previousPosition;
        GameObjectList players = GameWorld.Find("players") as GameObjectList;
        Player player;
        if (players.Children.Count != 0)
        {
            player = players.Children[0] as Player;
            if (player.IsAlive)
            {*/

        //    }

        //}

        HandleTimers(gameTime);
    }

    private void HandleTimers(GameTime gameTime)
    {
        meleeTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; // makes sure a specific enemy can only be melee'd once a second;
        
        if (meleeTimer >= 1000)
        {
            meleeTimer = 0;
            canBeMeleed = true;
        }

        colorTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds; // sets the color back to normal after timer hits 0;

        if(colorTimer <= 0)
        {
            color = Color.White;
            colorTimer = 200f;
        }

        if (!visible)
        {
            despawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(despawnTimer <= 0)
            {
                level.Delete.Add(this);
            }
        }

        HandleAnimations();
    }

    private void MoveToCLosestPlayer()
    {
        if (closestPlayer != null)
        {
            float opposite = (closestPlayer.Position.Y + closestPlayer.Height / 4) - position.Y;
            float adjacent = closestPlayer.Position.X - position.X;
            distance = (float)Math.Sqrt((opposite * opposite) + (adjacent * adjacent));
            float vertical = (float)Math.Atan2(opposite, adjacent);
            float horizontal = (float)Math.Atan2(adjacent, opposite);
            speedVert = ((float)Math.Sin(vertical) * speed) / 2;
            speedHori = ((float)Math.Sin(horizontal) * speed) / 2;
            if (distance > chaseDistance)
            {
                velocity.Y = speedVert;
                velocity.X = speedHori;
            }
            else
            {
                velocity.Y = 0;
                velocity.X = 0;
            }
            if (canBeInvisible && distance < 100)
            {
                visible = true;
                noInvisible = true;
            }
            else
            {
                noInvisible = false;
            }
        }
    }

    void HandleAnimations()
    {
        if (velocity == Vector2.Zero)
        {
            PlayAnimation("idle");
        }
        else PlayAnimation("run");
        


        if (velocity.X < 0)
        {
            lastLookedLeft = true;
        }
        else if (velocity.X > 0)
        {
            lastLookedLeft = false;
        }

        if (velocity.X < 0 || lastLookedLeft)
        {
            Mirror = true;
        }
        else Mirror = false;
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

    void HandleCollision()
    {
        //check Tile collision
        TileField tiles = GameWorld.Find("tiles") as TileField;
        Tile tile = tiles.Get(1, 1) as Tile;
        int Left = (int)((position.X - Width / 2) / tile.Width);
        int Right = (int)((position.X + Width / 2) / tile.Width);
        int Top = (int)((position.Y - Height / 2) / tile.Height);
        int Bottom = (int)((position.Y) / tile.Height);

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
            {
                TileType tileType = tiles.GetTileType(x, y);
                if (tileType == TileType.Background)
                {
                    continue;
                }
                Tile currentTile = tiles.Get(x, y) as Tile;
                Rectangle tileBounds = new Rectangle(x * tiles.CellWidth, y * tiles.CellHeight,
                                                        tiles.CellWidth, tiles.CellHeight);
                Rectangle boundingBox = new Rectangle((int)position.X - Width / 2, (int)position.Y - Height / 2, Width, Height / 2);

                if (((currentTile != null && !currentTile.CollidesWith(this)) || currentTile == null) && !tileBounds.Intersects(boundingBox))
                {
                    continue;
                }
                Vector2 tileDepth = Collision.CalculateIntersectionDepth(boundingBox, tileBounds);

                if (Math.Abs(tileDepth.X) < Math.Abs(tileDepth.Y))
                {
                    if (tileType == TileType.Wall || tileType == TileType.BreakableWall || tileType == TileType.HorizontalDoor
                        || tileType == TileType.VerticalDoor || tileType == TileType.Teleporter || tileType == TileType.Temple)
                    {
                        if (tiles.GetTileType(x + 1, y) == TileType.Background)
                            position.X += tileDepth.X;
                        else position.X += tileDepth.X - 1;
                    }
                    continue;
                }

                if (tileType == TileType.Wall || tileType == TileType.BreakableWall || tileType == TileType.Teleporter
                    || tileType == TileType.HorizontalDoor || tileType == TileType.VerticalDoor || tileType == TileType.Temple)
                {
                    if (tiles.GetTileType(x + 1, y) == TileType.Background)
                        position.Y += tileDepth.Y - 1;
                    else position.Y += tileDepth.Y;
                }
            }

        // checks collision w/ enemies
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;

        if (enemies != null)
        {
            foreach (EnemyObject enemy in enemies)
            {
                if (enemy != this)
                {
                    if (CollidesWith(enemy))
                    {
                        Rectangle enemieBox = enemy.BoundingBox;
                        Rectangle boundingBox = this.BoundingBox;
                        boundingBox.X += 1;
                        Vector2 enemyDepth = Collision.CalculateIntersectionDepth(boundingBox, enemieBox);

                        if (Math.Abs(enemyDepth.X) < Math.Abs(enemyDepth.Y))
                        {
                            if ((velocity.X > 0 && enemy.Velocity.X < 0) || (velocity.X < 0 && enemy.Velocity.X > 0) || (velocity.X > 0 && enemy.Velocity.X >= 0 && position.X < enemy.Position.X)
                                || (velocity.X <= 0 && enemy.Velocity.X < 0 && position.X > enemy.Position.X))
                                position.X = previousPosition.X;
                        }
                        else
                        {
                            if ((velocity.Y > 0 && enemy.Velocity.Y < 0) || (velocity.Y < 0 && enemy.Velocity.Y > 0) || (velocity.Y > 0 && enemy.Velocity.Y > 0 && position.Y < enemy.Position.Y)
                                || (velocity.Y < 0 && enemy.Velocity.Y < 0 && position.Y > enemy.Position.Y))
                                position.Y = previousPosition.Y;
                        }
                    }
                }

            }
        }

        // checks collision w/ players
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;

        if (players != null)
        {
            foreach (Player player in players)
            {
                if (CollidesWith(player))
                {
                    Rectangle playerBox = player.BoundingBox;
                    Rectangle boundingBox = this.BoundingBox;
                    boundingBox.X += 1;
                    Vector2 playerDepth = Collision.CalculateIntersectionDepth(boundingBox, playerBox);

                    if (Math.Abs(playerDepth.X) < Math.Abs(playerDepth.Y))
                    {
                        if ((velocity.X > 0 && player.Velocity.X < 0) || (velocity.X < 0 && player.Velocity.X > 0) || (velocity.X > 0 && player.Velocity.X >= 0 && position.X < player.Position.X)
                            || (velocity.X < 0 && player.Velocity.X <= 0 && previousPosition.X > player.Position.X))
                            position.X = previousPosition.X;
                    }
                    else
                    {
                        if ((velocity.Y > 0 && player.Velocity.Y < 0) || (velocity.Y < 0 && player.Velocity.Y > 0) || (velocity.Y > 0 && player.Velocity.Y >= 0 && position.Y < player.Position.Y)
                            || (velocity.Y < 0 && player.Velocity.Y <= 0 && position.Y > player.Position.Y))
                            position.Y = previousPosition.Y;
                    }
                }
            }
        }

        // checks collision w/ treasures
        List<GameObject> treasures = (GameWorld.Find("treasures") as GameObjectList).Children;

        if (players != null)
        {
            foreach (Treasure treasure in treasures)
            {
                if (CollidesWith(treasure))
                {
                    Rectangle playerBox = treasure.BoundingBox;
                    Rectangle boundingBox = this.BoundingBox;
                    boundingBox.X += 1;
                    Vector2 playerDepth = Collision.CalculateIntersectionDepth(boundingBox, playerBox);

                    if (Math.Abs(playerDepth.X) < Math.Abs(playerDepth.Y))
                    {
                        if ((velocity.X > 0 && treasure.Velocity.X < 0) || (velocity.X < 0 && treasure.Velocity.X > 0) || (velocity.X > 0 && treasure.Velocity.X >= 0 && position.X < treasure.Position.X)
                            || (velocity.X < 0 && treasure.Velocity.X <= 0 && previousPosition.X > treasure.Position.X))
                            position.X = previousPosition.X;
                    }
                    else
                    {
                        if ((velocity.Y > 0 && treasure.Velocity.Y < 0) || (velocity.Y < 0 && treasure.Velocity.Y > 0) || (velocity.Y > 0 && treasure.Velocity.Y >= 0 && position.Y < treasure.Position.Y)
                            || (velocity.Y < 0 && treasure.Velocity.Y <= 0 && position.Y > treasure.Position.Y))
                            position.Y = previousPosition.Y;
                    }
                }
            }
        }



        if (velocity.X >= 0 && velocity.Y >= 0)
            position = new Vector2((float)Math.Ceiling(position.X), (float)Math.Ceiling(position.Y));
        if (velocity.X >= 0 && velocity.Y <= 0)
            position = new Vector2((float)Math.Ceiling(position.X), (float)Math.Floor(position.Y));
        if (velocity.X <= 0 && velocity.Y >= 0)
            position = new Vector2((float)Math.Floor(position.X), (float)Math.Ceiling(position.Y));
        if (velocity.X <= 0 && velocity.Y <= 0)
            position = new Vector2((float)Math.Floor(position.X), (float)Math.Floor(position.Y));

    }

    public bool CollidesWithObject()
    {

        //check wall collision
        TileField tileField = GameWorld.Find("tiles") as TileField;
        Tile tile = tileField.Get(1, 1) as Tile;
        int Left = (int)((position.X -this.Width /2)/ tile.Width);
        int Right = (int)((position.X + this.Width /2) / tile.Width);
        int Top = (int)((position.Y-Height) / tile.Height);
        int Bottom = (int)((position.Y ) / tile.Height);

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
                if (tileField.GetTileType(x, y) == TileType.Wall || tileField.GetTileType(x, y) == TileType.BreakableWall 
                    || tileField.GetTileType(x, y) == TileType.HorizontalDoor || tileField.GetTileType(x, y) == TileType.VerticalDoor)
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
            if (enemy !=this)
                if (CollidesWith(enemy))
                    return true;

        List<GameObject> spawns = (GameWorld.Find("spawns") as GameObjectList).Children;
        if (spawns != null)
        {
            foreach (SpriteGameObject spawn in spawns)
                if (CollidesWith(spawn))
                    return true;
        }

            return false;
    }

    public virtual void HitByPlayer(float damage)
    {
        health -= (int)damage;
        color = Color.Red;
        colorTimer = 200f;
    }
    
    public bool Sent
    {
        get;
        set;
    }

    public int Health
    {
        get { return health; }
    }
}

