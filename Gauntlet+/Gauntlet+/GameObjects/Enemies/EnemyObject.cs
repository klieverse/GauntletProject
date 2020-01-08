﻿using System;
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

    public bool canBeMeleed = true;

    public EnemyObject(int layer, string id) : base(layer, id)
    {
        LoadAnimations();
        PlayAnimation(id + "idle");
    }
    void LoadAnimations()
    {
        LoadAnimation("Sprites/Enemies/spr_" + id + "idle", id + "idle", true);
        LoadAnimation("Sprites/Enemies/spr_" + id + "runRight@13", id + "runRight", true);
        LoadAnimation("Sprites/Enemies/spr_" + id + "runLeft@13", id + "runLeft", true);
    }
    public override void Update(GameTime gameTime)
    {
        previousPosition = position;
        base.Update(gameTime);
        
        tileField = GameWorld.Find("tiles") as TileField;
        if (CollidesWithObject())
            position = previousPosition;
        Player player = GameWorld.Find("Elf") as Player;
        if (player != null)
        {
            float opposite = player.Position.Y - position.Y + 55;
            float adjacent = player.Position.X - position.X + 30;
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

    public void HitByPlayer(float damage)
    {
        health -= (int)damage;
    }
    

}

