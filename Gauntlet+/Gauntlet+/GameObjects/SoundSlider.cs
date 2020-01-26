using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SoundSlider:SpriteGameObject
{
    Vector2 startPosition, maxPosition;
    bool slide;
    public SoundSlider(Vector2 startposition): base("ValkeryShot",100)
    {
        this.startPosition = startposition;
        maxPosition = startposition + new Vector2(500, 0);
        position = maxPosition;
        slide = false;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.MouseLeftButtonDown() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
        {
            slide = true;
        }
        if (!inputHelper.MouseLeftButtonDown())
            slide = false;
        position.X = inputHelper.MousePosition.X;
        if (position.X < startPosition.X)
            position.X = startPosition.X;
        if (position.X > maxPosition.X)
            position.X = maxPosition.X;
        float volume = (position.X - startPosition.X) / 500;
        GameEnvironment.Volume = volume;
    }

    
}

