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

        GameObjectList hintField = new GameObjectList(100);
        Add(hintField);
        SpriteGameObject hintFrame = new SpriteGameObject("Sprites/spr_frame", 1);
        hintField.Position = new Vector2((GameEnvironment.Screen.X - hintFrame.Width) / 2, 10);
        hintField.Add(hintFrame);
        TextGameObject hintText = new TextGameObject("StatFont", 2);
        hintText.Text = GameEnvironment.Connection.ConsoleMessage;
        hintText.Position = new Vector2(120, 25);
        hintText.Color = Color.Black;
        hintField.Add(hintText);
        VisibilityTimer hintTimer = new VisibilityTimer(hintField, 1, "hintTimer");
        Add(hintTimer);

       //GameEnvironment.AssetManager.PlayMusic("Gauntlet-theme");

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
                VisibilityTimer hintTimer = Find("hintTimer") as VisibilityTimer;
                hintTimer.StartVisible();
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
