﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data.SqlClient;
using System;

class GameOverState : GameObjectList
{
    protected IGameLoopObject playingState;

    public GameOverState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState");
        SpriteGameObject overlay = new SpriteGameObject("Backgrounds/overlay");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        Add(overlay);

        GameObjectList hintField = new GameObjectList(100);
        Add(hintField);
        SpriteGameObject hintFrame = new SpriteGameObject("Sprites/spr_frame", 1);
        hintField.Position = new Vector2((GameEnvironment.Screen.X - hintFrame.Width) / 2, 10);
        hintField.Add(hintFrame);
        TextGameObject hintText = new TextGameObject("StatFont", 2);
        hintText.Position = new Vector2(120, 25);
        hintText.Color = Color.Black;
        hintField.Add(hintText);
        VisibilityTimer hintTimer = new VisibilityTimer(hintField, 1, "hintTimer");
        Add(hintTimer);

        try
        {
            var cb = new SqlConnectionStringBuilder();
            cb.DataSource = "gauntletserver.database.windows.net";
            cb.UserID = "KayleighLieverse";
            cb.Password = "$ypl1Dfm$21e1";
            cb.InitialCatalog = "GauntletHighscore";

            using (var connection = new SqlConnection(cb.ConnectionString))
            {
                connection.Open();
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
            hintText.Text = "Highscore list is not available, your score won't be saved";
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Enter))
        {
            UpdateDatabase();
            playingState.Reset();
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }

    }

    public override void Update(GameTime gameTime)
    {
        playingState.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        VisibilityTimer hintTimer = Find("hintTimer") as VisibilityTimer;
        hintTimer.StartVisible();
    }

    public void UpdateDatabase()
    {

    }
}
