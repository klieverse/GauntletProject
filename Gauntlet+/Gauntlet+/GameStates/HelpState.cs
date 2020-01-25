﻿using Microsoft.Xna.Framework;

class HelpState : GameObjectList
{
    protected Button backButton;

    public HelpState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Backgrounds/spr_help", 0, "background");
        Add(background);

        // add a back button
        backButton = new Button("Sprites/Exit", 100);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Width) / 2, 750);
        Add(backButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
    }
}
