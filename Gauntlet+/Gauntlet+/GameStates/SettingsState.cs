using Microsoft.Xna.Framework;

class SettingsState : GameObjectList
{
    protected Button backButton, controllerButton, keyboardButton;
    protected SoundSlider slider;

    public SettingsState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Backgrounds/spr_settings", 0, "background");
        Add(background);

        // add a back but.ton
        backButton = new Button("Sprites/Exit", 100);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Width) / 2, 750);
        Add(backButton);

        slider = new SoundSlider(new Vector2(500, GameEnvironment.Screen.Y/2));
        Add(slider);

        // add controls button
        controllerButton = new Button("", 1);
        controllerButton.Position = new Vector2((GameEnvironment.Screen.X - controllerButton.Width) / 2, (GameEnvironment.Screen.X - controllerButton.Height) / 2);
        Add(controllerButton);

        keyboardButton = new Button("", 1);
        keyboardButton.Position = new Vector2((GameEnvironment.Screen.X - controllerButton.Width) / 2, GameEnvironment.Screen.X + 50);
        Add(keyboardButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
        slider.HandleInput(inputHelper);
        if (controllerButton.Pressed)
        {
            InputHelper.UsingController = false;
            keyboardButton.Position = controllerButton.Position;
            controllerButton.Position = new Vector2((GameEnvironment.Screen.X - controllerButton.Width) / 2, GameEnvironment.Screen.X + 50); 
        }
        if (keyboardButton.Pressed)
        {
            if (inputHelper.ControllerConnected())
            {
                InputHelper.UsingController = true;
                controllerButton.Position = keyboardButton.Position;
                keyboardButton.Position = new Vector2((GameEnvironment.Screen.X - controllerButton.Width) / 2, GameEnvironment.Screen.X + 50);
            }
            
        }
    }

}
