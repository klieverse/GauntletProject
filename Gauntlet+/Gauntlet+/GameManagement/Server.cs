using Lidgren.Network;
using System;

public class Server
{
    NetIncomingMessage message;
    public event EventHandler<NewPlayerEventArgs> NewPlayerEvent;
    private readonly ManagerLogger _managerLogger;
    private NetPeerConfiguration _config;
    public NetServer NetServer { get; private set; }
    public Server()
    {
        var config = new NetPeerConfiguration("gauntlet server") { Port = 12345 };
        var server = new NetServer(config);
        server.Start();
    }

    public void Update()
    {
    }
    

    public Server(ManagerLogger managerLogger)
    {
        _managerLogger = managerLogger;
        _gameRooms = new List<GameRoom>();
        _config = new NetPeerConfiguration("networkGame") { Port = 14241 };
        _config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
        NetServer = new NetServer(_config);
    }

    public void Run()
    {
        NetServer.Start();
        Console.WriteLine("Server started...");
        _managerLogger.AddLogMessage("Server", "Server started...");
        while (true)
        {
            NetIncomingMessage inc;
            if ((inc = NetServer.ReadMessage()) == null) continue;
            switch (inc.MessageType)
            {
                case NetIncomingMessageType.ConnectionApproval:
                    var login = new LoginCommand();
                    var gameRoom = GetGameRoomById(inc.ReadString());
                    login.Run(_managerLogger, this, inc, null, gameRoom);
                    break;
                case NetIncomingMessageType.Data:
                    Data(inc);
                    break;
            }
        }
    }

    private void Data(NetIncomingMessage inc)
    {
        var packetType = (PacketType)inc.ReadByte();
        var gameRoom = GetGameRoomById(inc.ReadString());
        var command = PacketFactory.GetCommand(packetType);
        command.Run(_managerLogger, this, inc, null, gameRoom);
    }


    public void SendNewPlayerEvent(string username, string gameGroupId)
    {
        if (NewPlayerEvent != null)
            NewPlayerEvent(this, new NewPlayerEventArgs(string.Format("{0}[{1}]", username, gameGroupId)));
    }

    public void KickPlayer(string username, string gameGroupId)
    {
        var command = PacketFactory.GetCommand(PacketType.Kick);
        var gameGroup = GetGameRoomById(gameGroupId);
        command.Run(_managerLogger, this, null, gameGroup.Players.FirstOrDefault(p => p.Player.Username == username), gameGroup);
    }

    private GameRoom GetGameRoomById(string id)
    {
        var gameRoom = _gameRooms.FirstOrDefault(g => g.GameRoomId == id);
        if (gameRoom == null)
        {
            gameRoom = new GameRoom(id, this, _managerLogger);
            _gameRooms.Add(gameRoom);
        }
        return gameRoom;
    }

    public void AddEnemy()
    {
        foreach (var gameRoom in _gameRooms)
        {
            gameRoom.AddEnemy();
        }
    }

}
