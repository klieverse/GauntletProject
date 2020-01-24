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

            Console.WriteLine(" >> " + "Client Started");
            multiplayerAllowed = true;
            //receivedMessages = new List<string>();
        }
        catch
        {
            Console.WriteLine("Could not connect to server. Multiplayer is not available");
            multiplayerAllowed = false;
        }
        
        
    }

    public void Update()
    {
        //get received messages
        try
        {
            networkStream = client.GetStream();
            byte[] buffer = new byte[10025];
            int byte_count = networkStream.Read(buffer, 0, buffer.Length);

            if (byte_count != 0)
            {
                string dataFromServer = Encoding.ASCII.GetString(buffer, 0, byte_count);
                if (dataFromServer.Contains("CurrentSelected = "))
                {
                    MultiplayerCharacterState.receiveMessage(dataFromServer);
                }
                else
                {
                    receivedMessages = new List<string>();
                    int newPosition = 0;
                    for(int i = 0; i < dataFromServer.Length - 1; i++)
                    {
                        if(dataFromServer[i] == '}' && dataFromServer[i+1] == '{')
                        {
                            int previousPosition = newPosition;
                            newPosition = i + 1 ;
                            MultiPlayerState.currentLevel.UpdateMultiplayer(dataFromServer.Substring(previousPosition, newPosition-previousPosition));
                        }
                    }
                    int endPosition = newPosition;
                    newPosition = dataFromServer.Length;
                    MultiPlayerState.currentLevel.UpdateMultiplayer(dataFromServer.Substring(endPosition, newPosition - endPosition));
                }
            }

            
            

            
            
            //receivedMessages.Add(dataFromServer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(" >> " + ex.ToString());
        }

    }

    //send the incoming string if it is not null
    public void Send(string msg)
    {
        if(msg != "null" && msg != null)
        {
            Byte[] sendBytes = null;
            string clientResponse = msg;
            sendBytes = Encoding.ASCII.GetBytes(clientResponse);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }
    }

    public List<string> receivedMessages
    {
        get;
        set;
    }

    public bool multiplayerAllowed
    {
        get;
        set;
    }
}


