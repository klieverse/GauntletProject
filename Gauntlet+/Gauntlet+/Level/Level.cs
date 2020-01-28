using Microsoft.Xna.Framework;
using System;

partial class Level : GameObjectList
{
    protected Button quitButton;
    public Vector2 startPositionThyra, startPositionQuestor, startPositionThor, startPositionMerlin;
    public int secretValue1 =0 , goalSecretValue1 = 0, secretValue2 = 0, goalSecretValue2 = 2, breakebleWalls=0;

    public Level(int levelIndex)
    {
        // load the backgrounds


        quitButton = new Button("Sprites/Exit", 100);
        Add(quitButton);
        

        Add(new GameObjectList(4, "players"));
        Add(new GameObjectList(2, "spawns")); 
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
        Add(new GameObjectList(2, "SpawnObjects"));

        goalSecretValue1 = breakebleWalls / 2 + 1;

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
            GameObjectList players = Find("players") as GameObjectList;
            Player player = players.Find(GameEnvironment.SelectedClass) as Player;
            return !player.IsAlive;
        }
    }
}
