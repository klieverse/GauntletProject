using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;


partial class Level : GameObjectList
{

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        /*if (quitButton.Pressed)
        {
            Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }*/
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        LoadPlayer();
        //Player player = Find("player") as Player;

        // check if we died

       
        // check if we ran out of time

        //for each existing player update the fitting statfield with the data of said player
        foreach (Player player in (Find("players") as GameObjectList).Children)
        {
                (Find(player.playerClass + "Stats") as PlayerStatField).Update(player);
        }



    }

    public void LoadPlayer()
    {
        string playerClass= GameEnvironment.SelectedClass; //Get the selected class
        // Check if the selected class is loaded, if not load the right player into the level
        //if multiplayer is chosen, all characters will be loaded
        if (Find(playerClass) == null)
        {

            if (playerClass == "Elf" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                //get the startPosition for the player
                Vector2 startPosition = new Vector2(((float)startPositionQuestor.X + 0.5f) * Tile.Size, (startPositionQuestor.Y + 1) * Tile.Size);
                Questor questor = new Questor(4, "Elf", startPosition, this, true); //create the player
                (Find("players") as GameObjectList).Add(questor); //add player to level
                PlayerStatField questorStats = new PlayerStatField("Elf"); //create the players Statfield
                (Find("StatFields") as GameObjectList).Add(questorStats); //add the statfield to the level
            }
            if(playerClass == "wizard" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                Vector2 startPosition = new Vector2(((float)startPositionMerlin.X + 0.5f) * Tile.Size, (startPositionMerlin.Y + 1f) * Tile.Size);
                Merlin merlin = new Merlin(4, "Wizard", startPosition, this, true);
                (Find("players") as GameObjectList).Add(merlin);
                PlayerStatField wizardStats = new PlayerStatField("Wizard");
                (Find("StatFields") as GameObjectList).Add(wizardStats);
            }
            if(playerClass =="Warrior" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                Vector2 startPosition = new Vector2(((float)startPositionThor.X) * Tile.Size, (startPositionThor.Y) * Tile.Size);
                Thor thor = new Thor(4, "Warrior", startPosition, this, true);
                (Find("players") as GameObjectList).Add(thor);
                PlayerStatField warriorStats = new PlayerStatField("Warrior");
                (Find("StatFields") as GameObjectList).Add(warriorStats);
            }
            if(playerClass == "Valkery" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                Vector2 startPosition = new Vector2((startPositionThyra.X + 2) * Tile.Size, (startPositionThyra.Y + 2) * Tile.Size);
                Thyra thyra = new Thyra(4, "Valkery", startPosition, this, true);
                (Find("players") as GameObjectList).Add(thyra);
                PlayerStatField valkeryStats = new PlayerStatField("Valkery");
                (Find("StatFields") as GameObjectList).Add(valkeryStats);

            }
        }
    }

    public override void Reset()
    {
        base.Reset();
    }
    
    public void SendInformation()
    {
        //sends the player class
        GameObjectList players = GameWorld.Find("players") as GameObjectList;
        Player player = players.Find(GameEnvironment.SelectedClass) as Player;
        //Console.WriteLine(player.ToString());
        string jplayer = JsonConvert.SerializeObject(player);
        Console.WriteLine(player.ToString());
        GameEnvironment.Connection.Send(jplayer);

        //sends the statistics of the current player
        GameObjectList stats = GameWorld.Find("StatFields") as GameObjectList;
        PlayerStatField statField = stats.Find(GameEnvironment.SelectedClass) as PlayerStatField;
        string jstats = JsonConvert.SerializeObject(statField);
        GameEnvironment.Connection.Send(jstats);

        /*
        //sends all in game enemies
        GameObjectList enemies = GameWorld.Find("enemies") as GameObjectList;
        foreach(EnemyObject enemy in enemies.Children)
        {
            string jenemy = JsonConvert.SerializeObject(enemy);
            GameEnvironment.Connection.Send(jenemy);
        }*/

        //sends all in game food
        GameObjectList foods = GameWorld.Find("food") as GameObjectList;
        foreach (Food food in foods.Children)
        {
            string jfood = JsonConvert.SerializeObject(food);
            GameEnvironment.Connection.Send(jfood);
        }
        /*
        //sends all in game keys
        GameObjectList keys = GameWorld.Find("keys") as GameObjectList;
        foreach (Key key in keys.Children)
        {
            string jkey = JsonConvert.SerializeObject(key);
            GameEnvironment.Connection.Send(jkey);
        }*/
        /*
        //sends all in game treasures
        GameObjectList treasures = GameWorld.Find("treasures") as GameObjectList;
        foreach (Treasure treasure in treasures.Children)
        {
            string jtreasure = JsonConvert.SerializeObject(treasure);
            GameEnvironment.Connection.Send(jtreasure);
        }*/
        /*
        //sends all in game potions
        GameObjectList potions = GameWorld.Find("potions") as GameObjectList;
        foreach (Potion potion in potions.Children)
        {
            string jpotion = JsonConvert.SerializeObject(potion);
            GameEnvironment.Connection.Send(jpotion);
        }*/

        //sends all in game player shots
        GameObjectList playershots = GameWorld.Find("playershot") as GameObjectList;
        foreach (PlayerShot playerShot in playershots.Children)
        {
            string jplayershot = JsonConvert.SerializeObject(playerShot);
            GameEnvironment.Connection.Send(jplayershot);
        }

        //sends all in game player shots
        /*GameObjectList enemieShots = GameWorld.Find("enemieShot") as GameObjectList;
        foreach ( in playershots.Children)
        {
            string jplayershot = JsonConvert.SerializeObject(playerShot);
            GameEnvironment.Connection.Send(jplayershot);
        }*/

        //sends current states of the doors
        GameObjectList doors = GameWorld.Find("Doors") as GameObjectList;
        foreach (Door door in doors.Children)
        {
            string jdoor = JsonConvert.SerializeObject(door);
            GameEnvironment.Connection.Send(jdoor);
        }

        //sends current states of the breakable walls
        GameObjectList breakableWalls = GameWorld.Find("BreakableWalls") as GameObjectList;
        foreach (BreakableWall breakableWall in breakableWalls.Children)
        {
            string jwall = JsonConvert.SerializeObject(breakableWall);
            GameEnvironment.Connection.Send(jwall);
        }
    }

    public void UpdateMultiplayer(string message)
    {
        Console.WriteLine(message);
        if(message.StartsWith(""))
        {
            //... ... = JsonConvert.DeserializeObject<...>(message);
        }
        else if(message.StartsWith(""))
        {

        }
        else if (message.StartsWith(""))
        {

        }
        else if (message.StartsWith(""))
        {

        }
        else if (message.StartsWith(""))
        {

        }
        else if (message.StartsWith(""))
        {

        }
        else if (message.StartsWith(""))
        {

        }
    }

}
