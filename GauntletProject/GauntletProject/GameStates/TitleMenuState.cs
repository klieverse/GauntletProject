using Microsoft.Xna.Framework;

class TitleMenuState : GameObjectList
{
    protected Button singlePlayerButton, multiPlayerButton, helpButton;

    public TitleMenuState()
    {
        // load the title screen
        SpriteGameObject titleScreen = new SpriteGameObject("Backgrounds/spr_title", 0, "background");
        Add(titleScreen);

        // add a single player button
        singlePlayerButton = new Button("Sprites/Button", 1);
        singlePlayerButton.Position = new Vector2((GameEnvironment.Screen.X - singlePlayerButton.Width) / 2, 440);
        Add(singlePlayerButton);

        //add a multi player button
        multiPlayerButton = new Button("Sprites/Button", 1);
        multiPlayerButton.Position = new Vector2((GameEnvironment.Screen.X - singlePlayerButton.Width) / 2, 540);
        Add(singlePlayerButton);

        // add a help button
        helpButton = new Button("Sprites/Button", 1);
        helpButton.Position = new Vector2((GameEnvironment.Screen.X - helpButton.Width) / 2, 600);
        Add(helpButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (singlePlayerButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("singlePlayerState");
        }
        else if(multiPlayerButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("multiPlayerState");
        }
        else if (helpButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("helpState");
        }
    }
}
