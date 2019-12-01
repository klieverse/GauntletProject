using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class TitleMenuState : GameObjectList
{
    protected Button singlePlayerButton, multiPlayerButton, helpButton;

    public TitleMenuState()
    {
        // load the title screen background
        SpriteGameObject titleScreen = new SpriteGameObject("Backgrounds/spr_title", 1, "background");
        Add(titleScreen);

        // add a single player button
        singlePlayerButton = new Button("Sprites/Buttons/SinglePlayerButton", 1);
        singlePlayerButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - singlePlayerButton.Width) / 2, 370);
        Add(singlePlayerButton);

        //add a multi player button
        multiPlayerButton = new Button("Sprites/Buttons/MultiPlayerButton", 1);
        multiPlayerButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - singlePlayerButton.Width) / 2, 470);
        Add(multiPlayerButton);

        // add a help button
        helpButton = new Button("Sprites/Buttons/HelpButton", 1);
        helpButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - helpButton.Width) / 2, 570);
        Add(helpButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (singlePlayerButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
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
