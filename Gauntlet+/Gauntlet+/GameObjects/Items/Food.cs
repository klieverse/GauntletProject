using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Food : Item
{
    public Food(int layer, string id, Vector2 position)
        : base(layer: 0, id, position)
    {
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
                    player.EatFood();
                    visible = false;
                    GameEnvironment.AssetManager.PlaySound("Food", position.X);
                }
    }
}

