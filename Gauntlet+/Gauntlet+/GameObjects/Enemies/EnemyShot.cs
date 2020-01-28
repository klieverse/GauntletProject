using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class EnemyShot : AnimatedGameObject
{
    protected int strength;
    protected bool isGnome;
    float timer = 5f;
    protected EnemyObject shooter;
    public EnemyShot(int layer, string id, EnemyObject shooter, bool isGnome = true) : base(layer, id)
    {
        this.isGnome = isGnome;
        LoadAnimations();
        PlayAnimation("shoot");
        velocity *= 0.8f;
        this.shooter = shooter;
    }

    void LoadAnimations()
    {
        LoadAnimation("GnomeShoot", "shoot", false);       
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        HandleCollision();

        timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timer <= 0)
            visible = false;
    }

    private void HandleCollision()
    {
        if (isGnome)
        {
            List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
            if (players != null)
                foreach (Player player in players)
                    if (CollidesWith(player))
                    {
                        player.HitByEnemy(strength);
                        visible = false;
                        GameEnvironment.AssetManager.PlaySound("Ghost hit", position.X);
                    }
        }
        else
        {
            List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
            if (players != null)
                foreach (Player player in players)
                    if (CollidesWith(player))
                    {
                        player.HitByEnemy(strength);
                        visible = false;
                        GameEnvironment.AssetManager.PlaySound("Ghost hit", position.X);
                    }
            if (CollidesWithObject())
            {
                visible = false;
                GameEnvironment.AssetManager.PlaySound("Ghost hit", position.X);
            }
        }
    }

    public bool CollidesWithObject()
    {

        //check wall collision
        TileField tileField = GameWorld.Find("tiles") as TileField;
        Tile tile = tileField.Get(1, 1) as Tile;
        int Left = (int)((position.X - this.Width / 2) / tile.Width);
        int Right = (int)((position.X + this.Width / 2) / tile.Width);
        int Top = (int)((position.Y - Height) / tile.Height);
        int Bottom = (int)((position.Y) / tile.Height);

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
                if (tileField.GetTileType(x, y) == TileType.Wall || tileField.GetTileType(x, y) == TileType.BreakableWall
                    || tileField.GetTileType(x, y) == TileType.HorizontalDoor || tileField.GetTileType(x, y) == TileType.VerticalDoor)
                    return true;
        //check playercollision
        /*List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (SpriteGameObject player in players)
                if (player != this)
                    if (CollidesWith(player))
                        return true;*/
        //check enemycollision
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;
        foreach (SpriteGameObject enemy in enemies)
            if (enemy != shooter)
                if (CollidesWith(enemy))
                    return true;

        List<GameObject> spawns = (GameWorld.Find("spawns") as GameObjectList).Children;
        if (spawns != null)
        {
            foreach (SpriteGameObject spawn in spawns)
                if (CollidesWith(spawn))
                    return true;
        }

        return false;
    }

}