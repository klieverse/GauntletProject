using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

class MultiPlayerState : PlayingState
{ 

    public MultiPlayerState(ContentManager content): base(content)
    {
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        //sends current data to the server
        CurrentLevel.SendInformation();
        //client retreives data from the server
        GameEnvironment.Connection.Update();
        //Console.WriteLine("tot hier");
        //client changes data in game according to what is retreived from the server
        for(int i = 0;  i < GameEnvironment.Connection.receivedMessages.Count;  i++)
        {
            string msg = GameEnvironment.Connection.receivedMessages[0];

            CurrentLevel.UpdateMultiplayer(msg);
            GameEnvironment.Connection.receivedMessages.Remove(msg);
        }
    }
}

