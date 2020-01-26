using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;


partial class Level : GameObjectList
{
    Player _player;
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
                    _player = players.Find("Elf") as Player;
                }
                catch
                {
                    Vector2 startPosition = new Vector2(((float)startPositionQuestor.X + 0.5f) * Tile.Size, (startPositionQuestor.Y + 1) * Tile.Size);
                    Questor questor = new Questor(4, "Elf", startPosition, this, true); //create the player
                    (Find("players") as GameObjectList).Add(questor); //add player to level
                    PlayerStatField questorStats = new PlayerStatField("Elf"); //create the players Statfield
                    (Find("StatFields") as GameObjectList).Add(questorStats);
                }
                SpriteSheet sprite = _player.Sprite;
                Questor newPlayer = JsonConvert.DeserializeObject<Questor>(message);
                PlayerUpdate(_player, newPlayer);
                _player.SetSprite(sprite);
            }
            else if (message.Contains("Wizard"))
            {
                try
                {
                    _player = players.Find("Wizard") as Player;
                }
                catch
                {
                    Vector2 startPosition = new Vector2(((float)startPositionMerlin.X + 0.5f) * Tile.Size, (startPositionMerlin.Y + 1f) * Tile.Size);
                    Merlin merlin = new Merlin(4, "Wizard", startPosition, this, true);
                    (Find("players") as GameObjectList).Add(merlin);
                    PlayerStatField wizardStats = new PlayerStatField("Wizard");
                    (Find("StatFields") as GameObjectList).Add(wizardStats);
                }
                SpriteSheet sprite = _player.Sprite;
                Merlin newPlayer = JsonConvert.DeserializeObject<Merlin>(message);
                PlayerUpdate(_player, newPlayer);
                _player.SetSprite(sprite);
            }
            else if (message.Contains("Warrior"))
            {
                try
                {
                    _player = players.Find("Warrior") as Player;
                }
                catch
                {
                    Vector2 startPosition = new Vector2(((float)startPositionThor.X) * Tile.Size, (startPositionThor.Y) * Tile.Size);
                    Thor thor = new Thor(4, "Warrior", startPosition, this, true);
                    (Find("players") as GameObjectList).Add(thor);
                    PlayerStatField warriorStats = new PlayerStatField("Warrior");
                    (Find("StatFields") as GameObjectList).Add(warriorStats);
                }
                _player = players.Find("Warrior") as Player;
                SpriteSheet sprite = _player.Sprite;
                Thor newPlayer = JsonConvert.DeserializeObject<Thor>(message);
                PlayerUpdate(_player, newPlayer);
                _player.SetSprite(sprite);
            }
            else if (message.Contains("Valkery"))
            {
                try
                {
                    _player = players.Find("Valkery") as Player;
                }
                catch
                {
                    Vector2 startPosition = new Vector2((startPositionThyra.X + 2) * Tile.Size, (startPositionThyra.Y + 2) * Tile.Size);
                    Thyra thyra = new Thyra(4, "Valkery", startPosition, this, true);
                    (Find("players") as GameObjectList).Add(thyra);
                    PlayerStatField valkeryStats = new PlayerStatField("Valkery");
                    (Find("StatFields") as GameObjectList).Add(valkeryStats);
                }
                SpriteSheet sprite = _player.Sprite;
                Thyra newPlayer = JsonConvert.DeserializeObject<Thyra>(message);
                PlayerUpdate(_player, newPlayer);
                _player.SetSprite(sprite);
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

