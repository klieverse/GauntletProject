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
        /*if (quitButton.Pressed)
        {
            Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }*/
    }

    public override void Update(GameTime gameTime)
    {
        if(GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
        {
            foreach (GameObject obj in children)
            {
                //if(obj.Id != "players")
                {
                    obj.Update(gameTime);
                }
            }
            /*
            foreach (Player player in (Find("players") as GameObjectList).Children)
            {
                if (player.Id == GameEnvironment.SelectedClass)
                {
                    player.Update(gameTime);
                }


            }*/
        }
        else
        {
            base.Update(gameTime);
        }
        

        LoadPlayer();
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

    public void LoadPlayer()
    {
        string playerClass= GameEnvironment.SelectedClass; //Get the selected class
        // Check if the selected class is loaded, if not load the right player into the level
        //if multiplayer is chosen, all characters will be loaded
        if (Find(playerClass) == null)
        {

            if (playerClass == "Elf" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                //get the startPosition for the player
                Vector2 startPosition = new Vector2(((float)startPositionQuestor.X + 0.5f) * Tile.Size, (startPositionQuestor.Y + 1) * Tile.Size);
                Questor questor = new Questor(4, "Elf", startPosition, this, true); //create the player
                (Find("players") as GameObjectList).Add(questor); //add player to level
                int statPosition = GameEnvironment.Screen.X / 2 + 272;//get position for the statfield
                PlayerStatField questorStats = new PlayerStatField("Elf", statPosition); //create the players Statfield
                (Find("StatFields") as GameObjectList).Add(questorStats); //add the statfield to the level
            }
            if(playerClass == "Wizard" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                Vector2 startPosition = new Vector2(((float)startPositionMerlin.X + 0.5f) * Tile.Size, (startPositionMerlin.Y + 1f) * Tile.Size);
                Merlin merlin = new Merlin(4, "Wizard", startPosition, this, true);
                (Find("players") as GameObjectList).Add(merlin);
                int statPosition = GameEnvironment.Screen.X / 2;
                PlayerStatField wizardStats = new PlayerStatField("Wizard", statPosition);
                (Find("StatFields") as GameObjectList).Add(wizardStats);
            }
            if(playerClass =="Warrior" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                Vector2 startPosition = new Vector2(((float)startPositionThor.X) * Tile.Size, (startPositionThor.Y) * Tile.Size);
                Thor thor = new Thor(4, "Warrior", startPosition, this, true);
                (Find("players") as GameObjectList).Add(thor);
                int statPosition = GameEnvironment.Screen.X / 2 - 2* 272;
                PlayerStatField warriorStats = new PlayerStatField("Warrior", statPosition);
                (Find("StatFields") as GameObjectList).Add(warriorStats);
            }
            if(playerClass == "Valkery" || GameEnvironment.GameStateManager.CurrentGameState == GameEnvironment.GameStateManager.GetGameState("multiPlayerState"))
            {
                Vector2 startPosition = new Vector2((startPositionThyra.X + 2) * Tile.Size, (startPositionThyra.Y + 2) * Tile.Size);
                Thyra thyra = new Thyra(4, "Valkery", startPosition, this, true);
                (Find("players") as GameObjectList).Add(thyra);
                int statPosition = GameEnvironment.Screen.X / 2 - 272;
                PlayerStatField valkeryStats = new PlayerStatField("Valkery", statPosition);
                (Find("StatFields") as GameObjectList).Add(valkeryStats);

            }
        }
    }

    public override void Reset()
    {
        base.Reset();
    }
}
