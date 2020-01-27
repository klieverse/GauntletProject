using Microsoft.Xna.Framework;

class SettingsState : GameObjectList
{
    protected Button backButton, controllerButton, keyboardButton;

    public SettingsState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Backgrounds/spr_settings", 0, "background");
        Add(background);

        // add a back but.ton
        backButton = new Button("Sprites/Exit", 100);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Width) / 2, 750);
        Add(backButton);

        // add controls button
        controllerButton = new Button("", 1);
        controllerButton.Position = new Vector2((GameEnvironment.Screen.X - controllerButton.Width) / 2, (GameEnvironment.Screen.X - controllerButton.Height) / 2);
        Add(controllerButton);

        keyboardButton = new Button("", 1);
        keyboardButton.Position = new Vector2((GameEnvironment.Screen.X - controllerButton.Width) / 2, GameEnvironment.Screen.X + 50);

    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
        if (controllerButton.Pressed)
        {
            InputHelper.UsingController = false;
            keyboardButton.Position = controllerButton.Position;
            //controllerButton.Position.Y += GameEnvironment.Screen.X; 
        }
        if (keyboardButton.Pressed)
        {
            InputHelper.UsingController = true;
        }
    }
}
