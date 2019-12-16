using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Ghost : EnemyObject
{
    
    public Ghost(Vector2 startPosition) : base(2, "Ghost")
    {
        this.position = startPosition;
        //set strength of ghost
        strength = 30;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //change its strength value based on the amount of health it has
        if (health < 21)
            strength = 20;
        if (health < 11)
            strength = 10;
        if (health < 1)
        {
            //removes the instance from game
            GameWorld.Remove(this);
        }
        Attack();
    }

    //calculates attack and removes instance from game
    private void Attack()
    {
        Player player = GameWorld.GameWorld.Find("Elf") as Player;
        if (CollidesWith(player))
        {
            player.health -= strength;
            GameWorld.Remove(this);
        } 
        
//        if (CollidesWithObject())
//        {
            //Assuming it can differentiate wall and player
            //Player.Health -= strength;
            //Yeetus deletus instance;
//        }
    }
}

