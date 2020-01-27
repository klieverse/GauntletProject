using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Button : SpriteGameObject
{
    protected bool pressed;
    protected Vector2 beginPosition;

    public Button(string imageAsset, int layer = 0, string id = "")
        : base(imageAsset, layer, id)
    {
        pressed = false;
        beginPosition = Vector2.Zero;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        pressed = inputHelper.MouseLeftButtonPressed() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
        if (BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
        {
            position = beginPosition - new Vector2(5, 5);
        }
        else
        {
            position = beginPosition;
        }
    }

    public override void Reset()
    {
        base.Reset();
        pressed = false;
    }

    public bool Pressed
    {
        get { return pressed; }
    }

    public virtual Vector2 BeginPosition
    {
        get { return beginPosition; }
        set { beginPosition = value; }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
        {
            return;
        }
        sprite.Draw(spriteBatch, this.GlobalPosition , rotation, origin, scale);
    }
}
