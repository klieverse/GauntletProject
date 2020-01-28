using Microsoft.Xna.Framework;
using System.Collections.Generic;

public static class Camera
{
    public static Vector2 Position { get; set; }

    public static Rectangle CameraBox
    {
        get { return new Rectangle((int)Position.X - 300, (int)Position.Y - 300, GameEnvironment.Screen.X + 600, GameEnvironment.Screen.Y + 600); }
    }
}
