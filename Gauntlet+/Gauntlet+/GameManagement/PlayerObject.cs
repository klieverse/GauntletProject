﻿using Microsoft.Xna.Framework;
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
        protected bool isAlive;
        protected float walkingSpeed;
        protected int health = 600;
        protected float armor;
        protected float magic;
        protected float shotStrength;
        protected float shotSpeed;
        protected PlayerShot playerShot;

        //protected List<Item> inventory;

        public PlayerObject(int layer, string id, Vector2 start, Level level, float speed, float armor, 
                            float magic, float shotStrength, float shotSpeed, float melee)
        : base(layer, id)
        {
            this.level = level;

            LoadAnimation("Sprites/Player/spr_" + id + "idle", id + "idle", true);
            LoadAnimation("Sprites/Player/spr_" + id + "runRight", id + "runRight", true);
            LoadAnimation("Sprites/Player/spr_" + id + "runDownRight", id + "runDownRight", true);
            LoadAnimation("Sprites/Player/spr_" + id + "runDown", id + "runDown", true);
            LoadAnimation("Sprites/Player/spr_" + id + "runDownLeft", id + "runDownLeft", true);
            LoadAnimation("Sprites/Player/spr_" + id + "runLeft", id + "runLeft", true);
            LoadAnimation("Sprites/Player/spr_" + id + "runUpLeft", id + "runUpLeft", true);
            LoadAnimation("Sprites/Player/spr_" + id + "runUp", id + "runUp", true);
            LoadAnimation("Sprites/Player/spr_" + id + "die", id + "die", false);

            walkingSpeed = speed;
            this.armor = armor;
            this.magic = magic;
            this.shotStrength = shotStrength;
            this.shotSpeed = shotSpeed;
            this.id = id;
            startPosition = start;
            //protected List<Item> invent = new List<Item>();
            Reset();
        }

        public override void Reset()
        {
            position = startPosition;
            velocity = Vector2.Zero;
            isAlive = true;
            PlayAnimation(id + "idle");
            
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
            else if (inputHelper.IsKeyDown(Keys.Right))
            {
                velocity.X = walkingSpeed;
            }
            else if (inputHelper.KeyPressed(Keys.Up))
            {
                velocity.Y = -walkingSpeed;
            }
            else if (inputHelper.KeyPressed(Keys.Down))
            {
                velocity.Y = walkingSpeed;
            }

            else if (inputHelper.IsKeyDown(Keys.Left) && inputHelper.IsKeyDown(Keys.Up))
            {
                velocity.Y = 0.71f * -walkingSpeed;
                velocity.X = 0.71f * -walkingSpeed;
            }
            else if (inputHelper.IsKeyDown(Keys.Left) && inputHelper.IsKeyDown(Keys.Down))
            {
                velocity.Y = 0.71f * walkingSpeed;
                velocity.X = 0.71f * -walkingSpeed;
            }
            else if (inputHelper.IsKeyDown(Keys.Right) && inputHelper.IsKeyDown(Keys.Up))
            {
                velocity.Y = 0.71f * -walkingSpeed;
                velocity.X = 0.71f * walkingSpeed;
            }
            else if (inputHelper.IsKeyDown(Keys.Right) && inputHelper.IsKeyDown(Keys.Down))
            {
                velocity.Y = 0.71f * walkingSpeed;
                velocity.X = 0.71f * walkingSpeed;
            }

            if (inputHelper.IsKeyDown(Keys.Space))
            {
                Shoot(velocity);
            }
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isAlive)
            {
                HandleAnimations();
                HandleCollisions();
            }

            if(health <= 0)
            {
                Die();
            }
        }

        private void HandleAnimations()
        {
            if (velocity == Vector2.Zero)
            {
                PlayAnimation(id + "idle");
            }
            else if (velocity.X > 0 && velocity.Y ==0)
            {
                PlayAnimation(id + "runRight");
            }
            else if (velocity.X > 0 && velocity.Y > 0)
            {
                PlayAnimation(id + "runDownRight");
            }
            else if (velocity.X == 0 && velocity.Y > 0)
            {
                PlayAnimation(id + "runDown");
            }
            else if (velocity.X < 0 && velocity.Y > 0)
            {
                PlayAnimation(id + "runDownLeft");
            }
            else if (velocity.X < 0 && velocity.Y == 0)
            {
                PlayAnimation(id + "runLeft");
            }
            else if (velocity.X < 0 && velocity.Y < 0)
            {
                PlayAnimation(id + "runUpLeft");
            }
            else if (velocity.X == 0 && velocity.Y < 0)
            {
                PlayAnimation(id + "runUp");
            }
        }

        private void Shoot(Vector2 direction)
        {
            playerShot = new PlayerShot(id, shotSpeed, shotStrength, velocity);
        }

        public void Die()
        {
            if (!isAlive)
            {
                return;
            }

            isAlive = false;
            velocity.Y = -900;
            GameEnvironment.AssetManager.PlaySound("Sounds/snd_" + id + "_die");
        
            PlayAnimation(id + "die");
        }

        private void HandleCollisions()
        {
            //foreach (var EnemyObject in List<EnemyObject>)
            //{
                //CollidesWith(EnemyObject) ????????
            //}

        }

        public void HitByEnemy(float EnemyStrength)//, EnemyObject)
        {
            float Damage = (0.5f * EnemyStrength) + (0.5f * EnemyStrength * (1- ((armor/100) / (armor/100+1)) ));
            health -= (int)Damage; 
        }

        public void ArmorUp()
        {
            armor += 10f;
        }
        

        public bool IsAlive
        {
            get { return isAlive; }
        }
    }
}
