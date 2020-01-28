using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

public class SpriteGameObject : GameObject
{
    [JsonIgnore]
    protected SpriteSheet sprite;

    protected Vector2 origin;
    public bool PerPixelCollisionDetection = true;
    protected float rotation = 0, scale = 1f;
    protected Color color = Color.White;

    public SpriteGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0)
        : base(layer, id)
    {
        if (assetName != "")
        {
            sprite = new SpriteSheet(assetName, sheetIndex);
        }
        else
        {
            sprite = null;
        }
    }

    public void Rotate(int degrees)
    {
        rotation = (float)MathHelper.ToRadians(degrees);
    }

    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
        {
            return;
        }
        if ((GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("playingState") || 
            GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState")) &&
            layer < 80)
        {
            sprite.Draw(spriteBatch, this.GlobalPosition - Camera.Position, rotation, origin, scale, color);
        }
        else
        {
            sprite.Draw(spriteBatch, this.GlobalPosition, rotation, origin, scale, color);
        }
    }

    [JsonIgnore]
    public virtual SpriteSheet Sprite
    {
        get { return sprite; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            if(sprite != null)
            {
                return sprite.Width;
            }
            else
            {
                return 0;
            }
            
        }
    }

    public int Height
    {
        get
        {
            if (sprite != null)
            {
                return sprite.Height;
            }
            else
            {
                return 0;
            }
        }
    }

    public bool Mirror
    {
        get
        {
            if (sprite != null)
            {
                return sprite.Mirror;
            }
            else
            {
                return true;
            }
        }
        set { sprite.Mirror = value; }
    }

    public Vector2 Origin
    {
        get { return origin; }
        set { origin = value; }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public bool CollidesWith(SpriteGameObject obj)
    {
        if (!visible || !obj.visible || !BoundingBox.Intersects(obj.BoundingBox))
        {
            return false;
        }
        if (!PerPixelCollisionDetection)
        {
            return true;
        }
        Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
        for (int x = 0; x < b.Width; x++)
        {
            for (int y = 0; y < b.Height; y++)
            {
                int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                if (sprite.IsTranslucent(thisx, thisy) && obj.sprite.IsTranslucent(objx, objy))
                {
                    return true;
                }
            }
        }
        return false;
    }


    public void SetSprite(SpriteSheet sprite)
    {
        this.sprite = sprite;

    }
}
