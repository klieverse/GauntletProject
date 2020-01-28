using Microsoft.Xna.Framework;
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
        SpriteGameObject hintFrame = new SpriteGameObject("Sprites/spr_frame", 1);
        hintField.Position = new Vector2((GameEnvironment.Screen.X - hintFrame.Width) / 2, 10);
        hintField.Add(hintFrame);
        TextGameObject hintText = new TextGameObject("StatFont", 2);
        hintText.Text = "Secret potion found! more " + id + " added to stat";
        hintText.Position = new Vector2(120, 25);
        hintText.Color = Color.Black;
        hintField.Add(hintText);
        VisibilityTimer hintTimer = new VisibilityTimer(hintField, 1, id + "Timer");
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
                if (CollidesWith(player))
                {
                    visible = false;
                    GameEnvironment.AssetManager.PlaySound("Key", position.X);
                    player.AddPotion(PotType);
                    if(PotType != PotionType.Normal && PotType != PotionType.Orange)
                    {
                        DisplayMessage();
                    }
                }
    }

    void DisplayMessage()
    {
        VisibilityTimer hintTimer = level.Find(Id + "Timer") as VisibilityTimer;
        hintTimer.StartVisible();
    }

    public PotionType PotType
    {
        get;

    }
}
