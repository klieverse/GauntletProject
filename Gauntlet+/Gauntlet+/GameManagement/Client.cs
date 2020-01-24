using Lidgren.Network;

public class Client
{
    public Client()
    {
        var config = new NetPeerConfiguration("gauntlet server");
        var client = new NetClient(config);
        client.Start();
        client.Connect(host: "127.0.0.1", port: 12345);
    }

    public void Update()
    {
        NetIncomingMessage message;
        while ((message = peer.ReadMessage()) != null)
        {
            switch (message.MessageType)
            {
                case NetIncomingMessageType.Data:
                    // handle custom messages
                    var data = message.Read * ();
                    break;

                case NetIncomingMessageType.StatusChanged:
                    // handle connection status messages
                    switch (message.SenderConnection.Status)
                    {
                        /* .. */
                    }
                    break;

                case NetIncomingMessageType.DebugMessage:
                    // handle debug messages
                    // (only received when compiled in DEBUG mode)
                    Console.WriteLine(message.ReadString());
                    break;

                /* .. */
                default:
                    Console.WriteLine("unhandled message with type: "
                        + message.MessageType);
                    break;
            }
        }
    }
}

    
