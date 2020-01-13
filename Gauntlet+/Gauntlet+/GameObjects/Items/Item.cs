using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Item : SpriteGameObject
{
    public Item(int layer, string id, Vector2 position)
        : base("", layer, id)
    {
        this.position = position;
    }

    
}

