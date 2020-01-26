using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;


partial class Level : GameObjectList
{
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
        /*
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
        }

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
        }*/
    }

    public void UpdateMultiplayer(string message)
    {
        //Player classes updated
        if (message.Contains("playerClass"))
        {
            GameObjectList players = GameWorld.Find("players") as GameObjectList;
            if (message.Contains("Elf"))
            {
                Player player = players.Find("Elf") as Player;
                SpriteSheet sprite = player.Sprite;
                Questor newPlayer = JsonConvert.DeserializeObject<Questor>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Wizard"))
            {
                Player player = players.Find("Wizard") as Player;
                SpriteSheet sprite = player.Sprite;
                Merlin newPlayer = JsonConvert.DeserializeObject<Merlin>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Warrior"))
            {
                Player player = players.Find("Warrior") as Player;
                SpriteSheet sprite = player.Sprite;
                Thor newPlayer = JsonConvert.DeserializeObject<Thor>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Valkery"))
            {
                Player player = players.Find("Valkery") as Player;
                SpriteSheet sprite = player.Sprite;
                Thyra newPlayer = JsonConvert.DeserializeObject<Thyra>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
                //Console.WriteLine("HIER KOMT VALKERY BINNEN: " + message);
            }
        }
        /*
        //Keys updated
        else if(message.Contains("key"))
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
        */


    }

    public void PlayerUpdate(Player orgP, Player newP)
    {
        orgP.health = newP.health;
        orgP.keys = newP.keys;
        orgP.potions = newP.Potions;
        orgP.score = newP.score;
        orgP.Mirror = newP.Mirror;
        orgP.Position = newP.Position;
        orgP.Velocity = newP.Velocity;
        orgP.Visible = newP.Visible;
    }
}
}

