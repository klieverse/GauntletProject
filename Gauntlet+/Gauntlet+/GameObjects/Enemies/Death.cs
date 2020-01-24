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

    public Death(Vector2 startPosition) : base(2, "Death")
    {
        this.position = startPosition;
        strength = 4;
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
                }
                else if (enoughDamage > 100)
                {
                    visible = false;
                    isDead = true;
                }
    }
}

