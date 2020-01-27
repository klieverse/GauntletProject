using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

public class EnemyObject : AnimatedGameObject
{
    [JsonIgnore]
    Player closestPlayer;

    protected float speedVert, speedHori;
    protected int health = 30, strength, speed = 250, chaseDistance;
    //TileField tileField;
    protected float meleeTimer = 1f, maxDistance = 99999999999f, distance;
    protected Vector2 previousPosition;
    protected bool lastLookedLeft = false, isDead = false, canBeInvisible, noInvisible, wasSpawned, beginCollision = false/*, collisionAtSpawn*/;

    public bool canBeMeleed = true;

    public EnemyObject(int layer, string id, int chaseDistance = 0, bool canBeInvisible = false/*, bool spawnCollision = false*/, bool sent = false) : base(layer, id)
    {
        this.canBeInvisible = canBeInvisible;
        this.chaseDistance = chaseDistance;
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
        if (CollidesWithObject())
            position = previousPosition;

        FindClosestPlayer();
        MoveToCLosestPlayer();


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

        TimeToAttack(gameTime);
    }

    private void TimeToAttack(GameTime gameTime)
    {
        /*meleeTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; // makes sure a specific enemy can only be melee'd once a second;
        if (meleeTimer >= 1000)
        {
            meleeTimer = 0;
            canBeMeleed = true;
        } */
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

    public void HitByPlayer(float damage)
    {
        health -= (int)damage;
    }
    
    public bool Sent
    {
        get;
        set;
    }

}

