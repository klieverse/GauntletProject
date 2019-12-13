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
    protected int health = 30;
    protected int strength;
    protected int speed = 250;
    TileField tileField;
    
    public EnemyObject(int layer, string id) : base(layer, id)
    {
        //loading animation for corresponding enemy id
        LoadAnimation(id, id, true);
        PlayAnimation(id);
        
       
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        tileField = GameWorld.Find("tiles") as TileField;
        Player player = GameWorld.Find("Player") as Player;
        if (player != null)
        {
            //calculate the opposite and adjacent distances of enemy and player
            float opposite = player.Position.Y - position.Y + 55;
            float adjacent = player.Position.X - position.X + 30;
            //calculate the degree in which the enemy and player are seperated
            float vertical = (float)Math.Atan2(opposite, adjacent);
            float horizontal = (float)Math.Atan2(adjacent, opposite);
            //calculate the speed in which the enemy will move to the player horizontally and vertically
            speedVert = (float)Math.Sin(vertical) * speed;
            speedHori = (float)Math.Sin(horizontal) * speed;
            velocity.Y = speedVert;
            velocity.X = speedHori;
        }
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

    //decrease health of enemy if hit by the player
    public void HitByPlayer(float damage)
    {
        health -= (int)damage;
    }
    

}

