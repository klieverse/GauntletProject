using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Death : EnemyObject  
{
    float timer = 0f;
    int enoughDamage = 0;

    public Death(Vector2 startPosition, SpawnObject spawnObject) : base(2, "Death", spawnObject)
    {
        this.position = startPosition;
        strength = 5;
    }

    public override void Update(GameTime gameTime)
    {
        if (!isDead)
        {
            base.Update(gameTime);
            //cooldown for when the enemy attacks
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > 50f)
            {
                Attack();
                timer = 0;
                
            }

        }
        
    }

    //how the enemy attacks
    private void Attack()
    {
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player) && enoughDamage <= 100)
                {
                    player.health -= strength;
                    enoughDamage++;
                    GameEnvironment.AssetManager.PlaySound("Death", position.X);
                }
                else if (enoughDamage > 150)
                {
                    visible = false;
                    isDead = true;
                }
    }
}

