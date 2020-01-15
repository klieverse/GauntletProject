using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Thief : EnemyObject
{
    readonly int thiefSpeed = 500;

    public Thief(Vector2 startPosition) : base(2, "Thief")
    {
        speed = thiefSpeed;
        position = startPosition;
        strength = 10;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Attack();
    }

    //executes attack when it collides with player.
    private void Attack()
    {
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players) 
                if (CollidesWith(player))
                {
                    player.health -= strength;
                    //steal item, run away
     
                }
    }
}

