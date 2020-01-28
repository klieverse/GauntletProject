﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data.SqlClient;
using System;

class GameOverState : GameObjectList
{
    protected IGameLoopObject playingState;
    protected TextBox textBox;
    protected bool enteredState;

    public GameOverState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState");
        SpriteGameObject overlay = new SpriteGameObject("Backgrounds/overlay");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        Add(overlay);

        textBox = new TextBox(new Vector2(560, 457));
        Add(textBox);
        enteredState = true;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (enteredState)
        {
            if (!inputHelper.AnyKeyDown)
                enteredState = false;
        }
        else
            base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.Enter))
        {
            UpdateDatabase(textBox.Text, GameEnvironment.SelectedClass);
            enteredState = true;
            PlayingState.Exit();
        }
    }

    public override void Update(GameTime gameTime)
    {

        if (enteredState)
            textBox.Text = "";
        else
            base.Update(gameTime);
        playingState.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        playingState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        textBox.Text = "";
    }

    public void UpdateDatabase(string name, string character)
    {
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Scores(Username, Class, Points) VALUES('" + name + "','" + character + "','" + Score + "');", connection);
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        
    }

    public static int Score
    {
        get;
        set;
    }
}
