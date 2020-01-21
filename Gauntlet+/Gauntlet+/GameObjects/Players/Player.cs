using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


class Player : AnimatedGameObject
{
    protected Vector2 startPosition, previousPosition, direction = new Vector2(1, 0);
    protected Level level;
    protected bool isAlive, isYou, lastLookedLeft = false, canMove = true, canShoot = true;
    protected float walkingSpeed, speedHelper, armor, magic, shotStrength, shotSpeed, melee;
    protected float baseSpeedHelper, baseArmor, baseMagic, baseShotStrength, baseShotSpeed, baseMelee;
    public int health = 100, keys, potions, score;
    float healthTimer = 1f, shootTimer = 0.225f;

    public Player(int layer, string id, Vector2 start, Level level, float speed, float armor,
                        float magic, float shotStrength, float shotSpeed, float melee, bool isYou)
        : base(layer, id)
    {
        this.isYou = isYou;
        this.level = level;
        speedHelper = speed;
        baseSpeedHelper = speed;
        this.armor = armor;
        baseArmor = armor;
        this.magic = magic * 10;
        baseMagic = magic * 10;
        this.shotStrength = shotStrength * 10;
        baseShotStrength = shotStrength * 10;
        this.shotSpeed = shotSpeed;
        baseShotSpeed = shotSpeed;
        this.melee = melee * 10;
        baseMelee = melee * 10;
        startPosition = new Vector2(start.X, start.Y + 20);

        LoadAnimations();
        Reset();
    }

    public virtual void LoadAnimations()
    {
        LoadAnimation("Sprites/Player/spr_" + id + "idle@4", "idle", true);
        LoadAnimation("Sprites/Player/spr_" + id + "run@4", "run", true);
        LoadAnimation("Sprites/Player/spr_" + id + "shoot@3", "shoot", true);
        LoadAnimation("Sprites/Player/spr_" + id + "die@3", "die", false);
    }

    public override void Reset()
    {
        position = startPosition;
        velocity = Vector2.Zero;
        isAlive = true;
        PlayAnimation("idle");
        score = potions = keys = 0;
        health = 600;
        speedHelper = baseSpeedHelper;
        armor = baseArmor;
        magic = baseMagic;
        shotStrength = baseShotStrength;
        shotSpeed = baseShotSpeed;
        melee = baseMelee;
        lastLookedLeft = false;
        canMove = canShoot = true;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (!isAlive)
        {
            return;
        }

        velocity = Vector2.Zero;

        walkingSpeed = (float)Math.Sqrt(speedHelper) * 10;

        if (canMove)
        {
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
        }

        if (inputHelper.IsKeyDown(Keys.Space))
        {
            canMove = false;

            if (inputHelper.IsKeyDown(Keys.A) && inputHelper.IsKeyDown(Keys.W))
            {
                direction = new Vector2(-1, -1);
                lastLookedLeft = true;
            }
            else if (inputHelper.IsKeyDown(Keys.A) && inputHelper.IsKeyDown(Keys.S))
            {
                direction = new Vector2(-1, 1);
                lastLookedLeft = true;
            }
            else if (inputHelper.IsKeyDown(Keys.D) && inputHelper.IsKeyDown(Keys.W))
            {
                direction = new Vector2(1, -1);
                lastLookedLeft = false;
            }
            else if (inputHelper.IsKeyDown(Keys.D) && inputHelper.IsKeyDown(Keys.S))
            {
                direction = new Vector2(1, 1);
                lastLookedLeft = false;
            }
            else if (inputHelper.IsKeyDown(Keys.A))
            {
                direction = new Vector2(-1, 0);
                lastLookedLeft = true;
            }
            else if (inputHelper.IsKeyDown(Keys.D))
            {
                direction = new Vector2(1, 0);
                lastLookedLeft = false;
            }
            else if (inputHelper.IsKeyDown(Keys.W))
            {
                direction = new Vector2(0, -1);
            }
            else if (inputHelper.IsKeyDown(Keys.S))
            {
                direction = new Vector2(0, 1);
            }

            if (canShoot)
            {
                shootTimer = 0.225f;
                canShoot = false;
                PlayAnimation("shoot");
                (GameWorld.Find("playershot") as GameObjectList).Add(new PlayerShot(id, shotSpeed, shotStrength, direction, position, this));
            }
        }

        if (inputHelper.keyReleased(Keys.Space))
        {
            canMove = true;
        }

        if (inputHelper.IsKeyDown(Keys.E))
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
        SetDirection();

        previousPosition = position;

        base.Update(gameTime);
        HandleCamera();

        if (!isAlive)
        {
            return;
        }

        HandleTimer(gameTime);
        CheckEnemyMelee();
        HandleEntityCollisions();
        HandleCollision();
        HandleAnimations();
        CheckIfDead();
        // stats.Update(100, health, potions,keys,position);
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            health = 0;
            velocity = Vector2.Zero;
            Die();
        }
    }

    private void SetDirection()
    {
        if (velocity != Vector2.Zero)
        {
            direction = velocity;
        }
    }

    private void HandleTimer(GameTime gameTime)
    {
        healthTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds; //makes the timer count down;
        shootTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (healthTimer <= 0)
        {
            health -= 1;
            healthTimer = 1f;
        }

        if (shootTimer <= 0)
        {
            canShoot = true;
            shootTimer = 0.225f;
        }
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
        if (!isAlive)
            return;
        if (canMove)
        {
            if (velocity == Vector2.Zero)
            {
                PlayAnimation("idle");
            }
            else PlayAnimation("run");
        }


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

    public void Die()
    {
        if (!isAlive)
        {
            return;
        }

        isAlive = false;
        //GameEnvironment.AssetManager.PlaySound("Sounds/snd_" + id + "_die");

        PlayAnimation("die");
    }

    private void HandleEntityCollisions() //sets the position back to the last known position was before the player walked en collided with an object;
    {
        if (CollidesWithEntity() == true)
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

    public void KillEnemiesOnScreen()
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
    void HandleCollision()
    {
        //check wall collision
        TileField tiles = GameWorld.Find("tiles") as TileField;
        Tile tile = tiles.Get(1, 1) as Tile;
        int Left = (int)((position.X - Width / 2) / tile.Width);
        int Right = (int)((position.X + Width / 2) / tile.Width);
        int Top = (int)((position.Y - Height) / tile.Height);
        int Bottom = (int)((position.Y) / tile.Height);

        for (int x = Left; x <= Right; x++)
            for (int y = Top; y <= Bottom; y++)
            {
                TileType tileType = tiles.GetTileType(x, y);
                if (tileType == TileType.Background)
                {
                    continue;
                }
                Tile currentTile = tiles.Get(x, y) as Tile;
                Rectangle tileBounds = new Rectangle(x * tiles.CellWidth, y * tiles.CellHeight,
                                                        tiles.CellWidth, tiles.CellHeight);
                Rectangle boundingBox = this.BoundingBox;
                //boundingBox.Height-=1;
                //boundingBox.Width -=1;
                if (((currentTile != null && !currentTile.CollidesWith(this)) || currentTile == null) && !tileBounds.Intersects(boundingBox))
                {
                    continue;
                }
                Vector2 depth = Collision.CalculateIntersectionDepth(boundingBox, tileBounds);
                if (Math.Abs(depth.X) < Math.Abs(depth.Y))
                {
                    if (tileType == TileType.Wall || tileType == TileType.BreakableWall || tileType == TileType.HorizontalDoor || tileType == TileType.VerticalDoor || tileType == TileType.Teleporter)
                    {
                        if (tiles.GetTileType(x + 1, y) == TileType.Background)
                            position.X += depth.X;
                        else position.X += depth.X - 1;
                    }
                    continue;
                }
                if (tileType == TileType.Wall || tileType == TileType.BreakableWall)
                {
                    if (tiles.GetTileType(x + 1, y) == TileType.Background)
                        position.Y += depth.Y - 1;
                    else position.Y += depth.Y;
                }
            }
        if (velocity.X >= 0 && velocity.Y >= 0)
            position = new Vector2((float)Math.Ceiling(position.X), (float)Math.Ceiling(position.Y));
        if (velocity.X >= 0 && velocity.Y <= 0)
            position = new Vector2((float)Math.Ceiling(position.X), (float)Math.Floor(position.Y));
        if (velocity.X <= 0 && velocity.Y >= 0)
            position = new Vector2((float)Math.Floor(position.X), (float)Math.Ceiling(position.Y));
        if (velocity.X <= 0 && velocity.Y <= 0)
            position = new Vector2((float)Math.Floor(position.X), (float)Math.Floor(position.Y));

    }

    public bool CollidesWithEntity()
    {
        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (player != this)
                    if (CollidesWith(player) && player.isAlive)
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
        keys += 1;
        ScoreUp(100);
    }

    public void UseKey()
    {
        if (keys >= 1)
        keys -= 1;
        return;
    }

    public void AddPotion(PotionType pot)
    {
        switch (pot)
        {
            case PotionType.Normal:
                potions += 1;
                break;
            case PotionType.Orange:
                potions += 1;
                break;
            default:
                break;
        }
    }

    public void EatFood()
    {
        health += 100;
    }

    public void ArmorUp()
    {
        armor += 10f;
    }

    public void SpeedUp()
    {
        speedHelper += 30f;
        walkingSpeed = (float)Math.Sqrt(speedHelper) * 10;
    }
    public void ScoreUp(int score)
    {
        this.score += score;
    }

    public string playerClass
    {
        get { return id; }
    }

    public bool IsAlive
    {
        get { return isAlive; }
    }

    public int Health
    {
        get { return health; }
    }

    public int Potions
    {
        get { return potions; }
    }

    public int Key
    {
        get { return keys; }
    }
    public int Score
    {
        get { return score; }
    }
}

