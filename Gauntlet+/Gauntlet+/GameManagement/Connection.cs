using System;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

public class Connection
{
    TcpClient client;
    NetworkStream stream;

    public Connection()
    {
        string serverIP = "127.0.0.1";
        int port = 8080;
        try
        {
            client = new TcpClient(serverIP, port);
            stream = client.GetStream();
        }
        catch
        {
            Console.WriteLine("Could not connect to server. Multiplayer is not available");
        }

        receivedMessages = new List<string>();
    }

    public void Update()
    {
        
            byte[] bytes = new byte[client.ReceiveBufferSize];
            
            //stream.Read(bytes, 0, (int)client.ReceiveBufferSize);
            
            string receivedMSG = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            
            receivedMessages.Add(receivedMSG);
            Console.WriteLine("Tot Hier");
        
        
    }

    public void Send(string msg)
    {
        int byteCount = Encoding.ASCII.GetByteCount(msg);

        byte[] sendData = new byte[byteCount];

        sendData = Encoding.ASCII.GetBytes(msg);

        stream.Write(sendData, 0, sendData.Length);
        Console.WriteLine("verzonden bericht");
    }

    public List<string> receivedMessages
    {
        get;
        set;
    }

    
}