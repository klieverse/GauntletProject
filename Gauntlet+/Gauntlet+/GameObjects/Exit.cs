using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


class Exit : SpriteGameObject
{

    public Exit(int layer, string id, Vector2 position)
    : base("", layer, id)
    {
        this.position = position;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                    PlayingState.NextLevel();
                }
    }


}