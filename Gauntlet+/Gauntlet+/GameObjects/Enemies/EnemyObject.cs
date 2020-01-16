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
    float meleeTimer = 1;
    protected Vector2 previousPosition;
    protected bool lastLookedLeft = false;

    public bool canBeMeleed = true;

    public EnemyObject(int layer, string id) : base(layer, id)
    {
        LoadAnimations();
    }
    void LoadAnimations()
    {
        LoadAnimation("Sprites/Enemies/spr_" + id + "idle@4", "idle", true);
        LoadAnimation("Sprites/Enemies/spr_" + id + "run@4", "run", true);
    }
    public override void Update(GameTime gameTime)
    {
        previousPosition = position;
        base.Update(gameTime);
        
        tileField = GameWorld.Find("tiles") as TileField;
        if (CollidesWithObject())
            position = previousPosition;
        GameObjectList players = GameWorld.Find("players") as GameObjectList;
        Player player;
        if (players.Children.Count != 0)
        {
            player = players.Children[0] as Player;
        
            float opposite = player.Position.Y - position.Y;
            float adjacent = player.Position.X - position.X;
            float vertical = (float)Math.Atan2(opposite, adjacent);
            float horizontal = (float)Math.Atan2(adjacent, opposite);
            speedVert = (float)Math.Sin(vertical) * speed;
            speedHori = (float)Math.Sin(horizontal) * speed;
            velocity.Y = speedVert;    
            velocity.X = speedHori;
        }

        meleeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds; // makes sure a specific enemy can only be melee'd once a second;
        if (meleeTimer <= 0)
        {
            canBeMeleed = true;
        }
        HandleAnimations();
    }

    void HandleAnimations()
    {
        if (velocity == Vector2.Zero)
        {
            PlayAnimation("idle");
        }
        else PlayAnimation("run");
        


        if (velocity.X < 0)
        {
            lastLookedLeft = true;
        }
        else if (velocity.X > 0)
        {
            lastLookedLeft = false;
        }

        if (velocity.X < 0 || lastLookedLeft)
        {
            Mirror = true;
        }
        else Mirror = false;
    }

    public bool CollidesWithObject()
    {
 
        //check wall collision
        Tile tile = tileField.Get(1, 1) as Tile;
        int Left = (int)((position.X -this.Width /2)/ tile.Width);
        int Right = (int)((position.X + this.Width /2) / tile.Width);
        int Top = (int)((position.Y-Height) / tile.Height);
        int Bottom = (int)((position.Y ) / tile.Height);

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
                if (tileField.GetTileType(x, y) == TileType.Wall || tileField.GetTileType(x, y) == TileType.BreakableWall 
                    || tileField.GetTileType(x, y) == TileType.HorizontalDoor || tileField.GetTileType(x, y) == TileType.VerticalDoor)
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
            if (enemy !=this)
                if (CollidesWith(enemy))
                    return true;

        return false;
    }

    public void HitByPlayer(float damage)
    {
        health -= (int)damage;
    }
    

}

