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
            int port = 8000;
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
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[10025];
            int byte_count = stream.Read(buffer, 0, buffer.Length);

            if (byte_count != 0)
            {
                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                if (data.Contains("CurrentSelected = ") && GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiplayerCharacterState"))
                {
                    MultiplayerCharacterState.receiveMessage(data);
                }
                else
                {
                    List<string> messages = RetrieveStrings(data);
                    foreach (string message in messages)
                    {
                        MultiPlayerState.currentLevel.UpdateMultiplayer(message);
                    }

                }
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
        if (msg != "null")
        {
            Byte[] sendBytes = null;
            string clientResponse = msg;
            sendBytes = Encoding.ASCII.GetBytes(clientResponse);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }
    }

    public List<string> RetrieveStrings(string data)
    {
        List<string> ret = new List<string>();
        int start = 0;
        int end = -1;
        for (int i = 0; i < data.Length - 1; i++)
        {
            if (data[i] == '}' && data[i + 1] == '{')
            {
                start = end + 1;
                end = i;
                string jsonConform = data.Substring(start, end - start + 1);
                ret.Add(jsonConform);
            }
        }
        if (data[data.Length - 1] == '}')
        {
            start = end + 1;
            end = data.Length - 1;
            string last = data.Substring(start, end - start + 1);
            ret.Add(last);
        }
        return ret;
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


