using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Item : SpriteGameObject
{
    public Item(int layer, string id, Vector2 position)
        : base(assetName:"Sprites/Items/"+ id, layer, id)
    {
        this.position = position;
        scale = 1.5f;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
}

