using Microsoft.Xna.Framework;
using System.Collections.Generic;

public static class Camera
{
    public static Vector2 Position { get; set; }

    public static Rectangle CameraBox
    {
        get { return new Rectangle((int)Position.X, (int)Position.Y, GameEnvironment.Screen.X, GameEnvironment.Screen.Y); }
    }
}
