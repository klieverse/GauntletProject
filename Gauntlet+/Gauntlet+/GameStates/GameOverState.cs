using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data.SqlClient;
using System;

class GameOverState : GameObjectList
{
    protected IGameLoopObject playingState;
    SqlConnection connection;

    public GameOverState()
    {
        playingState = GameEnvironment.GameStateManager.GetGameState("playingState");
        SpriteGameObject overlay = new SpriteGameObject( "Backgrounds/spr_gameover");
        overlay.Position = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 - overlay.Center;
        Add(overlay);

        
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Enter))
        {
            try
            {
                var cb = new SqlConnectionStringBuilder();
                cb.DataSource = "gauntletserver.database.windows.net";
                cb.UserID = "KayleighLieverse";
                cb.Password = "$ypl1Dfm$21e1";
                cb.InitialCatalog = "GauntletHighscore";

                using (connection = new SqlConnection(cb.ConnectionString))
                {
                    connection.Open();
                    //TO DO: NEW NAME
                    string name = "Kayleigh";
                    string character = GameEnvironment.SelectedClass;
                    //TO DO: CORRECT POINTS
                    int points = 200;

                    UpdateDatabase(name, character, points);
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Highscore list not available, your score won't be tracked");
            }
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

    public void UpdateDatabase(string name, string character, int points)
    {
        //adds the person to the database
        SqlCommand cmd = new SqlCommand("INSERT INTO Scores(Username, Class, Points) VALUES('" + name + "','" + character + "','" + points + "');", connection);
        cmd.ExecuteNonQuery();

    }
}
