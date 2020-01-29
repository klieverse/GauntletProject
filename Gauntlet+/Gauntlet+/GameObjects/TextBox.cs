using Microsoft.Xna.Framework;
using System;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

class TextBox : TextGameObject
{
    public List<Keys> pressedKeys;
    public Dictionary<Keys, float> usedKeys;

    public TextBox(string Id, Vector2 position) : base("TextFont", 150, Id)
    {
        this.position = position;
        color = Color.Red;
        text = "";
        usedKeys = new Dictionary<Keys, float>();
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        pressedKeys = inputHelper.PressedKeys();
    }
    
    public override void Update(GameTime gameTime)
    {
        UpdateKeys(gameTime);
        if(pressedKeys!= null)
        {
            foreach(Keys k in pressedKeys)
            {
                if((int)k > 47 && (int)k < 58 && !usedKeys.ContainsKey(k))
                {
                    text += k.ToString()[1];
                    usedKeys.Add(k, 0);
                }
                else if (k == Keys.OemPeriod && !usedKeys.ContainsKey(k))
                {
                    text += '.';
                    usedKeys.Add(k, 0);
                }
                else if (k != Keys.Back && k != Keys.Enter && !usedKeys.ContainsKey(k))
                {
                    text += k;
                    usedKeys.Add(k, 0);
                }
                else if (k == Keys.Back && !usedKeys.ContainsKey(k))
                {
                    text = text.Substring(0, text.Length - 1);
                    usedKeys.Add(k, 0);
                }
            }
        }
    }

    public void UpdateKeys(GameTime gameTime)
    {
        List<Keys> removeKeys = new List<Keys>();
        Dictionary<Keys, float> changeKeys = new Dictionary<Keys, float>();
        if(usedKeys != null && usedKeys.Count > 0)
        {
            foreach(KeyValuePair<Keys, float> kvp in usedKeys)
            {
                changeKeys.Add(kvp.Key, kvp.Value + (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (kvp.Value > 2)
                {
                    removeKeys.Add(kvp.Key);
                }
            }
        }
        foreach(KeyValuePair<Keys, float> kvp in changeKeys)
        {
            usedKeys[kvp.Key] = kvp.Value;
        }
        foreach(Keys k in removeKeys)
        {
            usedKeys.Remove(k);
        }

    }
}

