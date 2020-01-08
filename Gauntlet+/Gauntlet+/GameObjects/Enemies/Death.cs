using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Death : EnemyObject  
{
    float timer = 0f;
    public Death(Vector2 startPosition) : base(2, "Death")
    {
        this.position = startPosition;
        strength = 4;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //cooldown for when the enemy attacks
        if (timer == 0f)
        {
            Attack();
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        else if (timer > 50f)
        {
            timer = 0f;
        }
    }

    //how the enemy attacks
    private void Attack()
    {
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                    player.HitByEnemy(strength);
                }
    }
}

