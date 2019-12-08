using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class HellhoundShoot : AnimatedGameObject
{
    public int strength;
    public HellhoundShoot(Vector2 startPosition, Vector2 velocity, int strength) : base(2, "HellhoundSHoot")
    {
        LoadAnimation("Ghost", "ghost", true);
        PlayAnimation("ghost");
        this.position = startPosition;
        this.velocity = velocity;
        visible = true;
        this.strength = strength;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player))
        {
            player.health -= strength;
            GameWorld.Remove(this);
        }
    }
}

