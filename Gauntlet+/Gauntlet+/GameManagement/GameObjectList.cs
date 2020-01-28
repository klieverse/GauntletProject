using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class GameObjectList : GameObject
{
    protected List<GameObject> children;

    public List<GameObject> Delete;

    public GameObjectList(int layer = 0, string id = "") : base(layer, id)
    {
        children = new List<GameObject>();
        Delete = new List<GameObject>();
    }

    public List<GameObject> Children
    {
        get { return children; }
        set { children = value; }
    }

    public void Add(GameObject obj)
    {
        obj.Parent = this;
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].Layer > obj.Layer)
            {
                children.Insert(i, obj);
                return;
            }
        }
        children.Add(obj);
    }

    public void Remove(GameObject obj)
    {
        children.Remove(obj);
    }

    public GameObject Find(string id)
    {
        foreach (GameObject obj in children)
        {
            if (obj.Id == id)
            {
                return obj;
            }
            if (obj is GameObjectList)
            {
                GameObjectList objList = obj as GameObjectList;
                GameObject subObj = objList.Find(id);
                if (subObj != null)
                {
                    return subObj;
                }
            }
        }
        return null;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        for (int i = children.Count - 1; i >= 0; i--)
        {
            children[i].HandleInput(inputHelper);
        }
    }

    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in children)
        {
            
            if (ObjectIsList(obj))
            {
                obj.Update(gameTime);
            }
            else
            {
                if (Camera.CameraBox.Contains(obj.Position))
                {
                    obj.Update(gameTime);
                    if((!obj.Visible) && (Id == "enemies" || Id == "playershot" || Id == "enemieShot") && obj.Id != "Wizard")
                    {
                        Delete.Add(obj);
                    }
                }
            }
        }
        foreach(GameObject obj in Delete)
        {
            Remove(obj);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
        {
            return;
        }
        foreach(GameObject obj in children)
        {
            obj.Draw(gameTime, spriteBatch);
        }
        
    }

    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in children)
        {
            obj.Reset();
        }
    }

    public bool ObjectIsList(GameObject obj)
    {
        switch(obj.Id)
        {
            case "spawns":
            case "players":
            case "StatFields":
            case "enemies":
            case "food":
            case "keys":
            case "treasures":
            case "potions":
            case "playershot":
            case "enemieShot":
            case "teleport":
            case "BreakableWalls":
            case "Doors":
            case "Exits":
            case "SpawnObjects":
                return true;
            default:
                return false;
        }
    }
}

