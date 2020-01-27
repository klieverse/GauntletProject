using Microsoft.Xna.Framework;
using System;
using System.Text;
using Microsoft.Xna.Framework.Input;


class TextBox : TextGameObject
{
    static GameWindow gameWindow;
    protected bool writing, pressed;
    StringBuilder myString = new StringBuilder();

    public TextBox(Vector2 position):base("TextFont",100)
    {
        this.position = position;
        gameWindow = GameEnvironment.gameWindow;
        color = Color.Red;
        writing = true;
        text = "";
        pressed = false;
       
        gameWindow.TextInput += OnInput;
    }

    

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.Enter))
            writing = false;
        if (inputHelper.AnyKeyPressed)
        {
           // if (inputHelper.KeyPressed(Keys.Back))
                //myString.Remove(myString.Length-2, 1);
            pressed = true;
        }
        else
            pressed = false;
    }
    String character;
    public void OnInput(object sender, TextInputEventArgs e)
    {
        var k = e.Key;
        var c = e.Character;
        character = c.ToString();
        character.Replace("\t", "");
        character.Replace("\0", "");
        character.Replace("\b", "");
    }

    public override void Update(GameTime gameTime)
    {
        
        if (writing&&pressed)
        {           
            
            gameWindow.TextInput += OnInput;
                text += character;
            gameWindow.TextInput -= OnInput;
            
        }
    }


}

