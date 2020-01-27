using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


class Player : AnimatedGameObject
{
    protected Vector2 startPosition, previousPosition, direction = new Vector2(1, 0);
    [JsonIgnore]
    protected Level level
    {
        get;
        set;
    }
    protected bool isAlive, isYou, lastLookedLeft = false, canMove = true, canShoot = true;
    protected float walkingSpeed, speedHelper, armor, magic, shotStrength, shotSpeed, melee;
    protected float baseSpeedHelper, baseArmor, baseMagic, baseShotStrength, baseShotSpeed, baseMelee;
    public int health = 100, keys, potions, score;
    float healthTimer = 1f, shootTimer = 0.225f;
    InputHelper inputHelper;
    float multiplier;

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
        baseMagic = magic * 20;
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
        LoadAnimation("Sprites/Player/spr_" + id + "idle@4", "idle", true, 0.15f);
        LoadAnimation("Sprites/Player/spr_" + id + "run@4", "run", true);
        LoadAnimation("Sprites/Player/spr_" + id + "shoot@3", "shoot", true);
        LoadAnimation("Sprites/Player/spr_" + id + "die@3", "die", false);
    }

    public override void Reset()
    {
        Console.WriteLine("hier reset ie");
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
        this.inputHelper = inputHelper;
        
        if (!isAlive)
        {
            return;
        }

        velocity = Vector2.Zero;
        walkingSpeed = (float)Math.Sqrt(speedHelper) * 10;

        if (inputHelper.ControllerConnected())
            Xinput(inputHelper);
        else KeyboardInput(inputHelper);
        VectorHelper();
    }

    

    public override void Update(GameTime gameTime)
    {
        
        
        previousPosition = position;

        base.Update(gameTime);
        HandleCamera();

        if (!isAlive)
        {
            return;
        }

        HandleTimer(gameTime);
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

    private void VectorHelper()
    {
        if (velocity != Vector2.Zero && !inputHelper.ControllerConnected()) // sets direction foor when player is standing still and no controller is connected
        {
            direction = velocity;
        }
        else
        {
            float vectorLength = (float)Math.Sqrt((inputHelper.JoyStickLeft.Y * inputHelper.JoyStickLeft.Y) + (inputHelper.JoyStickLeft.X * inputHelper.JoyStickLeft.X));
            if (vectorLength > 1f)
                multiplier = 1f/vectorLength; /// vectorLength; // helps set the velocity to 1 * walkingspeed if it would otherwise succeed it
            else multiplier = 1f;
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

    public void HandleCamera()
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

    public void KillEnemiesOnScreen(bool intentional)
    {
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;
        foreach (EnemyObject enemy in enemies)
        {
            // checks if the enemy in question has a position somewhere on the screen;
            float onScreenEnemyX = MathHelper.Clamp(enemy.Position.X, Camera.Position.X, Camera.Position.X + GameEnvironment.Screen.X);
            float onScreenEnemyY = MathHelper.Clamp(enemy.Position.Y, Camera.Position.Y, Camera.Position.Y + GameEnvironment.Screen.Y);
            if (enemy.Position.X == onScreenEnemyX && enemy.Position.Y == onScreenEnemyY)
            {
                if (intentional)
                    enemy.HitByPlayer(magic);
                else enemy.HitByPlayer(magic/1.5f);
            }

        }

    }
    void HandleCollision()
    {
        //check Tile collision
        TileField tiles = GameWorld.Find("tiles") as TileField;
        Tile tile = tiles.Get(1, 1) as Tile;
        int Left = (int)((position.X - Width / 2) / tile.Width);
        int Right = (int)((position.X + Width / 2) / tile.Width);
        int Top = (int)((position.Y - Height / 2) / tile.Height);
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
                Rectangle boundingBox = new Rectangle((int)position.X - Width/2, (int)position.Y - Height / 2, Width, Height/2 );
                 
                if (((currentTile != null && !currentTile.CollidesWith(this)) || currentTile == null) && !tileBounds.Intersects(boundingBox))
                {
                    continue;
                }
                Vector2 tileDepth = Collision.CalculateIntersectionDepth(boundingBox, tileBounds);

                List<GameObject> doors = (GameWorld.Find("Doors") as GameObjectList).Children;

                if (Math.Abs(tileDepth.X) < Math.Abs(tileDepth.Y))
                {
                    if (tileType == TileType.Wall || tileType == TileType.BreakableWall || tileType == TileType.HorizontalDoor
                        || tileType == TileType.VerticalDoor || tileType == TileType.Teleporter || tileType == TileType.Temple)
                    {
                        if (doors != null)
                        {
                            foreach (Door door in doors)
                            {
                                if (CollidesWith(door) && keys > 0)
                                {
                                    door.DeleteDoors();
                                    keys -= 1;
                                }
                            }
                        }
                        if (tiles.GetTileType(x + 1, y) == TileType.Background)
                            position.X += tileDepth.X;
                        else position.X += tileDepth.X - 1;
                    }
                    continue;
                }

                if (tileType == TileType.Wall || tileType == TileType.BreakableWall || tileType == TileType.Teleporter
                    || tileType == TileType.HorizontalDoor || tileType == TileType.VerticalDoor || tileType == TileType.Temple)
                {
                    if (doors != null)
                    {
                        foreach (Door door in doors)
                        {
                            if (CollidesWith(door) && keys > 0)
                            {
                                door.DeleteDoors();
                                keys -= 1;
                            }
                        }
                    }
                    if (tiles.GetTileType(x + 1, y) == TileType.Background)
                        position.Y += tileDepth.Y - 1;
                    else position.Y += tileDepth.Y;
                }
            }

        // checks collision w/ enemies
        List<GameObject> enemies = (GameWorld.Find("enemies") as GameObjectList).Children;

        if (enemies != null)
        {
            foreach (EnemyObject enemy in enemies)
            {
                if (CollidesWith(enemy))
                {
                    Rectangle enemieBox = enemy.BoundingBox;
                    Rectangle boundingBox = this.BoundingBox;
                    boundingBox.X += 1;
                    Vector2 enemyDepth = Collision.CalculateIntersectionDepth(boundingBox, enemieBox);

                    if (Math.Abs(enemyDepth.X) < Math.Abs(enemyDepth.Y))
                    {
                        if ((velocity.X > 0 && enemy.Velocity.X < 0) || (velocity.X < 0 && enemy.Velocity.X > 0) || (velocity.X > 0 && enemy.Velocity.X >= 0 && position.X < enemy.Position.X) 
                            || (velocity.X <= 0 && enemy.Velocity.X < 0 && position.X > enemy.Position.X))
                            position.X = previousPosition.X;
                    }
                    else
                    {
                        if ((velocity.Y > 0 && enemy.Velocity.Y < 0) || (velocity.Y < 0 && enemy.Velocity.Y > 0) || (velocity.Y > 0 && enemy.Velocity.Y > 0 && position.Y < enemy.Position.Y) 
                            || (velocity.Y < 0 && enemy.Velocity.Y < 0 && position.Y > enemy.Position.Y))
                            position.Y = previousPosition.Y;
                    }

                    if (enemy.canBeMeleed)
                    {
                        enemy.HitByPlayer(melee);
                        enemy.canBeMeleed = false;
                    }
                }

            }
        }

        // checks collision w/ players
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;

        if (players != null)
        {
            foreach (Player player in players)
            {
                if (player != this)
                    if (CollidesWith(player))
                    {
                        Rectangle playerBox = player.BoundingBox;
                        Rectangle boundingBox = this.BoundingBox;
                        boundingBox.X += 1;
                        Vector2 playerDepth = Collision.CalculateIntersectionDepth(boundingBox, playerBox);

                        if (Math.Abs(playerDepth.X) < Math.Abs(playerDepth.Y))
                        {
                            if ((velocity.X > 0 && player.Velocity.X < 0) || (velocity.X < 0 && player.Velocity.X > 0) || (velocity.X > 0 && player.Velocity.X >= 0 && position.X < player.Position.X)
                                || (velocity.X < 0 && player.Velocity.X <= 0 && previousPosition.X > player.Position.X))
                                position.X = previousPosition.X;
                        }
                        else
                        {
                            if ((velocity.Y > 0 && player.Velocity.Y < 0) || (velocity.Y < 0 && player.Velocity.Y > 0) || (velocity.Y > 0 && player.Velocity.Y >= 0 && position.Y < player.Position.Y)
                                || (velocity.Y < 0 && player.Velocity.Y <= 0 && position.Y > player.Position.Y))
                                position.Y = previousPosition.Y;
                        }
                    }
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
            case PotionType.Armor:
                armor += 10f;
                break;
            case PotionType.Magic:
                magic += 10f;
                break;
            case PotionType.Melee:
                melee += 10f;
                break;
            case PotionType.ShotPower:
                shotStrength += 10f;
                break;
            case PotionType.ShotSpeed:
                shotSpeed += 10f;
                break;
            case PotionType.Speed:
                speedHelper += 30f;
                break;
            default:
                break;
        }
    }

    public void EatFood()
    {
        health += 100;
    }
    public void ScoreUp(int score)
    {
        this.score += score;
    }

    private void KeyboardInput(InputHelper inputHelper)
    {

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
                GameEnvironment.AssetManager.PlaySound(id + " shot", position.X);
                (GameWorld.Find("playershot") as GameObjectList).Add(new PlayerShot(id, shotSpeed, shotStrength, direction, position, this, inputHelper));
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
                KillEnemiesOnScreen(true);
            }
        }
    }

    void Xinput(InputHelper inputHelper)
    {
        if (canMove)
        {
            velocity.X = multiplier * walkingSpeed * inputHelper.JoyStickLeft.X;
            velocity.Y = multiplier * walkingSpeed * -inputHelper.JoyStickLeft.Y;
        }

        direction.X = inputHelper.JoyStickRight.X;
        direction.Y = -inputHelper.JoyStickRight.Y;

        if (canShoot && (inputHelper.JoyStickRight.X > 0.2f || inputHelper.JoyStickRight.X < -0.2f || inputHelper.JoyStickRight.Y > 0.2f || inputHelper.JoyStickRight.Y < -0.2f))
        {
            shootTimer = 0.225f;
            canShoot = false;
            PlayAnimation("shoot");
            GameEnvironment.AssetManager.PlaySound(id +" shot",position.X);
            (GameWorld.Find("playershot") as GameObjectList).Add(new PlayerShot(id, shotSpeed, shotStrength, direction, position, this, inputHelper));
        }

        if (inputHelper.JoyStickRight == Vector2.Zero)
        {
            canShoot = true;
            canMove = true;
        }
        else canMove = false;

        if (inputHelper.ButtonPressed(Buttons.Y))
        {
            if (potions > 0)
            {
                potions -= 1;
                KillEnemiesOnScreen(true);
            }
        }
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

