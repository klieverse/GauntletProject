using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

public class EnemyObject : AnimatedGameObject
{
    protected float speedVert;
    protected float speedHori;
    protected int health = 30;
    protected int strength;
    protected int speed = 250;
    TileField tileField;
    float meleeTimer = 1;
    protected Vector2 previousPosition;
    protected bool lastLookedLeft = false;

    public bool canBeMeleed = true;

    public EnemyObject(int layer, string id) : base(layer, id)
    {
        LoadAnimations();
        PlayAnimation("idle");
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
        
        tileField = GameWorld.Find("tiles") as TileField;
        //if (CollidesWithObject())
        //    position = previousPosition;
        GameObjectList players = GameWorld.Find("players") as GameObjectList;
        Player player;
        if (players.Children.Count != 0)
        {
            player = players.Children[0] as Player;
            if (player.IsAlive)
            {
                float opposite = (player.Position.Y + player.Height / 4) - position.Y;
                float adjacent = player.Position.X - position.X;
                float vertical = (float)Math.Atan2(opposite, adjacent);
                float horizontal = (float)Math.Atan2(adjacent, opposite);
                speedVert = (float)Math.Sin(vertical) * speed;
                speedHori = (float)Math.Sin(horizontal) * speed;
                velocity.Y = speedVert / 2;
                velocity.X = speedHori / 2;
            }
            
        }

        meleeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds; // makes sure a specific enemy can only be melee'd once a second;
        if (meleeTimer <= 0)
        {
            canBeMeleed = true;
        }
        HandleCollision();
        HandleAnimations();
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

    public bool CollidesWithObject()
    {
 
        //check wall collision
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

        return false;
    }

    void HandleCollision()
    {

        //check Tile collision
        TileField tiles = GameWorld.Find("tiles") as TileField;
        Tile tile = tiles.Get(1, 1) as Tile;
        int Left = (int)((position.X - Width / 2) / tile.Width);
        int Right = (int)((position.X + Width / 2) / tile.Width);
        int Top = (int)((position.Y - Height) / tile.Height);
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
                Rectangle boundingBox = this.BoundingBox;
                //boundingBox.Height-=1;
                //boundingBox.Width -=1;
                if (((currentTile != null && !currentTile.CollidesWith(this)) || currentTile == null) && !tileBounds.Intersects(boundingBox))
                {
                    continue;
                }
                Vector2 tileDepth = Collision.CalculateIntersectionDepth(boundingBox, tileBounds);

                if (Math.Abs(tileDepth.X) < Math.Abs(tileDepth.Y))
                {
                    if (tileType == TileType.Wall || tileType == TileType.BreakableWall || tileType == TileType.HorizontalDoor
                        || tileType == TileType.VerticalDoor || tileType == TileType.Teleporter)
                    {
                        if (tiles.GetTileType(x + 1, y) == TileType.Background)
                            position.X += tileDepth.X;
                        else position.X += tileDepth.X - 1;
                    }
                    continue;
                }

                if (tileType == TileType.Wall || tileType == TileType.BreakableWall || tileType == TileType.Teleporter
                    || tileType == TileType.HorizontalDoor || tileType == TileType.VerticalDoor)
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
                if(enemy != this)
                {
                    if (CollidesWith(enemy))
                    {
                        Rectangle enemieBox = enemy.BoundingBox;
                        Rectangle boundingBox = this.BoundingBox;
                        boundingBox.X += 1;
                        Vector2 enemyDepth = Collision.CalculateIntersectionDepth(boundingBox, enemieBox);

                        if (Math.Abs(enemyDepth.X) < Math.Abs(enemyDepth.Y))
                        {
                            if ((velocity.X > 0 && enemy.Velocity.X < 0) || (velocity.X < 0 && enemy.Velocity.X > 0))
                                position.X = previousPosition.X;

                            if ((velocity.X > 0 && enemy.Velocity.X >= 0 && position.X < enemy.Position.X) || (velocity.X <= 0 && enemy.Velocity.X < 0 && position.X > enemy.Position.X))
                                position.X = previousPosition.X;
                        }
                        else
                        {
                            if ((velocity.Y > 0 && enemy.Velocity.Y < 0) || (velocity.Y < 0 && enemy.Velocity.Y > 0))
                                position.Y = previousPosition.Y;

                            if ((velocity.Y > 0 && enemy.Velocity.Y > 0 && position.Y < enemy.Position.Y) || (velocity.Y < 0 && enemy.Velocity.Y < 0 && position.Y > enemy.Position.Y))
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
                        if ((velocity.X > 0 && player.Velocity.X < 0) || (velocity.X < 0 && player.Velocity.X > 0))
                            position.X = previousPosition.X;

                        if ((velocity.X > 0 && player.Velocity.X >= 0 && position.X < player.Position.X) || (velocity.X < 0 && player.Velocity.X <= 0 && previousPosition.X > player.Position.X))
                            position.X = previousPosition.X;
                    }
                    else 
                    {
                        if ((velocity.Y > 0 && player.Velocity.Y < 0) || (velocity.Y < 0 && player.Velocity.Y > 0))
                            position.Y = previousPosition.Y;

                        if ((velocity.Y > 0 && player.Velocity.Y >= 0 && position.Y < player.Position.Y) || (velocity.Y < 0 && player.Velocity.Y <= 0 && position.Y > player.Position.Y))
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

    public void HitByPlayer(float damage)
    {
        health -= (int)damage;
    }
    

}

