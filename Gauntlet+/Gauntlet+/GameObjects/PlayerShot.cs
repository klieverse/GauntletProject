using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauntlet_.GameObjects
{
    class PlayerShot : SpriteGameObject
    {
        float shotSpeed;
        float shotStrength;
        Vector2 direction;
        protected Level level;
        protected TileField tileField;


        public PlayerShot(string id, float shotSpeed, float shotStrength, Vector2 direction) : base(assetName: "", layer: 0, id, sheetIndex: 0)
        {

            HandleDirection();
            this.direction = direction;
            this.shotSpeed = shotSpeed;
            this.shotStrength = shotStrength;
        }

        void HandleDirection()
        {
            if (direction.X > 0 && direction.Y == 0)
            {
                direction.X = shotSpeed * 50 + 200;
                Rotate(90);
            }
            if (direction.X > 0 && direction.Y > 0)
            {
                direction.X = 0.71f * (shotSpeed * 50 + 200);
                direction.Y = 0.71f * (shotSpeed * 50 + 200);
                Rotate(135);
            }
            if (direction.X < 0 && direction.Y < 0)
            {
                direction.X = -0.71f * (shotSpeed * 50 + 200);
                direction.Y = -0.71f * (shotSpeed * 50 + 200);
                Rotate(315);
            }
            if (direction.X == 0 && direction.Y > 0)
            {
                direction.Y = (shotSpeed * 50 + 200);
                Rotate(180);
            }
            if (direction.X < 0 && direction.Y == 0)
            {
                direction.X = -(shotSpeed * 50 + 200);
                Rotate(270);
            }
            if (direction.X == 0 && direction.Y < 0)
            {
                direction.Y = -(shotSpeed * 50 + 200);
                Rotate(0);
            }
            if (direction.X > 0 && direction.Y < 0)
            {
                direction.X = 0.71f * (shotSpeed * 50 + 200);
                direction.Y = -0.71f * (shotSpeed * 50 + 200);
                Rotate(45);
            }
            if (direction.X < 0 && direction.Y > 0)
            {
                direction.X = -0.71f * (shotSpeed * 50 + 200);
                direction.Y = 0.71f * (shotSpeed * 50 + 200);
                Rotate(225);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            HandleCollisions();
        }

        void HandleCollisions()
        {
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
            foreach (SpriteGameObject enemy in enemies)
                if (CollidesWith(enemy))
                {
                    visible = false;
                    //enemy.HitByPlayer(shotStrength);
                }
                    
        }
    }
}
