using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        //sends all in game enemies
        GameObjectList enemies = GameWorld.Find("enemies") as GameObjectList;
        foreach (EnemyObject enemy in enemies.Children)
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
        
        //statfield updated

        //enemies updated

        //playershots updated

        //doors updated

        //breakable walls updated

        //enemy shots updated



    }
}

