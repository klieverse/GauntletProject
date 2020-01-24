using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

class PlayingState : IGameLoopObject
{
    static protected List<Level> levels;
    static protected int currentLevelIndex = 1;
    protected ContentManager content;
    protected int maxLevelIndex = 9;
    static protected bool maxLevelReached = false;

    public PlayingState(ContentManager content)
    {
        this.content = content;
        currentLevelIndex = 0;
        levels = new List<Level>();
        LoadLevels();
    }

    static public Level CurrentLevel
    {
        get { return levels[currentLevelIndex]; } 
    }

    static public int CurrentLevelIndex
    {
        get { return currentLevelIndex; }
        set
        {
            if (value >= 0 && value < levels.Count)
            {
                currentLevelIndex = value;
                CurrentLevel.Reset();
            }
        }
    }

    public List<Level> Levels
    {
        get { return levels; }
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        CurrentLevel.HandleInput(inputHelper);
    }

    public virtual void Update(GameTime gameTime)
    {
        CurrentLevel.Update(gameTime);
        if (CurrentLevel.GameOver)
        {
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");
        }
        else if (CurrentLevel.Completed)
        {
            GameEnvironment.GameStateManager.SwitchTo("levelFinishedState");
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        CurrentLevel.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
        CurrentLevel.Reset();
    }

   

    static public void NextLevel(int index)
    {
        CurrentLevel.Reset();
        if (maxLevelReached || currentLevelIndex >= levels.Count - 1)
        {
            CurrentLevelIndex = GameEnvironment.Random.Next(1,5);
            maxLevelReached = true;
        }

        else
        {
            CurrentLevelIndex = index;
        }
    }


    public void LoadLevels()
    {
        for(int i = 0; i <= 4; i++)
            levels.Add(new Level(i));
    }
}
