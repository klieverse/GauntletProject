using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Player : AnimatedGameObject
{
    protected Vector2 startPosition, previousPosition, direction = new Vector2(0,1);
    protected Level level;
    protected bool isAlive, isYou;
    protected float walkingSpeed, speedHelper, armor, magic, shotStrength, shotSpeed, melee;
    protected int orangePotions;
    public int health = 600, keys, potions;
    float timer = 1f;
    public string playerClass;

    public Player(int layer, string id, Vector2 start, Level level, float speed, float armor,
                        float magic, float shotStrength, float shotSpeed, float melee, bool isYou)
        : base(layer, id)
    {
        this.isYou = isYou;
        this.level = level;
        speedHelper = speed;
        this.armor = armor;
        this.magic = magic;
        this.shotStrength = shotStrength;
        this.shotSpeed = shotSpeed;
        this.melee = melee;
        this.id = id;
        startPosition = start;
        playerClass = id;

        LoadAnimations();
        Reset();
    }

    void LoadAnimations()
    {
        ////er mist rightup
        LoadAnimation("Sprites/Player/spr_" + id + "idle", id + "idle", true);
        LoadAnimation("Sprites/Player/spr_" + id + "runRight@13", id + "runRight", true);
      //LoadAnimation("Sprites/Player/spr_" + id + "runDownRight", id + "runDownRight", true);
      //LoadAnimation("Sprites/Player/spr_" + id + "runDown", id + "runDown", true);
      //LoadAnimation("Sprites/Player/spr_" + id + "runDownLeft", id + "runDownLeft", true);
        LoadAnimation("Sprites/Player/spr_" + id + "runLeft@13", id + "runLeft", true);
      //LoadAnimation("Sprites/Player/spr_" + id + "runUpLeft", id + "runUpLeft", true);
      //LoadAnimation("Sprites/Player/spr_" + id + "runUp", id + "runUp", true);
      //LoadAnimation("Sprites/Player/spr_" + id + "die", id + "die", false);
    }

    public override void Reset()
    {
        position = startPosition;
        velocity = Vector2.Zero;
        isAlive = true;
        PlayAnimation(id + "idle");
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (!isAlive)
        {
            return;
        }

        velocity = Vector2.Zero;

        if (inputHelper.IsKeyDown(Keys.A) && inputHelper.IsKeyDown(Keys.W))
        {
            velocity.Y = 0.71f * -walkingSpeed;
            velocity.X = 0.71f * -walkingSpeed;
        }
        else if (inputHelper.IsKeyDown(Keys.A) && inputHelper.IsKeyDown(Keys.S))
        {
            velocity.Y = 0.71f * walkingSpeed;
            velocity.X = 0.71f * -walkingSpeed;
        }
        else if (inputHelper.IsKeyDown(Keys.D) && inputHelper.IsKeyDown(Keys.W))
        {
            velocity.Y = 0.71f * -walkingSpeed;
            velocity.X = 0.71f * walkingSpeed;
        }
        else if (inputHelper.IsKeyDown(Keys.D) && inputHelper.IsKeyDown(Keys.S))
        {
            velocity.Y = 0.71f * walkingSpeed;
            velocity.X = 0.71f * walkingSpeed;
        }
        else if (inputHelper.IsKeyDown(Keys.A))
        {
            velocity.X = -walkingSpeed;
        }
        else if (inputHelper.IsKeyDown(Keys.D))
        {
            velocity.X = walkingSpeed;
        }
        else if (inputHelper.IsKeyDown(Keys.W))
        {
            velocity.Y = -walkingSpeed;
        }
        else if (inputHelper.IsKeyDown(Keys.S))
        {
            velocity.Y = walkingSpeed;
        }

        if (inputHelper.IsKeyDown(Keys.Space))
        {
            // PlayerShot shot = new PlayerShot(id, shotSpeed, shotStrength, velocity, position);
            (GameWorld.Find("playershot") as GameObjectList).Add(new PlayerShot(id, shotSpeed, shotStrength, direction, position));
        }

        if (inputHelper.IsKeyDown(Keys.LeftAlt))
        {
            if (potions > 0)
            {
                potions -= 1;
                KillEnemiesOnScreen();
            }
        }
    }


    public override void Update(GameTime gameTime)
    {
        if (velocity != Vector2.Zero)
        {
            direction = velocity;
        }
        previousPosition= position;
        walkingSpeed = (float)Math.Sqrt(speedHelper) * 10;
        base.Update(gameTime);
        HandleCamera();

        if (!isAlive)
        {
            return;
        }

        timer -= (float)gameTime.ElapsedGameTime.TotalSeconds; //makes the timer count down;

        if(timer <= 0)
        {
            health -= 1;
            timer = 1f;
        }

        CheckEnemyMelee();
        HandleCollisions();
        HandleAnimations();


        if (health <= 0)
        {
            Die();
        }
       // stats.Update(100, health, potions,keys,position);
    }

    void CheckEnemyMelee()
    {
        //check enemycollision
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;
        foreach (EnemyObject enemy in enemies)
            if (CollidesWith(enemy) && enemy.canBeMeleed == true)
            {
                enemy.HitByPlayer(melee);
                enemy.canBeMeleed = false;
            }
    }

    private void HandleAnimations() // Makes sure the right animation is being played;
    {
        if (velocity == Vector2.Zero)
        {
            PlayAnimation(id + "idle");
        }/*
        else if (velocity.X > 0 && velocity.Y == 0)
        {
            PlayAnimation(id + "runRight");
        }
        else if (velocity.X > 0 && velocity.Y > 0)
        {
            PlayAnimation(id + "runDownRight");
        }
        else if (velocity.X == 0 && velocity.Y > 0)
        {
            PlayAnimation(id + "runDown");
        }
        else if (velocity.X < 0 && velocity.Y > 0)
        {
            PlayAnimation(id + "runDownLeft");
        }
        else if (velocity.X < 0 && velocity.Y == 0)
        {
            PlayAnimation(id + "runLeft");
        }
        else if (velocity.X < 0 && velocity.Y < 0)
        {
            PlayAnimation(id + "runUpLeft");
        }
        else if (velocity.X == 0 && velocity.Y < 0)
        {
            PlayAnimation(id + "runUp");
        }
        */ //tijdenlijk voor testen met i.v.m. berkte animaties
        else if (velocity.X < 0)
        {
            PlayAnimation(id + "runLeft");
        }
        else if (velocity.X > 0)
        {
            PlayAnimation(id + "runRight");
        }
        else if (velocity.Y > 0)
        {
            PlayAnimation(id + "runRight");
        }
        else if (velocity.Y < 0)
        {
            PlayAnimation(id + "runLeft");
        }
    }

    public void Die()
    {
        if (!isAlive)
        {
            return;
        }

        isAlive = false;
        visible = false;
            velocity.Y = -900;
       // GameEnvironment.AssetManager.PlaySound("Sounds/snd_" + id + "_die");

        //PlayAnimation(id + "die");
    }

    private void HandleCollisions() //sets the position back to the last known position was before the player walked en collided with an object;
    {
        if (CollidesWithObject() == true)
        {
            position = previousPosition;
        }
    }

    void HandleCamera()
    {
        if (isYou)
        {
            float offsetX = GameEnvironment.Screen.X / 2;
            float offsetY = GameEnvironment.Screen.Y / 2;

            // makes sure camera does not go out of bounds w/ a clamp;
            float camX = MathHelper.Clamp(Position.X - offsetX, 0, level.LevelWidth - GameEnvironment.Screen.X); 
            float camY = MathHelper.Clamp(Position.Y - offsetY, 0, level.LevelHeight - GameEnvironment.Screen.Y);

            Camera.Position = new Vector2(camX, camY);
        }
    }

    void KillEnemiesOnScreen()
    {
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;
        foreach (SpriteGameObject enemy in enemies)
        {
            // checks if the enemy in question has a position somewhere on the screen;
            float onScreenEnemyX = MathHelper.Clamp(enemy.Position.X, Camera.Position.X, Camera.Position.X + GameEnvironment.Screen.X);
            float onScreenEnemyY = MathHelper.Clamp(enemy.Position.Y, Camera.Position.Y, Camera.Position.Y + GameEnvironment.Screen.Y); 
            if (enemy.Position.X == onScreenEnemyX && enemy.Position.Y == onScreenEnemyY) 
            {
                //enemy.Die();
            }

        }
            
    }

    public bool CollidesWithObject()
    {
        TileField tileField = GameWorld.Find("tiles") as TileField;
        //check wall collision
        Tile tile = tileField.Get(1, 1) as Tile;
        int Left = (int)((position.X - Width / 2) / tile.Width);
        int Right = (int)((position.X + Width / 2) / tile.Width);
        int Top = (int)((position.Y - Height) / tile.Height);
        int Bottom = (int)((position.Y) / tile.Height);

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

    public void HitByEnemy(float EnemyStrength)
    {   // calculates the damage, where the more armor the player has, the closer the damage is to being only half the strength of the enemy;
        float Damage = (0.5f * EnemyStrength) + (0.5f * EnemyStrength * (1 - ((armor / 100) / (armor / 100 + 1)))); 
        health -= (int)Damage;
    }

    public void AddKey()
    {
        keys+= 1;
    }

    public void AddPotion(PotionType pot)
    {
        switch (pot)
        {
            case PotionType.Normal:
                potions += 1;
                break;
            case PotionType.Orange:
                orangePotions += 1;
                break;
            default:
                break;
        }
    }

    public void ArmorUp()
    {
        armor += 10f;
    }

    public void SpeedUp()
    {
        speedHelper += 30f;
    }
    
    public bool IsAlive
    {
        get { return isAlive; }
    }
}
    
