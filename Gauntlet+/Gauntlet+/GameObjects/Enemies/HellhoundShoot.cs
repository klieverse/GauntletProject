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
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                    player.health -= strength;
                    visible = false;
                    GameWorld.Remove(this);
                    GameEnvironment.AssetManager.PlaySound("Ghost hit");
                }
    }
}

