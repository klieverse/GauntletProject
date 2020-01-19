using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Wall : Tile
{
    public Wall(Vector2 position, int layer, string id, string assetname) : base(assetname, TileType.Wall, layer, id)
    {
    }
}

