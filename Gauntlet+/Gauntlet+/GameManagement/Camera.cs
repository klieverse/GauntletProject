using Microsoft.Xna.Framework;

public class Camera
{
    public Rectangle cameraField;
    public Vector2 cameraPosition;
    public const int scoreBoardWidth = 505;


    public Camera()
    {
        cameraField = new Rectangle(0, 0, 0, 0);
        cameraPosition = Vector2.Zero;
    }

    public void Update()
    {
        if (GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("playingState"))
        {
            cameraPosition.X = Player.PlayerPosition.X - cameraField.Width / 2;
            cameraPosition.Y = Player.PlayerPosition.Y - cameraField.Height / 2;
            if (cameraPosition.X < 0)
            {
                cameraPosition.X = 0;
            }
            else if (cameraPosition.X > 64*55 + scoreBoardWidth - cameraField.Width)
            {
                cameraPosition.X = 64*55 + scoreBoardWidth - cameraField.Width;
            }
            if (cameraPosition.Y >= 64*55 - cameraField.Height)
            {
                cameraPosition.Y = 64*55 - cameraField.Height;
            }
            else if (cameraPosition.Y < 0)
            {
                cameraPosition.Y = 0;
            }
        }
        else
        {
            cameraPosition.X = 0;
            cameraPosition.Y = 0;
        }
    }
}
