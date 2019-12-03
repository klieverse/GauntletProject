using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

enum TileType
{
    Background,
    Exit,
    Wall,
    BreakableWall,
    Teleporter,
    Door,
    Trap
}

class Tile : SpriteGameObject
{
    protected TileType type;

    public Tile(string assetname = "", TileType tp = TileType.Background, int layer = 0, string id = "")
        : base(assetname, layer, id)
    {
        type = tp;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }


    public TileType TileType
    {
        get { return type; }
    }
    
}

