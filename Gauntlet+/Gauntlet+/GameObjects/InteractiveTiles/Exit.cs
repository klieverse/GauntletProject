using System;
using Microsoft.Xna.Framework;

public static class Exit : SpriteGameObject
{

    public Exit(int layer = 0, string id = "") : base("Sprites/spr_ladder", layer, id)
    {
    }

    public override void Update(GameTime gameTime)
    {
        Player player = GameWorld.Find("player") as Player;
        if (CollidesWith(player))
        {
            PlayingState.NextLevel();
        }
    }
}

