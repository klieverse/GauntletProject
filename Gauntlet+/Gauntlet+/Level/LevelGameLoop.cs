using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;


partial class Level : GameObjectList
{

    public override void HandleInput(InputHelper inputHelper)
    {
        if (GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
        {
            for (int i = children.Count - 1; i >= 0; i--)
            {
                if(children[i].Id != "players")
                {
                    children[i].HandleInput(inputHelper);
                }
            }
            foreach (Player player in (Find("players") as GameObjectList).Children)
            {
                if (player.Id == GameEnvironment.SelectedClass)
                {
                    player.HandleInput(inputHelper);
                }
            }
        }
        else
        {
            base.HandleInput(inputHelper);
        }
        if (quitButton.Pressed)
        {
            PlayingState.Exit();
        }
    }

    public override void Update(GameTime gameTime)
    {
        
        if (GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
        {
            foreach (GameObject obj in children)
            {
                if(obj.Id != "players")
                {
                    obj.Update(gameTime);
                }
            }
            foreach (Player player in (Find("players") as GameObjectList).Children)
            {
                if (player.Id == GameEnvironment.SelectedClass)
                {
                    player.Update(gameTime);
                }
            }
        }
        else
        {
            base.Update(gameTime);
        }
        
        
        //Player player = Find("player") as Player;

        // check if we died
        

        //update the fitting statfield with the data of the current player
        foreach (Player player in (Find("players") as GameObjectList).Children)
        {
            if(player.Id == GameEnvironment.SelectedClass)
            {
                (Find(player.playerClass + "Stats") as PlayerStatField).Update(player);
            }
        }
    }
    
    public override void Reset()
    {
        
        (Find("players") as GameObjectList).Children.Clear();
        (Find("StatFields") as GameObjectList).Children.Clear();
        (Find("enemies") as GameObjectList).Children.Clear();
        (Find("playershot") as GameObjectList).Children.Clear();
        (Find("enemieShot") as GameObjectList).Children.Clear();
        base.Reset();
        ReloadEnemies();
        
    }
    

}
