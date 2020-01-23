using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    //protected Button quitButton;
    protected Vector2 startPositionThyra, startPositionQuestor, startPositionThor, startPositionMerlin;

    public Level(int levelIndex)
    {
        // load the backgrounds


        //quitButton = new Button("Sprites/spr_button_quit", 100);
        // quitButton.Position = new Vector2(GameEnvironment.Screen.X - quitButton.Width - 10, 10);
        //Add(quitButton);
        

        Add(new GameObjectList(4, "players"));
        Add(new GameObjectList(6, "StatFields"));
        Add(new GameObjectList(4, "enemies"));
        Add(new GameObjectList(3, "food"));
        Add(new GameObjectList(3, "keys"));
        Add(new GameObjectList(3, "treasures"));
        Add(new GameObjectList(3, "potions"));
        Add(new GameObjectList(5, "playershot"));
        Add(new GameObjectList(5, "enemieShot"));
        Add(new GameObjectList(2, "teleport"));
        Add(new GameObjectList(2, "BreakableWalls"));
        Add(new GameObjectList(2, "Doors"));
        Add(new GameObjectList(2, "Exits"));
        // Add(new Questor(2, "Elf", new Vector2(150, 150), this, true));
        // Add(new PlayerStatField("Elf"));

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
