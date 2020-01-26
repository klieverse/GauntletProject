using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Thief : EnemyObject
{
    readonly int thiefSpeed = 900;
    bool escape = false;
    float escapeDistance;

    public Thief(Vector2 startPosition) : base(2, "Thief")
    {
        speed = thiefSpeed;
        position = startPosition;
        strength = 10;
        health = 20;
    }

    public override void Update(GameTime gameTime)
    {
        if (!isDead)
        {
            base.Update(gameTime);
            //List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
            //if (CollidesWith(players) )
            
            

            if (health <= 15)
                strength = 5;
            //dies if health is less than 1
            if (health <= 0)
            {
                visible = false;
                isDead = true;
            }

            if (!escape)
            {
                Attack();
            }
            else
            {
                escapeDistance = distance;
                velocity.X *= -1;
                velocity.Y *= -1;
                Escaping();
            }
            
        }
       
    }

    //executes attack when it collides with player.
    private void Attack()
    {
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players) 
                if (CollidesWith(player))
                {
                    player.HitByEnemy(strength);
                    escape = true;
                    GameEnvironment.AssetManager.PlaySound("Thief laughter", position.X);
                }
    }

    private void Escaping()
    {
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (escapeDistance >= 1000)
                {
                    visible = false;
                    isDead = true;
                }
    }
}

