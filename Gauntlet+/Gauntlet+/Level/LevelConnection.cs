using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;


partial class Level : GameObjectList
{
    Player player;
    string jplayer;

    public void SendInformation()
    {
        //sends the player class
        GameObjectList players = GameWorld.Find("players") as GameObjectList;
        Player player = players.Find(GameEnvironment.SelectedClass) as Player;
        SpriteSheet sprite = player.Sprite;
        player.SetSprite(null);
        jplayer = JsonConvert.SerializeObject(player);
        GameEnvironment.Connection.Send(jplayer);
        player.SetSprite(sprite);
        /*
        
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
            if(message == jplayer)
            {
                return;
            }
            GameObjectList players = GameWorld.Find("players") as GameObjectList;
            if (message.Contains("Elf"))
            {
                try
                {
                    player = players.Find("Elf") as Player;
                }
                catch
                {

                }
                SpriteSheet sprite = player.Sprite;
                Questor newPlayer = JsonConvert.DeserializeObject<Questor>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Wizard"))
            {
                try
                {
                    player = players.Find("Wizard") as Player;
                }
                catch
                {

                }
                SpriteSheet sprite = player.Sprite;
                Merlin newPlayer = JsonConvert.DeserializeObject<Merlin>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Warrior"))
            {
                try
                {
                    player = players.Find("Warrior") as Player;
                }
                catch
                {

                }
                Player player = players.Find("Warrior") as Player;
                SpriteSheet sprite = player.Sprite;
                Thor newPlayer = JsonConvert.DeserializeObject<Thor>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Valkery"))
            {
                try
                {
                    player = players.Find("Valkery") as Player;
                }
                catch
                {
                    //player add
                }
                SpriteSheet sprite = player.Sprite;
                Thyra newPlayer = JsonConvert.DeserializeObject<Thyra>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
        }
        /*
        
        

        //enemies updated

        //playershots updated
        
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

