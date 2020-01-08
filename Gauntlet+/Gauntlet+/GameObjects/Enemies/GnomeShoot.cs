using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GnomeShoot : AnimatedGameObject
{
    public int strength;
    public GnomeShoot(Vector2 startPosition, Vector2 velocity, int strength) : base(2, "GnomeShoot")
    {
        LoadAnimation("GnomeShoot", "ghost", true);
        PlayAnimation("ghost");
        this.position = startPosition;
        this.velocity = velocity;
        this.strength = strength;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //looks for players in game, if it collides with it, reduces player's health and then this instance gets removed
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                    player.health -= strength;
                    visible = false;
                    GameWorld.Remove(this);
                }
    }
}

