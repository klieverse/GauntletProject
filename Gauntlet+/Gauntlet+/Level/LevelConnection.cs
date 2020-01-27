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
        GameObjectList playershots = Find("playershot") as GameObjectList;
        foreach (PlayerShot playerShot in playershots.Children)
        {
            if(playerShot.Id == GameEnvironment.SelectedClass && playerShot.Visible)
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

        //if the chosen player is elf it sends all created enemies to the server
        if(GameEnvironment.SelectedClass == "Elf")
        {
            GameObjectList enemies = GameWorld.Find("enemies") as GameObjectList;
            foreach (EnemyObject enemy in enemies.Children)
            {
                if (!enemy.Sent)
                {
                    enemy.Sent = true;
                    sprite = enemy.Sprite;
                    enemy.SetSprite(null);
                    string jenemy = JsonConvert.SerializeObject(enemy);
                    GameEnvironment.Connection.Send(jenemy);
                    enemy.SetSprite(sprite);
                }
            }
        }
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
                sprite.Mirror = newPlayer.isMirror;
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Wizard") && GameEnvironment.SelectedClass != "Wizard")
            {
                Player player = players.Find("Wizard") as Player;
                SpriteSheet sprite = player.Sprite;
                Merlin newPlayer = JsonConvert.DeserializeObject<Merlin>(message);
                sprite.Mirror = newPlayer.isMirror;
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Warrior") && GameEnvironment.SelectedClass != "Warrior")
            {
                Player player = players.Find("Warrior") as Player;
                SpriteSheet sprite = player.Sprite;
                Thor newPlayer = JsonConvert.DeserializeObject<Thor>(message);
                sprite.Mirror = newPlayer.isMirror;
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
            else if (message.Contains("Valkery") && GameEnvironment.SelectedClass != "Valkery")
            {
                Player player = players.Find("Valkery") as Player;
                SpriteSheet sprite = player.Sprite;
                Thyra newPlayer = JsonConvert.DeserializeObject<Thyra>(message);
                sprite.Mirror = newPlayer.isMirror;
                PlayerUpdate(player, newPlayer);
                player.SetSprite(sprite);
            }
        }
        

        //enemies updated
        if(message.Contains("Sent") && GameEnvironment.SelectedClass != "Elf")
        {
            GameObjectList enemies = Find("enemies") as GameObjectList;
            if (message.Contains("Troll"))
            {
                Troll enemy = JsonConvert.DeserializeObject<Troll>(message);
                enemies.Add(enemy);
            }
            else if (message.Contains("Ghost"))
            {
                Ghost enemy = JsonConvert.DeserializeObject<Ghost>(message);
                enemies.Add(enemy);
            }
            else if (message.Contains("Gnome"))
            {
                Gnome enemy = JsonConvert.DeserializeObject<Gnome>(message);
                enemies.Add(enemy);
            }
            else if (message.Contains("Hellhound"))
            {
                Hellhound enemy = JsonConvert.DeserializeObject<Hellhound>(message);
                enemies.Add(enemy);
            }
            else if (message.Contains("Thief"))
            {
                Thief enemy = JsonConvert.DeserializeObject<Thief>(message);
                enemies.Add(enemy);
            }
            else if (message.Contains("Wizard"))
            {
                Wizard enemy = JsonConvert.DeserializeObject<Wizard>(message);
                enemies.Add(enemy);
            }
        }

        //playershots updated
        if(message.Contains("IsShot"))
        {
            PlayerShot shot = JsonConvert.DeserializeObject<PlayerShot>(message);
            if(shot.Id != GameEnvironment.SelectedClass && shot.Visible)
            {
                PlayerShotUpdate(shot);
            }
            if(!shot.Visible)
            {
                //DeletePlayerShot(shot);
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
        orgP.Sprite.Mirror = newP.isMirror;
        orgP.Position = newP.Position;
        orgP.Velocity = newP.Velocity;
        orgP.Visible = newP.Visible;
    }

    public void PlayerShotUpdate(PlayerShot shot)
    {
        GameObjectList playershots = Find("playershot") as GameObjectList;
        foreach (PlayerShot playerShot in playershots.Children)
        {
            if (playerShot.shotCount == shot.shotCount && playerShot.Id == shot.Id)
            {
                return;
            }
        }
        SpriteSheet shotsprite = new SpriteSheet(shot.Id + "Shot");
        shot.SetSprite(shotsprite);
        GameObjectList players = GameWorld.Find("players") as GameObjectList;
        Player player = players.Find(shot.Id) as Player;
        shot.player = player;
        shot.HandleIncoming();
        playershots.Add(shot);
    }
    /*
    public void DeletePlayerShot(PlayerShot shot)
    {
        GameObjectList playershots = GameWorld.Find("playershot") as GameObjectList;
        foreach (PlayerShot playerShot in playershots.Children)
        {
            if (playerShot.shotCount == shot.shotCount && playerShot.Id == shot.Id)
            {
                playershots.Remove(playerShot);
                return;
            }
        }
    }*/
}

