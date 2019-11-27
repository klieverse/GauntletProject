﻿using Microsoft.Xna.Framework;

class BetweenLevelState : GameObjectList
{
    protected Button backButton;

    public BetweenLevelState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Backgrounds/spr_help", 0, "background");
        Add(background);

        // add a back but.ton
        backButton = new Button("Sprites/spr_button_back", 1);
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