using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    //protected Button quitButton;

    public Level(int levelIndex)
    {
        // load the backgrounds


        //quitButton = new Button("Sprites/spr_button_quit", 100);
        // quitButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 10);
        //Add(quitButton);

        Add(new GameObjectList(2, "players"));
        Add(new GameObjectList(2, "enemies"));
        Add(new GameObjectList(1, "items"));
        Add(new Elf(2,"player",new Vector2(300, 300),this, true));

        LoadTiles("Content/Levels/" + levelIndex + ".txt");
    }

    public bool Completed
    {
        get
        {
            //SpriteGameObject exitObj = Find("exit") as SpriteGameObject;
            //Player player = Find("player") as Player;
            //if (!exitObj.CollidesWith(player))
            //{
            //    return false;
            //}

            //return true;
            return false;
        }
    }

    public bool GameOver
    {
        get
        {
            //Player player = Find("player") as Player;
            //return !player.IsAlive;
            return false;
        }
    }
}
