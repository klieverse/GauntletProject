using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Wizard : EnemyObject
{
    //Player closestPlayer;
    float attackTimer = 0f, visibilityTimer = 0f;
    //int check = 0;
    public Wizard(Vector2 startPosition, bool wasSpawned = false) : base(2, "Wizard", canBeInvisible: true)
    {
        this.wasSpawned = wasSpawned;
        //this.closestPlayer = closestPlayer;
        this.position = startPosition;
        strength = 10;
    }

    public override void Update(GameTime gameTime)
    {
        if (wasSpawned && CollidesWithObject() && !beginCollision)
        {
            isDead = true;
            visible = false;
        }
        else
        {
            beginCollision = true;
        }
        if (!isDead)
        {
            base.Update(gameTime);
            //strength based on current health

            //FindClosestPlayer();

            visibilityTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            attackTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //wizard can get invisible, timer which switches state of visibility
            //if (NoInvisibility())
            //{
            if (visibilityTimer > 500 && !noInvisible)
                {
                    visible = !visible;
                    visibilityTimer = 0;
                }
            //}
            //else
            //{
            //    visible = true;
            //}
            //calculates when the enemy can attack
            if (attackTimer > 1000f)
            {
                Attack();
                attackTimer = 0f;
            }
        }

        if (this.health < 21)
            strength = 8;
        if (this.health < 11)
            strength = 5;
        //removes this object when health is less than 1
        if (this.health < 1)
        {
            visible = false;
            isDead = true;

        }

    }

    //find player, if collides, then player loses health according to this' strength;
    private void Attack()
    {
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player) && visible)
                {
                    player.HitByEnemy(strength);
                    GameEnvironment.AssetManager.PlaySound("Wizard attack", position.X);
                }
    }

 /*   private void FindClosestPlayer()
    {
        //check for all the portals in the level
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        foreach (Player player in players)
        {
            if (player != closestPlayer)
            {
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
            //determines the closest portal to this portal, other than this portal itself           


        }
    }

    private bool NoInvisibility()
    {
        //check for all the portals in the level
        /*
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
            return DistanceToPlayer();
    }

    private bool DistanceToPlayer()
    {
        
        if (closestPlayer != null)
        {
            float opposite = closestPlayer.Position.Y /*+ closestPlayer.Height / 4) - position.Y;
            float adjacent = closestPlayer.Position.X - position.X;
            distance = (float)Math.Sqrt((opposite * opposite) + (adjacent * adjacent));
            if (distance < 50)
            {
                return true;
            }
        }
        if (check > 2)
        {
            return true;
        }
        check++;
        return false;
    } */
}

