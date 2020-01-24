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
        //get the data from the player
        int score = player.Score;
        int health = player.Health;
        int potions = player.Potions;
        int keys = player.Key;

        int value1 = PlayingState.CurrentLevel.secretValue1;
        int value2 = PlayingState.CurrentLevel.secretValue2;
        int value3 = PlayingState.CurrentLevel.goalSecretValue1;
        int value4 = PlayingState.CurrentLevel.goalSecretValue2;
        //set data into a text format
        text = 
              " Score: " + score +
            "\n Health:" + health +
            "\n Potions:" + potions +
            "\n Keys:"+ keys
            + value1+value2+value3+value4;
    }
}
