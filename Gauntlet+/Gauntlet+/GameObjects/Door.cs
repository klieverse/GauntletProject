using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


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
                if (CollidesWith(player))
                {
                    player.UseKey();
                    visible = false;
                    /*if ( TileField.Door(x,y+1) == this.door)
                    {
                        this.visible = false; 
                    } 
                    */
                }
    }
}

