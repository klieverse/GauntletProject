using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

enum PotionType
{
    Normal,
    Orange,
    Armor,
    Magic,
    ShotPower,
    ShotSpeed,
    Speed,
    Melee
}

class Potion : Item
{
    Level level;

    public Potion(PotionType pot, int layer, string id, Vector2 position, Level level)
        : base(layer, id, position)
    {
        PotType = pot;
        this.level = level;
        GameObjectList hintField = new GameObjectList(100);
        level.Add(hintField);
        SpriteGameObject hintFrame = new SpriteGameObject("Sprites/spr_frame", 100);
        hintField.Position = new Vector2((GameEnvironment.Screen.X - hintFrame.Width) / 2, 10);
        hintField.Add(hintFrame);
        TextGameObject hintText = new TextGameObject("StatFont", 101);
        hintText.Text = "Secret potion found! more " + id + " added to stat";
        hintText.Position = new Vector2(120, 25);
        hintText.Color = Color.Black;
        hintField.Add(hintText);
        VisibilityTimer hintTimer = new VisibilityTimer(hintField, 90, id + "Timer");
        level.Add(hintTimer);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        CheckPlayerCollision();
    }

    private void CheckPlayerCollision()
    {
        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player) && PotType != PotionType.Normal && PotType != PotionType.Orange)
                {
                    visible = false;
                    GameEnvironment.AssetManager.PlaySound("Key", position.X);
                    player.AddPotion(PotType);
                    DisplayMessage();
                }
                else if (CollidesWith(player) && !player.InventoryFull)
                {
                    visible = false;
                    GameEnvironment.AssetManager.PlaySound("Key", position.X);
                    player.AddPotion(PotType);
                }
    }

    void DisplayMessage()
    {
        VisibilityTimer hintTimer = level.Find(Id + "Timer") as VisibilityTimer;
        hintTimer.StartVisible();
    }

    /*public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(PotType != PotionType.Normal && PotType != PotionType.Orange)
        {
            sprite.Draw(spriteBatch, this.GlobalPosition, rotation, origin, scale, color);
        }
        else base.Draw(gameTime, spriteBatch);
    }*/

    public PotionType PotType
    {
        get;

    }
}
