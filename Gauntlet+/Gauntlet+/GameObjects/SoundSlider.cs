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
    float sliderWidth = 400;
    Texture2D line;

    public SoundSlider(Vector2 startposition) : base("SoundSlider", 100)
    {
        this.startPosition = startposition;
        maxPosition = startposition + new Vector2(sliderWidth, 0);
        position = maxPosition;
        slide = false;
        line = GameEnvironment.AssetManager.GetSprite("SoundSliderLine");
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
            position.X = inputHelper.MousePosition.X-21;
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
        for (float i = startPosition.X +18; i < maxPosition.X +24; i++)
            spriteBatch.Draw(line, new Vector2(i, position.Y + 48), Color.White);
        base.Draw(gameTime, spriteBatch);
    }

}

