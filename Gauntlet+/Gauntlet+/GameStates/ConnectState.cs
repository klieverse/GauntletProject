using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data.SqlClient;
using System;
using System.Net;

class ConnectState : GameObjectList
{
    public Button backButton;
    protected TextBox textBox;
    protected TextGameObject console;

    public ConnectState()
    {
        console = new TextGameObject("TextFont", 150);
        console.Color = Color.Red;
        console.Position = new Vector2(560, 257);
        console.Text = "Fill in IP4: ";
        textBox = new TextBox("ip", new Vector2(560, 457));
        Add(textBox);

        backButton = new Button("Sprites/Exit", 100);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Width) / 2, 750);
        Add(backButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        backButton.HandleInput(inputHelper);
        textBox.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.Enter))
        {
            CheckIP(textBox.Text);
        }
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        textBox.Update(gameTime);
        console.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        textBox.Draw(gameTime, spriteBatch);
        console.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        console.Text = "Fill in IP4: ";
        textBox.Text = "";
    }

    public void CheckIP(string ip)
    {
        try
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            GameEnvironment.Connection.Connecting(ipAddress);
            if(GameEnvironment.Connection.multiplayerAllowed)
            {
                GameEnvironment.GameStateManager.SwitchTo("multiplayerCharacterState");
            }
            else
            {
                console.Text = "Could not connect to server";
            }
        }
        catch
        {
            console.Text = "Not a valid IP, try again: ";
            textBox.Text = "";
        }
    }
}
