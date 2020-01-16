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
    HorizontalDoor,
    VerticalDoor,
    Trap
}

class Tile : SpriteGameObject
{
    protected TileType type;

    public new int Width = 64;
    public new int Height = 64;
    public Tile(string assetname = "", TileType tp = TileType.Background, int layer = 0, string id = "")
        : base(assetname, layer, id)
    {
        type = tp;
        scale = 2f;
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

