using Microsoft.Xna.Framework;

class SettingsState : GameObjectList
{
    protected Button backButton;
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
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
        slider.HandleInput(inputHelper);
    }
}
