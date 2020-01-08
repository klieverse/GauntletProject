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

        foreach (Player player in (Find("players") as GameObjectList).Children)
        {
            
                (Find(player.playerClass + "Stats") as PlayerStatField).Update(player);
           
        }
        
        
        //(Find("ElfStats") as PlayerStatField).Update(Find("Elf") as Player);
    }

    public void LoadPlayer()
    {
        string playerClass= GameEnvironment.SelectedClass;
        if (Find(playerClass) == null)
        {
            if (playerClass == "Elf" )
            {
                Vector2 startPosition = new Vector2(((float)startPositionQuestor.X + 0.5f) * 55, (startPositionQuestor.Y + 1) * 55);
                Questor questor = new Questor(2, "Elf", startPosition, this, true);
                (Find("players") as GameObjectList).Add(questor);
                PlayerStatField questorStats = new PlayerStatField("Elf");
                (Find("StatFields") as GameObjectList).Add(questorStats);
            }
            if(playerClass == "wizard")
            {
                Vector2 startPosition = new Vector2(((float)startPositionMerlin.X + 0.5f) * 55, (startPositionMerlin.Y + 1) * 55);
                Merlin merlin = new Merlin(2, "Wizard", startPosition, this, true);
                (Find("players") as GameObjectList).Add(merlin);
                PlayerStatField wizardStats = new PlayerStatField("Wizard");
                (Find("StatFields") as GameObjectList).Add(wizardStats);
            }
            if(playerClass =="Warrior")
            {
                Vector2 startPosition = new Vector2(((float)startPositionThor.X + 0.5f) * 55, (startPositionThor.Y + 1) * 55);
                Thor thor = new Thor(2, "Warrior", startPosition, this, true);
                (Find("players") as GameObjectList).Add(thor);
                PlayerStatField warriorStats = new PlayerStatField("Warrior");
                (Find("StatFields") as GameObjectList).Add(warriorStats);
            }
            if(playerClass == "Valkery")
            {
                Vector2 startPosition = new Vector2(((float)startPositionThyra.X + 0.5f) * 55, (startPositionThyra.Y + 1) * 55);
                Thyra thyra = new Thyra(2, "Valkery", startPosition, this, true);
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
