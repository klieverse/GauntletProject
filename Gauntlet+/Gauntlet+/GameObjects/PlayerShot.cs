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


    public PlayerShot(string id, float shotSpeed, float shotStrength, Vector2 direction, Vector2 position) : base(assetName: id + "Shot", layer: 0, id, sheetIndex: 0)
    {
        velocity = direction;
        this.position = position;
        this.shotSpeed = shotSpeed;
        this.shotStrength = shotStrength;
        HandleDirection();
    }

    void HandleDirection() // rotates the sprite to the right direction, based on the direction it is going
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
            velocity.Y = -(shotSpeed *75 + baseShotSpeed);
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

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        HandleCollisions();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }

    void HandleCollisions()
    {
        TileField tileField = GameWorld.Find("tiles") as TileField;

        //check wall collision
        Tile tile = tileField.Get(1, 1) as Tile;
        int Left = (int)(position.X / tile.Width);
        int Right = (int)((position.X + Width) / tile.Width);
        int Top = (int)(position.Y / tile.Height);
        int Bottom = (int)((position.Y + Height) / tile.Height);

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
                if (tileField.GetTileType(x, y) == TileType.Wall)
                {
                    visible = false;
                }

        //check breakable wall collision

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
                if (tileField.GetTileType(x, y) == TileType.BreakableWall)
                {
                    visible = false;
                    //iets met muur;
                }

        //check enemycollision
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;
        foreach (EnemyObject enemy in enemies)
            if (CollidesWith(enemy))
            {
                visible = false;
                enemy.(shotStrength);
            }

    }
}

