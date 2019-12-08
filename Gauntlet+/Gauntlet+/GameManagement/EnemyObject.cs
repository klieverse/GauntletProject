using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

public class EnemyObject : AnimatedGameObject
{
    protected float speedVert;
    protected float speedHori;
    protected bool isHellhound;
    protected bool isGnome;
    protected bool isTroll;
    protected bool isDeath;
    protected bool isGhost;
    protected bool isWizard;
    protected bool isThief;
    protected int health = 30;
    protected int strength;
    protected int speed = 250;
    TileField tileField;
    
    public EnemyObject(int layer, string id, bool isHellhound, bool isGnome, bool isTroll, bool isDeath, bool isGhost, bool isWizard, bool isThief) : base(layer, id)
    {
        LoadAnimation(id, id, true);
        PlayAnimation(id);

        this.isHellhound = isHellhound;
        this.isGnome = isGnome;
        this.isTroll = isTroll;
        this.isDeath = isDeath;
        this.isGhost = isGhost;
        this.isWizard = isWizard;
        this.isThief = isThief;
        
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

//        Player player = GameWorld.Find("Player") as Player;

        float opposite = Player.playerPosition.Y  - position.Y + 55;
        float adjacent = Player.playerPosition.X  - position.X + 30;
        float vertical = (float)Math.Atan2(opposite, adjacent);
        float horizontal = (float)Math.Atan2(adjacent, opposite);
        speedVert = (float)Math.Sin(vertical) * speed;
        speedHori = (float)Math.Sin(horizontal) * speed;
        velocity.Y = speedVert;
        velocity.X = speedHori;

    }

    public bool CollidesWithObject()
    {
 
        //check wall collision
        Tile tile = tileField.Get(1, 1) as Tile;
        int Left = (int)(position.X / tile.Width);
        int Right = (int)((position.X + Width) / tile.Width);
        int Top = (int)(position.Y / tile.Height);
        int Bottom = (int)((position.Y + Height) / tile.Height);

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
                if (tileField.GetTileType(x, y) == TileType.Wall || tileField.GetTileType(x, y) == TileType.BreakableWall)
                    return true;
        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (SpriteGameObject player in players)
                if (player != this)
                    if (CollidesWith(player))
                        return true;
        //check enemycollision
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;
        foreach (SpriteGameObject enemy in enemies)
            if (CollidesWith(enemy))
                return true;

        return false;
    }
    

}

