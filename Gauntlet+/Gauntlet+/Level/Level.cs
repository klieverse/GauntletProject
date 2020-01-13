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
        Add(new GameObjectList(4, "StatFields"));
        Add(new GameObjectList(2, "enemies"));
        Add(new GameObjectList(1, "items"));
        Add(new GameObjectList(3,"playershot"));
        Add(new GameObjectList(3,"enemieShot"));

        Add(new Questor(2, "Elf", new Vector2(150, 150), this, true));
        Add(new PlayerStatField("Elf"));

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
