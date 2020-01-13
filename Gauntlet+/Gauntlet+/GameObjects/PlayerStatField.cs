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
    Vector2 StaticPosition;
    public PlayerStatField(string playerClass,int layer = 5) :base("statSprites/" + playerClass + "Stats",layer,"StatField")
    {
        id = playerClass + "Stats";
        Stats = new PlayerStats(playerClass);
        StaticPosition = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y - Height);
        position = StaticPosition;
    }

    public void Update(Player player)
    {
        Stats.Update(player);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
        {
            return;
        }
        if (GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("playingState"))
        {
            sprite.Draw(spriteBatch, this.GlobalPosition , rotation, origin);
        }
        else
        {
            sprite.Draw(spriteBatch, this.GlobalPosition, rotation, origin);
        }
        Stats.Draw(gameTime, spriteBatch);
    }
}

