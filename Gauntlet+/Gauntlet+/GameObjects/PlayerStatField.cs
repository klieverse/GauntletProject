using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayerStatField : SpriteGameObject
{
    PlayerStats Stats;
    public PlayerStatField(string playerClass, int layer = 5) :base("statSprites/" + playerClass + "Stats",layer,"StatField")
    {
        id = playerClass + "Stats"; //set the id based on the playerclass
        SetPosition();
        Stats = new PlayerStats(position,playerClass); // create the statlist for in the field
        
    }

    public void Update(Player player)
    {
        //if(player.Id == GameEnvironment.SelectedClass)
        {
            Stats.Update(player); //update the statlist based op the given player
        }
        
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //draw the object without ajusting to the camera
        if (!visible || sprite == null)
        {
            return;
        }
        if (GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("playingState"))
        {
            sprite.Draw(spriteBatch, this.GlobalPosition , rotation, origin, scale, color);
        }
        else
        {
            sprite.Draw(spriteBatch, this.GlobalPosition, rotation, origin, scale, color);
        }
        //draw the stats into the field
        Stats.Draw(gameTime, spriteBatch);
    }

    public void SetPosition()
    {
        if (id == "ElfStats")
            position = new Vector2(GameEnvironment.Screen.X / 2 + 272 , GameEnvironment.Screen.Y - Height);
        if (id == "WizardStats")
            position = new Vector2(GameEnvironment.Screen.X / 2 , GameEnvironment.Screen.Y - Height);
        if (id == "WarriorStats")
            position = new Vector2(GameEnvironment.Screen.X / 2 - 2 * 272, GameEnvironment.Screen.Y - Height);
        if (id == "ValkeryStats")
            position = new Vector2(GameEnvironment.Screen.X / 2 - 272, GameEnvironment.Screen.Y - Height);

    }
}

