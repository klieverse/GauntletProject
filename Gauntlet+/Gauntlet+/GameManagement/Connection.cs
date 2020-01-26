using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Collections.Generic;

public class Connection
{
    TcpClient client;
    NetworkStream networkStream;

    public Connection()
    {
        try
        {
            IPAddress ipAddress = IPAddress.Parse("25.62.226.197");
            int port = 8080;
            client = new TcpClient();
            client.Connect(ipAddress, port);
            networkStream = client.GetStream();

            ConsoleMessage = "You are online";
            multiplayerAllowed = true;
            //receivedMessages = new List<string>();
        }
        catch
        {
            ConsoleMessage = "Could not connect to server. Multiplayer is not available";
            multiplayerAllowed = false;
        }


    }

    public void Update()
    {
        //get received messages
        try
        {
            networkStream = client.GetStream();
            byte[] bytesFrom = new byte[client.ReceiveBufferSize];
            networkStream.Read(bytesFrom, 0, (int)client.ReceiveBufferSize);
            string dataFromServer = System.Text.Encoding.ASCII.GetString(bytesFrom);
            dataFromServer = dataFromServer.Substring(0, dataFromServer.IndexOf("$"));
            if (dataFromServer.Contains("CurrentSelected = "))
            {
                MultiplayerCharacterState.receiveMessage(dataFromServer);
            }
            else
            {
                MultiPlayerState.currentLevel.UpdateMultiplayer(dataFromServer);
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(" >> " + ex.ToString());
        }

    }

    public void Send(string msg)
    {
        //send string msg
        Byte[] sendBytes = null;
        string clientResponse = msg;
        sendBytes = Encoding.ASCII.GetBytes(clientResponse);
        networkStream.Write(sendBytes, 0, sendBytes.Length);
        networkStream.Flush();

    }

    public bool multiplayerAllowed
    {
        get;
        set;
    }

    public string ConsoleMessage
    {
        get;
        set;
    }
}


