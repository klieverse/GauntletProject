using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

class PlayingState : IGameLoopObject
{
    static protected List<Level> levels;
    static protected int currentLevelIndex;
    protected ContentManager content;
    protected int maxLevelIndex = 9;
    static protected bool maxLevelReached = false, justOpened;
    

    public PlayingState(ContentManager content)
    {
        this.content = content;
        currentLevelIndex = 0;
        levels = new List<Level>();
        LoadLevels();
        justOpened = true;
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
        if (justOpened)
        {
            LoadPlayers();
            justOpened = false;
        }
        CurrentLevel.Update(gameTime);
        if (CurrentLevel.GameOver)
        {
            GameEnvironment.GameStateManager.SwitchTo("gameOverState");
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        CurrentLevel.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
    }

   static public void Exit()
   {
        CurrentLevel.Reset();
        currentLevelIndex = 0;
        justOpened = true;
        GameEnvironment.GameStateManager.SwitchTo("titleMenu");
   }

    static public void NextLevel(int index)
    {
        List<Player> players = new List<Player>();
        foreach (Player player in (CurrentLevel.Find("players") as GameObjectList).Children)
            players.Add(player);
        
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
        
        ReloadPlayers(players);

    }

    static public void ReloadPlayers(List<Player> players)
    {
        foreach (Player player in players)
        {
            if (player.Id== "Elf")
                player.Position = new Vector2(((float)CurrentLevel.startPositionQuestor.X + 0.5f) * Tile.Size,
                    (CurrentLevel.startPositionQuestor.Y + 1) * Tile.Size);
            if (player.Id == "Wizard")
                player.Position = new Vector2(((float)CurrentLevel.startPositionMerlin.X + 0.5f) * Tile.Size,
                    (CurrentLevel.startPositionMerlin.Y + 1) * Tile.Size);
            if (player.Id == "Warrior")
                player.Position = new Vector2(((float)CurrentLevel.startPositionThor.X + 0.5f) * Tile.Size,
                    (CurrentLevel.startPositionThor.Y + 1) * Tile.Size);
            if (player.Id == "Valkery")
                player.Position = new Vector2(((float)CurrentLevel.startPositionThyra.X + 0.5f) * Tile.Size,
                    (CurrentLevel.startPositionThyra.Y + 1) * Tile.Size);
            (CurrentLevel.Find("players") as GameObjectList).Add(player);
            (CurrentLevel.Find("StatFields") as GameObjectList).Children.Add(new PlayerStatField(player.Id));
        }
    }

    public void LoadLevels()
    {
        for(int i = 0; i <= 4; i++)
            levels.Add(new Level(i));
    }

   

    public void LoadPlayers()
    {
        if (GameEnvironment.SelectedClass == "Elf" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
        {
            LoadElf();
        }
        if (GameEnvironment.SelectedClass == "Wizard" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
        {
            LoadWizard();
        }
        if (GameEnvironment.SelectedClass == "Warrior" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
        {
            LoadWarrior();
        }
        if (GameEnvironment.SelectedClass == "Valkery" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
        {
            LoadValkery();
        }
    }
    public void LoadElf()
    {
        //get the startPosition for the player
        Vector2 startPosition = new Vector2(((float)CurrentLevel.startPositionQuestor.X + 0.5f) * Tile.Size, (CurrentLevel.startPositionQuestor.Y + 1) * Tile.Size);
        Questor questor = new Questor(4, "Elf", startPosition, CurrentLevel, true); //create the player
        (CurrentLevel.Find("players") as GameObjectList).Add(questor); //add player to level
        PlayerStatField questorStats = new PlayerStatField("Elf"); //create the players Statfield
        (CurrentLevel.Find("StatFields") as GameObjectList).Add(questorStats); //add the statfield to the level
    }
    public void LoadWizard()
    {
        Vector2 startPosition = new Vector2(((float)CurrentLevel.startPositionMerlin.X + 0.5f) * Tile.Size, (CurrentLevel.startPositionMerlin.Y + 1f) * Tile.Size);
        Merlin merlin = new Merlin(4, "Wizard", startPosition, CurrentLevel, true);
        (CurrentLevel.Find("players") as GameObjectList).Add(merlin);
        PlayerStatField wizardStats = new PlayerStatField("Wizard");
        (CurrentLevel.Find("StatFields") as GameObjectList).Add(wizardStats);
    }
    public void LoadWarrior()
    {
        Vector2 startPosition = new Vector2(((float)CurrentLevel.startPositionThor.X) * Tile.Size, (CurrentLevel.startPositionThor.Y) * Tile.Size);
        Thor thor = new Thor(4, "Warrior", startPosition, CurrentLevel, true);
        (CurrentLevel.Find("players") as GameObjectList).Add(thor);
        PlayerStatField warriorStats = new PlayerStatField("Warrior");
        (CurrentLevel.Find("StatFields") as GameObjectList).Add(warriorStats);
    }
    public void LoadValkery()
    {
        Vector2 startPosition = new Vector2((CurrentLevel.startPositionThyra.X + 2) * Tile.Size, (CurrentLevel.startPositionThyra.Y + 2) * Tile.Size);
        Thyra thyra = new Thyra(4, "Valkery", startPosition, CurrentLevel, true);
        (CurrentLevel.Find("players") as GameObjectList).Add(thyra);
        PlayerStatField valkeryStats = new PlayerStatField("Valkery");
        (CurrentLevel.Find("StatFields") as GameObjectList).Add(valkeryStats);
    }
}
