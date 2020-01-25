using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class TitleMenuState : GameObjectList
{
    protected Button singlePlayerButton, multiPlayerButton, helpButton, settingsButton;

    public TitleMenuState()
    {
        // load the title screen background
        SpriteGameObject titleScreen = new SpriteGameObject("Backgrounds/spr_title", 1, "background");
        Add(titleScreen);

        // add a single player button
        singlePlayerButton = new Button("Sprites/Buttons/SinglePlayerButton", 1);
        singlePlayerButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - singlePlayerButton.Width) / 2, 350);
        Add(singlePlayerButton);

        //add a multi player button
        multiPlayerButton = new Button("Sprites/Buttons/MultiPlayerButton", 1);
        multiPlayerButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - singlePlayerButton.Width) / 2, 440);
        Add(multiPlayerButton);

        // add a settings button
        settingsButton = new Button("Sprites/Buttons/Settings", 1);
        settingsButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - settingsButton.Width) / 2, 530);
        Add(settingsButton);

        // add a help button
        helpButton = new Button("Sprites/Buttons/HelpButton", 1);
        helpButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - helpButton.Width) / 2, 620);
        Add(helpButton);

        
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (singlePlayerButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("chooseCharacterState");
        }
        else if(multiPlayerButton.Pressed)
        {
            if(GameEnvironment.Connection.multiplayerAllowed)
            {
                GameEnvironment.GameStateManager.SwitchTo("multiplayerCharacterState");
            }
            else
            {
                Console.WriteLine("Connection is not available");
            }
            
        }
        else if(settingsButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("SettingsState");
        }
        else if (helpButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("helpState");
        }
    }
    
}
