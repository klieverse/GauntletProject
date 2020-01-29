using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Troll : EnemyObject
{
    float timer = 0f;
    public Troll(Vector2 startPosition, SpawnObject spawnObject, bool wasSpawned = false) : base(2, "Troll", spawnObject)
    {
        this.wasSpawned = wasSpawned;
        this.position = startPosition;
        strength = 40;
        health = 50;
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
            //strength is based on current health
            if (this.health < 21)
                strength = 20;
            if (this.health < 11)
                strength = 10;
            //dies if health is less than 1
            if (this.health < 1)
            {
                visible = false;
                isDead = true;
                if (spawn != null)
                    spawn.enemies-=1;
            }
            //timer for when it can attack
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > 500f)
            {
                Attack();
                timer = 0f;
            }
        }
    }

    public override void HitByPlayer(float damage)
    {
        health -= (int)damage;
        color = Color.DarkRed;
        colorTimer = 200f;
    }

    //finds player, if collides, then reduce player's health according to this' strength
    private void Attack()
    {
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
            {
                if (CollidesWith(player))
                {
                    player.HitByEnemy(strength);
                    GameEnvironment.AssetManager.PlaySound("Ghoblin attack", position.X);
                }
            }                  
    }
}

