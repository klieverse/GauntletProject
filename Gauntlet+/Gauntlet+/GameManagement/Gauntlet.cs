﻿using Microsoft.Xna.Framework;
using SuperWebSocket;
using System;

class Gauntlet : GameEnvironment
{
    [STAThread]
    static void Main()
    {
        Gauntlet game = new Gauntlet();
        game.Run();
        
    }

    public Gauntlet()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        screen = new Point(1440, 825);
        windowSize = new Point(1024, 586);
        FullScreen = false;

        gameStateManager.AddGameState("titleMenu", new TitleMenuState());
        //gameStateManager.AddGameState("helpState", new HelpState());
        //gameStateManager.AddGameState("ChooseCharacterState", new ChooseCharacterState());
        gameStateManager.AddGameState("playingState", new PlayingState(Content));
        //gameStateManager.AddGameState("multiPlayerState", new MultiPlayerState(Content));
        //gameStateManager.AddGameState("gameOverState", new GameOverState());
        //gameStateManager.AddGameState("BetweenLevelState", new BetweenLevelState());
        gameStateManager.SwitchTo("titleMenu");

        //AssetManager.PlayMusic("Sounds/snd_music");
    }
}
