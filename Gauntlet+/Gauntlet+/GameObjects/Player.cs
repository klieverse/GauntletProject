using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

class Player : SpriteGameObject
{
    public static Vector2 playerPosition;
    public int health = 600;

    public Player(Vector2 start) : base("Player", 2, id:"player")
    {
        Vector2 playerPosition = start;
        position = start;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.IsKeyDown(Keys.Left))
        {
            position.X -= 10;
        }
        else if (inputHelper.IsKeyDown(Keys.Right))
        {
            position.X += 10;
        }
        else if (inputHelper.IsKeyDown(Keys.Down))
        {
            position.Y += 10;
        }
        else if (inputHelper.IsKeyDown(Keys.Up))
        {
            position.Y -= 10;
        }
        playerPosition = position;
    }

    public static Vector2 PlayerPosition
    {
        get
        {
            return playerPosition;
        }
    }
}

