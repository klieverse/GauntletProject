using Microsoft.Xna.Framework;
using SuperWebSocket;
using System;

class Gauntlet : GameEnvironment
{
    private static WebSocketServer wsServer;
    [STAThread]
    static void Main()
    {
        Gauntlet game = new Gauntlet();
        wsServer = new WebSocketServer();
        int port = 8088;
        wsServer.Setup(port);
        wsServer.Start();
        wsServer.NewSessionConnected += WsServer_NewSessionConnected;
        wsServer.NewMessageReceived += WsServer_NewMessageReceived;
        wsServer.NewDataReceived += WsServer_NewDataReceived;
        wsServer.SessionClosed += WsServer_SessionClosed;
        Console.WriteLine("Server is running on port " + port + ". Press ENTER to exit....");
        Console.Read();
        game.Run();
        
    }

    public Gauntlet()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    private static void WsServer_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
    {
        Console.WriteLine("SessionClosed");
    }

    private static void WsServer_NewDataReceived(WebSocketSession session, byte[] value)
    {
        Console.WriteLine("NewDataReceived");
    }

    private static void WsServer_NewMessageReceived(WebSocketSession session, string value)
    {
        Console.WriteLine("NewMessageReceived: " + value);
        if (value == "Hello server")
        {
            session.Send("Hello client");
        }
    }

    private static void WsServer_NewSessionConnected(WebSocketSession session)
    {
        Console.WriteLine("NewSessionConnected");
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
        //gameStateManager.AddGameState("multiPlaterState", new MultiPlayerState(Content));
        //gameStateManager.AddGameState("gameOverState", new GameOverState());
        //gameStateManager.AddGameState("BetweenLevelState", new BetweenLevelState());
        gameStateManager.SwitchTo("titleMenu");

        //AssetManager.PlayMusic("Sounds/snd_music");
    }
}
