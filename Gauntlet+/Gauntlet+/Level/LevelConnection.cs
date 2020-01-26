using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


partial class Level : GameObjectList
{
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
        foreach (EnemyObject enemy in enemies.Children)
        {
            sprite = enemy.Sprite;
            enemy.SetSprite(null);
            string jenemy = JsonConvert.SerializeObject(enemy);
            GameEnvironment.Connection.Send(jenemy);
            enemy.SetSprite(sprite);
        }*/
        
        //sends all in game player shots
        GameObjectList playershots = GameWorld.Find("playershot") as GameObjectList;
        foreach (PlayerShot playerShot in playershots.Children)
        {
            if(playerShot.Id == GameEnvironment.SelectedClass)
            {
                sprite = playerShot.Sprite;
                Player _player = playerShot.player;
                playerShot.player = null;
                playerShot.SetSprite(null);
                string jplayershot = JsonConvert.SerializeObject(playerShot);
                GameEnvironment.Connection.Send(jplayershot);
                playerShot.SetSprite(sprite);
                playerShot.player = _player;
            }
            
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
            if (message.Contains("Elf") && GameEnvironment.SelectedClass != "Elf")
            {
                Player player = players.Find("Elf") as Player;
                SpriteSheet sprite = player.Sprite;
                Questor newPlayer = JsonConvert.DeserializeObject<Questor>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);

                player.SetSprite(sprite);
            }
            else if (message.Contains("Wizard") && GameEnvironment.SelectedClass != "Wizard")
            {
                Player player = players.Find("Wizard") as Player;
                SpriteSheet sprite = player.Sprite;
                Merlin newPlayer = JsonConvert.DeserializeObject<Merlin>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Warrior") && GameEnvironment.SelectedClass != "Warrior")
            {
                Player player = players.Find("Warrior") as Player;
                SpriteSheet sprite = player.Sprite;
                Thor newPlayer = JsonConvert.DeserializeObject<Thor>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Valkery") && GameEnvironment.SelectedClass != "Valkery")
            {
                Player player = players.Find("Valkery") as Player;
                SpriteSheet sprite = player.Sprite;
                Thyra newPlayer = JsonConvert.DeserializeObject<Thyra>(message);
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
        }
        

        //enemies updated


        //playershots updated
        if(message.Contains("IsShot"))
        {
            Console.WriteLine(message);
            PlayerShot shot = JsonConvert.DeserializeObject<PlayerShot>(message);
            if(shot.Id != GameEnvironment.SelectedClass && shot.Visible == true)
            {
                SpriteSheet shotsprite = new SpriteSheet(shot.Id + "Shot");
                shot.SetSprite(shotsprite);
                GameObjectList players = GameWorld.Find("players") as GameObjectList;
                Player player = players.Find(shot.Id) as Player;
                shot.player = player;
                shot.HandleIncoming();
                GameObjectList playershots = GameWorld.Find("playershot") as GameObjectList;
                playershots.Add(shot);
            }
        }

        //enemy shots updated



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

