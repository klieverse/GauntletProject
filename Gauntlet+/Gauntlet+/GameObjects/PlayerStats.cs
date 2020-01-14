using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PlayerStats : TextGameObject
{
    
    Vector2 staticPosition;
    public PlayerStats( Vector2 fieldPosition, string playerClass, int layer = 6):base("StatFont",layer)
    {
        
        position = fieldPosition + new Vector2(20, +40); //set positon 
        //set base text
        text = "Score: 0 " +
            "\n Health: 600" +
            "\n Potions: 0" +
            "\n Key: 0";
    }

    public void Update(Player player)
    {
        //get the date from the player
        int score = 100;
        int health = player.Health;
        int potions = player.Potions;
        int keys = player.Key;

        //set data into a text format
        text = 
              " Score: " + score +
            "\n Health:" + health +
            "\n Potions:" + potions +
            "\n Keys"+ keys;
    }
}
