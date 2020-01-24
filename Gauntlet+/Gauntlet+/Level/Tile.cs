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

    public new int Width = 48;
    public new int Height = 48;
    public static int Size = 48;

    public Tile(string assetname = "", TileType tp = TileType.Background, int layer = 0, string id = "")
        : base(assetname, layer, id)
    {
        type = tp;
        if (TileType != TileType.HorizontalDoor && TileType != TileType.VerticalDoor && TileType != TileType.Teleporter)
            scale = 1.5f;
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

