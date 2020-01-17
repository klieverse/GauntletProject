using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

//Moeten nog even kijken voor de sprites of we twee losse classes nodig hebben of niet
    class Door : SpriteGameObject
    {
    public Door(int layer, string id, Vector2 position)
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

                //Als de player de deur raakt en een sleutel heeft moet de deur verdwijnen + eventuele deuren ernaast of erboven

                if (CollidesWith(player))
                {
                    
                    player.UseKey();
                    this.visible = false;
                    
                }
    }
}

