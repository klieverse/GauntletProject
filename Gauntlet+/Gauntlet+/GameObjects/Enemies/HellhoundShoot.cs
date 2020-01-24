using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class HellhoundShoot : EnemyShot
{
    public HellhoundShoot(Vector2 startPosition, Vector2 velocity, int strength, EnemyObject shooter) : base(2, "HellhoundShoot", isGnome: false)
    {
        this.shooter = shooter;
        this.position = startPosition;
        this.velocity = velocity;
        this.strength = strength;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

       /* List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                    player.health -= strength;
                    visible = false;
                } */
    }
}

