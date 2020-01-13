using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PlayerStats : TextGameObject
{
    string playerClass;
    Vector2 staticPosition;
    public PlayerStats(string playerClass, int layer = 6):base("StatFont",layer)
    {
        this.playerClass = playerClass;
        staticPosition = new Vector2(GameEnvironment.Screen.X / 2+50, GameEnvironment.Screen.Y -140);
        position = staticPosition;
        text = playerClass;
    }

    public void Update(Player player)
    {
        int score = 100;
        int health = player.health;
        int potions = player.potions;
        int keys = player.keys;


        text = playerClass + "\n \n " +
               "Score: " + score +
            "\n health:" + health +
            "\n Potions:" + potions +
            "\n Keys"+ keys;
    }
}
