using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        /*if (quitButton.Pressed)
        {
            Reset();
            GameEnvironment.GameStateManager.SwitchTo("levelMenu");
        }*/
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        LoadPlayer();
        //Player player = Find("player") as Player;

        // check if we died

       
        // check if we ran out of time

        //for each existing player update the fitting statfield with the data of said player
        foreach (Player player in (Find("players") as GameObjectList).Children)
        {
                (Find(player.playerClass + "Stats") as PlayerStatField).Update(player);
        }
    }

    public void LoadPlayer()
    {
        string playerClass= GameEnvironment.SelectedClass; //Get the selected class
        // Check if the selected class is loaded, if not load the right player into the level
        if (Find(playerClass) == null)
        {

            if (playerClass == "Elf" )
            {
                //get the startPosition for the player
                Vector2 startPosition = new Vector2(((float)startPositionQuestor.X + 0.5f) * Tile.Size , (startPositionQuestor.Y + 1f) * Tile.Size);
                Questor questor = new Questor(4, "Elf", startPosition, this, true); //create the player
                (Find("players") as GameObjectList).Add(questor); //add player to level
                PlayerStatField questorStats = new PlayerStatField("Elf"); //create the players Statfield
                (Find("StatFields") as GameObjectList).Add(questorStats); //add the statfield to the level
            }
            if(playerClass == "Wizard")
            {
                Vector2 startPosition = new Vector2(((float)startPositionMerlin.X + 0.5f) * Tile.Size, (startPositionMerlin.Y + 1f) * Tile.Size);
                Merlin merlin = new Merlin(4, "Wizard", startPosition, this, true);
                (Find("players") as GameObjectList).Add(merlin);
                PlayerStatField wizardStats = new PlayerStatField("Wizard");
                (Find("StatFields") as GameObjectList).Add(wizardStats);
            }
            if(playerClass =="Warrior")
            {
                Vector2 startPosition = new Vector2(((float)startPositionThor.X + 0.5f) * Tile.Size, (startPositionThor.Y + 1f) * Tile.Size);
                Thor thor = new Thor(4, "Warrior", startPosition, this, true);
                (Find("players") as GameObjectList).Add(thor);
                PlayerStatField warriorStats = new PlayerStatField("Warrior");
                (Find("StatFields") as GameObjectList).Add(warriorStats);
            }
            if(playerClass == "Valkery")
            {
                Vector2 startPosition = new Vector2((startPositionThyra.X + 0.5f) * Tile.Size, (startPositionThyra.Y + 1f) * Tile.Size);
                Thyra thyra = new Thyra(4, "Valkery", startPosition, this, true);
                (Find("players") as GameObjectList).Add(thyra);
                PlayerStatField valkeryStats = new PlayerStatField("Valkery");
                (Find("StatFields") as GameObjectList).Add(valkeryStats);

            }
        }
    }

    public override void Reset()
    {
        base.Reset();
        
    }
}
