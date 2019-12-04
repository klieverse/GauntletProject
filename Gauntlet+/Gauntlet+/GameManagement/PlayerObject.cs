using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauntlet_.GameObjects
{
    class PlayerObject : AnimatedGameObject
    {
        protected Vector2 startPosition;
        protected Level level;
        protected float previousYPosition;
        protected bool isAlive;
        protected float walkingSpeed;
        protected int health = 600;
        protected float armor;
        protected float magic;
        protected float strength;
        protected float shotSpeed;

        public PlayerObject(int layer, string id, Vector2 start, Level level, float speed, float armor, float magic, float strength, float shotSpeed)
        : base(layer, id)
        {
            this.level = level;
            this.walkingSpeed = speed;
            startPosition = start;
        }

        public override void Reset()
        {
            position = startPosition;
            velocity = Vector2.Zero;
            isAlive = true;
            PlayAnimation("idle");
            previousYPosition = BoundingBox.Bottom;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (!isAlive)
            {
                return;
            }
            if (inputHelper.IsKeyDown(Keys.Left))
            {
                velocity.X = -walkingSpeed;
            }
            if (inputHelper.IsKeyDown(Keys.Right))
            {
                velocity.X = walkingSpeed;
            }
            if (inputHelper.KeyPressed(Keys.Up))
            {
                velocity.Y = -walkingSpeed;
            }
            if (inputHelper.KeyPressed(Keys.Down))
            {
                velocity.Y = walkingSpeed;
            }
            if (velocity.X != 0.0f)
            {
                Mirror = velocity.X < 0;
            }


        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isAlive)
            {
               
                if (velocity.X == 0)
                {
                    PlayAnimation("idle");
                }
                else
                {
                    PlayAnimation("run");
                }
                
                if (velocity.Y < 0)
                {
                    PlayAnimation("jump");
                }
            }

            HandleCollisions();
        }

        private void HandleCollisions()
        {
        }

        

        public bool IsAlive
        {
            get { return isAlive; }
        }
    }
}
