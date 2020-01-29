using Microsoft.Xna.Framework;

class SettingsState : GameObjectList
{
    protected Button backButton, controllerButton, keyboardButton;
    protected SoundSlider slider;
    SpriteGameObject selector;

    public SettingsState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Backgrounds/spr_Settings", 0, "background", scale: 1f);
        Add(background);

        // add a back but.ton
        backButton = new Button("Sprites/Exit", 100);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Width) / 2, 750);
        Add(backButton);

        slider = new SoundSlider(new Vector2(500, GameEnvironment.Screen.Y/3));
        Add(slider);

        // add controls button
        controllerButton = new Button("Sprites/Buttons/Controller", 30);
        controllerButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - controllerButton.Width) / 2 - 200, 500);
        Add(controllerButton);

        keyboardButton = new Button("Sprites/Buttons/Keyboard", 30);
        keyboardButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - keyboardButton.Width) / 2 + 200,500);
        Add(keyboardButton);

        selector = new SpriteGameObject("Backgrounds/Selector", 40, scale: 0.95f);
        selector.Position = selector.Position = new Vector2(675, 315);
        Add(selector);
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
            if (inputHelper.ControllerConnected())
            {
                InputHelper.UsingController = true;
                controllerButton.Position = new Vector2((GameEnvironment.Screen.X - controllerButton.Width) / 2, GameEnvironment.Screen.X + 200);
                selector.Position = new Vector2(275, 315);
            }
        }
        if (keyboardButton.Pressed)
        {
            InputHelper.UsingController = false;
            keyboardButton.Position = new Vector2((GameEnvironment.Screen.X - keyboardButton.Width) / 2, GameEnvironment.Screen.X + 200);
            selector.Position = new Vector2(675, 315);
        }
    }

}
