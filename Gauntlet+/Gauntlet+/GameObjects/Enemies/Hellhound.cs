using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Hellhound : EnemyObject
{
    float timer = 0f;
    public Hellhound(Vector2 startPosition, SpawnObject spawnObject, bool wasSpawned = false) : base(2, "Hellhound", spawnObject, 300)
    {
        this.wasSpawned = wasSpawned;
        //starting position equal to what is determined in SpawnObject.cs
        position = startPosition;
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
            //strength of enemy reduces with current health
            if (this.health < 21)
                strength = 8;
            if (this.health < 11)
                strength = 5;
            if (this.health < 1)
            {
                visible = false;
                isDead = true;
                spawn.enemies--;
            }
            //cooldown for when the enemy attacks

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

    //attacks by creating a shooting object, where the object shoots in the direction in which the enemy is moving
    private void Attack()
    {
        //HellhoundShoot hellhoundShoot = new HellhoundShoot(this.position, this.velocity, strength);
        (GameWorld.Find("enemieShot") as GameObjectList).Add(new HellhoundShoot(this.position, new Vector2(speedHori, speedVert), strength, this));
        GameEnvironment.AssetManager.PlaySound("Wizard shot", position.X);
    }

    
}

