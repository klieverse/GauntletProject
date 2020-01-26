using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SoundSlider:SpriteGameObject
{
    Vector2 startPosition, maxPosition;
    bool slide;
    DrawingHelper drawing;
    float sliderWidth = 400;
    protected static Texture2D pixel;

    public SoundSlider(Vector2 startposition) : base("SoundSlider", 100)
    {
        this.startPosition = startposition;
        maxPosition = startposition + new Vector2(sliderWidth, 0);
        position = maxPosition;
        slide = false;
        drawing = new DrawingHelper();
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.MouseLeftButtonDown() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
        {
            slide = true;
        }
        
        if (slide)
        { 
            position.X = inputHelper.MousePosition.X;
            if (position.X < startPosition.X)
               position.X = startPosition.X;
            if (position.X > maxPosition.X)
               position.X = maxPosition.X;
            float volume = (position.X - startPosition.X) / sliderWidth;
            GameEnvironment.Volume = volume;
            if (!inputHelper.MouseLeftButtonDown())
            {
                slide = false;
                GameEnvironment.AssetManager.PlaySound("Ghost hit",-1);
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        
    }

}

