using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;


partial class Level : GameObjectList
{

    public override void HandleInput(InputHelper inputHelper)
    {
        if (GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
        {
            for (int i = children.Count - 1; i >= 0; i--)
            {
                if(children[i].Id != "players")
                {
                    children[i].HandleInput(inputHelper);
                }
            }
            foreach (Player player in (Find("players") as GameObjectList).Children)
            {
                if (player.Id == GameEnvironment.SelectedClass)
                {
                    player.HandleInput(inputHelper);
                }
            }
        }
        else
        {
            base.HandleInput(inputHelper);
        }
        if (quitButton.Pressed)
        {
            Reset();
            //Find(GameEnvironment.SelectedClass).Visible = false;
            IGameLoopObject playingState = GameEnvironment.GameStateManager.GetGameState("playingState");
            playingState.Reset();
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }
    }

    public override void Update(GameTime gameTime)
    {
        if(GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
        {
            foreach (GameObject obj in children)
            {
                if(obj.Id != "players")
                {
                    obj.Update(gameTime);
                }
            }
            foreach (Player player in (Find("players") as GameObjectList).Children)
            {
                if (player.Id == GameEnvironment.SelectedClass)
                {
                    player.Update(gameTime);
                }


            }
        }
        else
        {
            base.Update(gameTime);
        }
        LoadPlayer();



        //Player player = Find("player") as Player;

        // check if we died


        //update the fitting statfield with the data of the current player
        foreach (Player player in (Find("players") as GameObjectList).Children)
        {
            if(player.Id == GameEnvironment.SelectedClass)
            {
                (Find(player.playerClass + "Stats") as PlayerStatField).Update(player);
            }

            
        }



    }

    public void LoadPlayer()
    {
        string playerClass= GameEnvironment.SelectedClass; //Get the selected class
        // Check if the selected class is loaded, if not load the right player into the level
        //if multiplayer is chosen, all characters will be loaded
        GameObjectList list = Find("players") as GameObjectList;
        if (list.Find(playerClass) == null)
        {
            Console.WriteLine("Hij komt hier");
            if (playerClass == "Elf" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                //get the startPosition for the player
                Vector2 startPosition = new Vector2(((float)startPositionQuestor.X + 0.5f) * Tile.Size, (startPositionQuestor.Y + 1) * Tile.Size);
                Questor questor = new Questor(4, "Elf", startPosition, this, true); //create the player
                (Find("players") as GameObjectList).Add(questor); //add player to level
                int statPosition = GameEnvironment.Screen.X / 2 + 272;//get position for the statfield
                PlayerStatField questorStats = new PlayerStatField("Elf", statPosition); //create the players Statfield
                (Find("StatFields") as GameObjectList).Add(questorStats); //add the statfield to the level
            }
            if(playerClass == "Wizard" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                Vector2 startPosition = new Vector2(((float)startPositionMerlin.X + 0.5f) * Tile.Size, (startPositionMerlin.Y + 1f) * Tile.Size);
                Merlin merlin = new Merlin(4, "Wizard", startPosition, this, true);
                (Find("players") as GameObjectList).Add(merlin);
                int statPosition = GameEnvironment.Screen.X / 2;
                PlayerStatField wizardStats = new PlayerStatField("Wizard", statPosition);
                (Find("StatFields") as GameObjectList).Add(wizardStats);
            }
            if(playerClass =="Warrior" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                Vector2 startPosition = new Vector2(((float)startPositionThor.X) * Tile.Size, (startPositionThor.Y) * Tile.Size);
                Thor thor = new Thor(4, "Warrior", startPosition, this, true);
                (Find("players") as GameObjectList).Add(thor);
                int statPosition = GameEnvironment.Screen.X / 2 - 2* 272;
                PlayerStatField warriorStats = new PlayerStatField("Warrior", statPosition);
                (Find("StatFields") as GameObjectList).Add(warriorStats);
            }
            if(playerClass == "Valkery" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                Vector2 startPosition = new Vector2((startPositionThyra.X + 2) * Tile.Size, (startPositionThyra.Y + 2) * Tile.Size);
                Thyra thyra = new Thyra(4, "Valkery", startPosition, this, true);
                (Find("players") as GameObjectList).Add(thyra);
                int statPosition = GameEnvironment.Screen.X / 2 - 272;
                PlayerStatField valkeryStats = new PlayerStatField("Valkery", statPosition);
                (Find("StatFields") as GameObjectList).Add(valkeryStats);

            }
            else
            {
               (Find(playerClass) as Player).Visible = true;
            }
        }
    }

    public override void Reset()
    {
        base.Reset();
    }

    public void ResetLevel()
    {
        foreach (GameObject obj in children)
        {
            if(obj.Id != "players" && obj.Id != "StatFields" )
            {
                Console.WriteLine(obj.Id);
                obj.Reset();
            }
            
        }
    }
    
    public void SendInformation()
    {   
        //sends the player class
        GameObjectList players = GameWorld.Find("players") as GameObjectList;
        Player player = players.Find(GameEnvironment.SelectedClass) as Player;
        SpriteSheet sprite = player.Sprite;
        player.SetSprite(null);
        string jplayer = JsonConvert.SerializeObject(player);
        GameEnvironment.Connection.Send(jplayer);
        player.SetSprite(sprite);

        //sends the statistics of the current player
        GameObjectList stats = GameWorld.Find("StatFields") as GameObjectList;
        PlayerStatField statField = stats.Find(GameEnvironment.SelectedClass) as PlayerStatField;
        string jstats = JsonConvert.SerializeObject(statField);
        GameEnvironment.Connection.Send(jstats);

        //sends all in game enemies
        GameObjectList enemies = GameWorld.Find("enemies") as GameObjectList;
        foreach(EnemyObject enemy in enemies.Children)
        {
            sprite = enemy.Sprite;
            enemy.SetSprite(null);
            string jenemy = JsonConvert.SerializeObject(enemy);
            GameEnvironment.Connection.Send(jenemy);
            enemy.SetSprite(sprite);
        }
        

        //sends all in game food
        GameObjectList foods = GameWorld.Find("food") as GameObjectList;
        foreach (Food food in foods.Children)
        {
            sprite = food.Sprite;
            food.SetSprite(null);
            string jfood = JsonConvert.SerializeObject(food);
            GameEnvironment.Connection.Send(jfood);
            food.SetSprite(sprite);
        }
        

        //sends all in game keys
        GameObjectList keys = GameWorld.Find("keys") as GameObjectList;
        foreach (Key key in keys.Children)
        {
            sprite = key.Sprite;
            key.SetSprite(null);
            string jkey = JsonConvert.SerializeObject(key);
            GameEnvironment.Connection.Send(jkey);
            key.SetSprite(sprite);
        }
        
        //sends all in game treasures
        GameObjectList treasures = GameWorld.Find("treasures") as GameObjectList;
        foreach (Treasure treasure in treasures.Children)
        {
            sprite = treasure.Sprite;
            treasure.SetSprite(null);
            string jtreasure = JsonConvert.SerializeObject(treasure);
            GameEnvironment.Connection.Send(jtreasure);
            treasure.SetSprite(sprite);
        }
        
        //sends all in game potions
        GameObjectList potions = GameWorld.Find("potions") as GameObjectList;
        foreach (Potion potion in potions.Children)
        {
            sprite = potion.Sprite;
            potion.SetSprite(null);
            string jpotion = JsonConvert.SerializeObject(potion);
            GameEnvironment.Connection.Send(jpotion);
            potion.SetSprite(sprite);
        }
        
        //sends all in game player shots
        GameObjectList playershots = GameWorld.Find("playershot") as GameObjectList;
        foreach (PlayerShot playerShot in playershots.Children)
        {
            sprite = playerShot.Sprite;
            playerShot.SetSprite(null);
            string jplayershot = JsonConvert.SerializeObject(playerShot);
            GameEnvironment.Connection.Send(jplayershot);
            playerShot.SetSprite(sprite);
        }

        //sends all in game enemy shots
        /*
        GameObjectList enemieShots = GameWorld.Find("enemieShot") as GameObjectList;
        foreach ( in playershots.Children)
        {
            string jplayershot = JsonConvert.SerializeObject(playerShot);
            GameEnvironment.Connection.Send(jplayershot);
        }*/

        //sends current states of the doors
        GameObjectList doors = GameWorld.Find("Doors") as GameObjectList;
        foreach (Door door in doors.Children)
        {
            sprite = door.Sprite;
            door.SetSprite(null);
            string jdoor = JsonConvert.SerializeObject(door);
            GameEnvironment.Connection.Send(jdoor);
            door.SetSprite(sprite);
        }

        //sends current states of the breakable walls
        GameObjectList breakableWalls = GameWorld.Find("BreakableWalls") as GameObjectList;
        foreach (BreakableWall breakableWall in breakableWalls.Children)
        {
            sprite = breakableWall.Sprite;
            breakableWall.SetSprite(null);
            string jwall = JsonConvert.SerializeObject(breakableWall);
            GameEnvironment.Connection.Send(jwall);
            breakableWall.SetSprite(sprite);
        }
    }

    public void UpdateMultiplayer(string message)
    {
        //Player classes updated
        if(message.Contains("playerClass"))
        {
            GameObjectList players = GameWorld.Find("players") as GameObjectList;
            if(message.Contains("Elf"))
            {
                Player player = players.Find("Elf") as Player;
                SpriteSheet sprite = player.Sprite;
                player = JsonConvert.DeserializeObject<Player>(message);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Wizard"))
            {
                Player player = players.Find("Wizard") as Player;
                SpriteSheet sprite = player.Sprite;
                player = JsonConvert.DeserializeObject<Player>(message);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Warrior"))
            {
                Player player = players.Find("Warrior") as Player;
                SpriteSheet sprite = player.Sprite;
                player = JsonConvert.DeserializeObject<Player>(message);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Valkery"))
            {
                Player player = players.Find("Valkery") as Player;
                SpriteSheet sprite = player.Sprite;
                player = JsonConvert.DeserializeObject<Player>(message);
                player.SetSprite(sprite);
            }
        }

        //Keys updated
        else if(message.Contains("Key"))
        {
            Key key = JsonConvert.DeserializeObject<Key>(message);
            GameObjectList keys = GameWorld.Find("keys") as GameObjectList;
            foreach(Key k in keys.Children)
            {
                //only change if visibility of new key is false
                if(key.Position == k.Position && key.Visible == false)
                {
                    k.Visible = key.Visible;
                }
            }
        }
        //treasure updated
        else if (message.Contains("treasure"))
        {
            Treasure treasure = JsonConvert.DeserializeObject<Treasure>(message);
            GameObjectList treasures = GameWorld.Find("treasures") as GameObjectList;
            foreach (Treasure t in treasures.Children)
            {
                //only change if visibility of new treasure is false
                if (treasure.Position == t.Position && treasure.Visible == false)
                {
                    t.Visible = treasure.Visible;
                }
            }
        }

        //potions updated
        else if (message.Contains("NormalPotion") || message.Contains("OrangePotion"))
        {
            Potion potion = JsonConvert.DeserializeObject<Potion>(message);
            GameObjectList potions = GameWorld.Find("potions") as GameObjectList;
            foreach (Potion p in potions.Children)
            {
                //only change if visibility of new potion is false
                if (potion.Position == p.Position && potion.Visible == false)
                {
                    p.Visible = potion.Visible;
                }
            }
        }

        //Food updated
        else if (message.Contains("Food"))
        {
            Food food = JsonConvert.DeserializeObject<Food>(message);
            GameObjectList foods = GameWorld.Find("food") as GameObjectList;
            foreach (Food f in foods.Children)
            {
                //only change if visibility of incoming food is false
                if (food.Position == f.Position && food.Visible == false)
                {
                    f.Visible = food.Visible;
                }
            }
        }
        //statfield updated

        //enemies updated

        //playershots updated

        //doors updated

        //breakable walls updated

        //enemy shots updated



    }

}
