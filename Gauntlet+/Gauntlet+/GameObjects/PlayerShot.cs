using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayerShot : SpriteGameObject
{
    float shotSpeed;
    float shotStrength;
    int WallCounter = 3;


    public PlayerShot(string id, float shotSpeed, float shotStrength, Vector2 direction, Vector2 position) : base(assetName: "arrow", layer: 0, id, sheetIndex: 0)
    {
        velocity = direction;
        this.position = position;
        this.shotSpeed = shotSpeed;
        this.shotStrength = shotStrength;
        HandleDirection();
    }

    void HandleDirection()
    {
        if (velocity.X > 0 && velocity.Y == 0)
        {
            velocity.X = shotSpeed * 75 + 200;
            Rotate(90);
        }
        if (velocity.X > 0 && velocity.Y > 0)
        {
            velocity.X = 0.71f * (shotSpeed * 75 + 200);
            velocity.Y = 0.71f * (shotSpeed * 75 + 200);
            Rotate(135);
        }
        if (velocity.X < 0 && velocity.Y < 0)
        {
            velocity.X = -0.71f * (shotSpeed * 75 + 200);
            velocity.Y = -0.71f * (shotSpeed * 75 + 200);
            Rotate(315);
        }
        if (velocity.X == 0 && velocity.Y > 0)
        {
            velocity.Y = (shotSpeed * 75 + 200);
            Rotate(180);
        }
        if (velocity.X < 0 && velocity.Y == 0)
        {
            velocity.X = -(shotSpeed * 75 + 200);
            Rotate(270);
        }
        if (velocity.X == 0 && velocity.Y < 0)
        {
            velocity.Y = -(shotSpeed * 75 + 200);
            Rotate(0);
        }
        if (velocity.X > 0 && velocity.Y < 0)
        {
            velocity.X = 0.71f * (shotSpeed * 75 + 200);
            velocity.Y = -0.71f * (shotSpeed * 75 + 200);
            Rotate(45);
        }
        if (velocity.X < 0 && velocity.Y > 0)
        {
            velocity.X = -0.71f * (shotSpeed * 75 + 200);
            velocity.Y = 0.71f * (shotSpeed * 75 + 200);
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
                    WallCounter -= 1;
                    WallBreaker();

                }

        //check enemycollision
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;
        foreach (EnemyObject enemy in enemies)
            if (CollidesWith(enemy))
            {
                visible = false;
                enemy.HitByPlayer(shotStrength);
            }



    }
    public void WallBreaker()
        {
        if (WallCounter <= 0)
        {
            //Lukt me nog niet om dit werkende te krijgen
            //TileType.BreakableWall != visible;

            return;
        }
        return;

        }
        
}

