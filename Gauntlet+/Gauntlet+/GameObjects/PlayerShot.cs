using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayerShot : SpriteGameObject
{
    float shotSpeed, baseShotSpeed = 250;
    float shotStrength;
    public Player player
    {
        get;
        set;
    }
    public int shotCount
    {
        get;
        set;
    }


    public PlayerShot(string id, int shotCount, float shotSpeed, float shotStrength, Vector2 direction, Vector2 position, Player player, InputHelper inputHelper) : base(assetName: id + "Shot", layer: 0, id, sheetIndex: 0)
    {
        velocity = direction;
        this.position.X = position.X;
        this.shotCount = shotCount;
        if(player != null)
        {
            this.position.Y = position.Y - player.Height / 3;
            this.player = player;
        }
        else
        {
            this.player = null;
            this.position.Y = position.Y + 18;
        }
        
        this.shotSpeed = shotSpeed;
        this.shotStrength = shotStrength;

        if (inputHelper != null)
        {
            HandleDirection(inputHelper);
        }
        
        if(sprite != null)
        {
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }
        else
        {
            origin = Vector2.Zero;
        }
        
    }

    void HandleDirection(InputHelper inputHelper) // rotates the sprite to the right direction, based on the direction it is going and giving it the right position and velocity;
    {
        if (!inputHelper.ControllerConnected())
        {
            if (velocity.X > 0 && velocity.Y == 0) // facing right
            {
                velocity.X = shotSpeed * 75 + baseShotSpeed;
                Rotate(90);
            }
            if (velocity.X > 0 && velocity.Y > 0) // facing down right
            {
                velocity.X = 0.71f * (shotSpeed * 75 + baseShotSpeed);
                velocity.Y = 0.71f * (shotSpeed * 75 + baseShotSpeed);
                Rotate(135);
            }
            if (velocity.X < 0 && velocity.Y < 0) // facing up left
            {
                velocity.X = -0.71f * (shotSpeed * 75 + baseShotSpeed);
                velocity.Y = -0.71f * (shotSpeed * 75 + baseShotSpeed);
                Rotate(315);
            }
            if (velocity.X == 0 && velocity.Y > 0) // facing down
            {
                velocity.Y = (shotSpeed * 75 + baseShotSpeed);
                Rotate(180);
            }
            if (velocity.X < 0 && velocity.Y == 0) // facing left
            {
                velocity.X = -(shotSpeed * 75 + baseShotSpeed);
                Rotate(270);
            }
            if (velocity.X == 0 && velocity.Y < 0) // facing up
            {
                velocity.Y = -(shotSpeed * 75 + baseShotSpeed);
                Rotate(0);
            }
            if (velocity.X > 0 && velocity.Y < 0) // facing up right
            {
                velocity.X = 0.71f * (shotSpeed * 75 + baseShotSpeed);
                velocity.Y = -0.71f * (shotSpeed * 75 + baseShotSpeed);
                Rotate(45);
            }
            if (velocity.X < 0 && velocity.Y > 0) // facing down left
            {
                velocity.X = -0.71f * (shotSpeed * 75 + baseShotSpeed);
                velocity.Y = 0.71f * (shotSpeed * 75 + baseShotSpeed);
                Rotate(225);
            }
        }
        else // this means a controller is connected
        {
            int degrees = 0;
            if (velocity.X >= 0 && velocity.Y < 0)
                degrees = (int)MathHelper.ToDegrees((float)Math.Atan(velocity.X / Math.Abs(velocity.Y)));// calculates the rotation using the velocity vector
            else if (velocity.X > 0 && velocity.Y >= 0)
                degrees = (int)MathHelper.ToDegrees((float)Math.Atan(velocity.Y/ velocity.X)) + 90;
            else if (velocity.X <= 0 && velocity.Y > 0)
                degrees = (int)MathHelper.ToDegrees((float)Math.Atan(Math.Abs(velocity.X) / velocity.Y)) + 180;
            else if (velocity.X < 0 && velocity.Y <= 0)
                degrees = (int)MathHelper.ToDegrees((float)(Math.Atan(velocity.Y / velocity.X))) + 270;


            Rotate(degrees);

            float vectorLength = (float)Math.Sqrt(velocity.Y * velocity.Y + velocity.X * velocity.X);
            float multiplier = 1 / vectorLength;

            velocity.X = velocity.X * multiplier * (shotSpeed * 75 + baseShotSpeed);
            velocity.Y = velocity.Y * multiplier * (shotSpeed * 75 + baseShotSpeed);
        }

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        HandleCollisions();
        HandleOutOfScreen();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }

    void HandleCollisions()
    {
        TileField tileField = GameWorld.Find("tiles") as TileField;
        //check wall/door collision
        Tile tile = tileField.Get(1, 1) as Tile;
        int Left = (int)(position.X / tile.Width);
        int Right = (int)((position.X + Width) / tile.Width);
        int Top = (int)(position.Y / tile.Height);
        int Bottom = (int)((position.Y + (Height/3)) / tile.Height);

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
            {
                TileType type = tileField.GetTileType(x, y);
                if ( type == TileType.Wall || type == TileType.VerticalDoor || type == TileType.HorizontalDoor)
                {
                    if (visible)
                        GameEnvironment.AssetManager.PlaySound("Ghost hit",position.X);
                    visible = false;
                }
            }

        //checks BreakableWalls collision
        List<GameObject> bWalls = (GameWorld.Find("BreakableWalls") as GameObjectList).Children;
        foreach (BreakableWall bWall in bWalls)
            if (CollidesWith(bWall))
            {
                visible = false;
                bWall.HitByShot();
                GameEnvironment.AssetManager.PlaySound("Ghost hit", position.X);
            }

        //check enemycollision
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;
        
        foreach (EnemyObject enemy in enemies)
            if (CollidesWith(enemy))
            {
                visible = false;
                enemy.HitByPlayer(shotStrength);
                if (enemy.Health < 1)
                    player.ScoreUp(50);
                GameEnvironment.AssetManager.PlaySound("Ghost hit", position.X);
            }
            
        //check spawnercollision
        List<GameObject> spawns = (GameWorld.Find("spawns") as GameObjectList).Children;
        foreach (SpawnObject spawn in spawns)
            if (CollidesWith(spawn))
            {
                visible = false;
                spawn.HitByPlayer(shotStrength);
                if (spawn.Health < 1)
                {
                    player.ScoreUp(100);
                }
            }

        //check food collision
        List<GameObject> foods = (GameWorld.Find("food") as GameObjectList).Children;
        foreach (Food food in foods)
            if (CollidesWith(food))
            {
                visible = false;
                food.Visible = false;
                GameEnvironment.AssetManager.PlaySound("Ghost hit", position.X);
            }
        //check potion collision
        List<GameObject> potions = (GameWorld.Find("potions") as GameObjectList).Children;
        foreach (Potion potion in potions)
            if (CollidesWith(potion))
            {
                visible = false;
                GameEnvironment.AssetManager.PlaySound("Ghost hit", position.X);
                if (potion.PotType == PotionType.Normal)
                {
                    potion.Visible = false;
                    player.KillEnemiesOnScreen(false);
                    GameEnvironment.AssetManager.PlaySound("Ghost hit", position.X);
                    GameEnvironment.AssetManager.PlaySound("Explosion", position.X);
                } 
            }
    }

    public void HandleOutOfScreen()
    {
        if(!Camera.CameraBox.Contains(position))
        {
            visible = false;
        }
    }

    public void HandleIncoming()
    {
        this.position.Y = position.Y - player.Height / 3;
        origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        if (velocity.X > 0 && velocity.Y == 0) // facing right
        {
            velocity.X = shotSpeed * 75 + baseShotSpeed;
            Rotate(90);
        }
        if (velocity.X > 0 && velocity.Y > 0) // facing down right
        {
            velocity.X = 0.71f * (shotSpeed * 75 + baseShotSpeed);
            velocity.Y = 0.71f * (shotSpeed * 75 + baseShotSpeed);
            Rotate(135);
        }
        if (velocity.X < 0 && velocity.Y < 0) // facing up left
        {
            velocity.X = -0.71f * (shotSpeed * 75 + baseShotSpeed);
            velocity.Y = -0.71f * (shotSpeed * 75 + baseShotSpeed);
            Rotate(315);
        }
        if (velocity.X == 0 && velocity.Y > 0) // facing down
        {
            velocity.Y = (shotSpeed * 75 + baseShotSpeed);
            Rotate(180);
        }
        if (velocity.X < 0 && velocity.Y == 0) // facing left
        {
            velocity.X = -(shotSpeed * 75 + baseShotSpeed);
            Rotate(270);
        }
        if (velocity.X == 0 && velocity.Y < 0) // facing up
        {
            velocity.Y = -(shotSpeed * 75 + baseShotSpeed);
            Rotate(0);
        }
        if (velocity.X > 0 && velocity.Y < 0) // facing up right
        {
            velocity.X = 0.71f * (shotSpeed * 75 + baseShotSpeed);
            velocity.Y = -0.71f * (shotSpeed * 75 + baseShotSpeed);
            Rotate(45);
        }
        if (velocity.X < 0 && velocity.Y > 0) // facing down left
        {
            velocity.X = -0.71f * (shotSpeed * 75 + baseShotSpeed);
            velocity.Y = 0.71f * (shotSpeed * 75 + baseShotSpeed);
            Rotate(225);
        }
    }

    public bool IsShot
    {
        get { return true; }

    }
}

