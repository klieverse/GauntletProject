using Microsoft.Xna.Framework;

class MultiplayerCharacterState: GameObjectList
{
    protected Button elfButton, valkeryButton, wizardButton, warriorButton, startButton;
    protected PlayerStats elfStats, valkeryStats, wizardStats, warriorStats;
    string currentSelected, message;
    bool elfChosen, valkeryChosen, wizardChosen, warriorChosen;

    public MultiplayerCharacterState()
    {

        elfButton = new Button("statSprites/ElfStats", 2); // create classbutton
        elfButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4, 200); //give the button a position
        elfStats = new PlayerStats(elfButton.BeginPosition, "Elf"); // create statsList for the class
        //add stats and button to the screen
        Add(elfStats);
        Add(elfButton);


        valkeryButton = new Button("statSprites/ValkeryStats", 2);
        valkeryButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4 * 3, 200);
        valkeryStats = new PlayerStats(valkeryButton.BeginPosition, "Valkery");
        Add(valkeryStats);
        Add(valkeryButton);

        wizardButton = new Button("statSprites/WizardStats", 2);
        wizardButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4, 500);
        wizardStats = new PlayerStats(wizardButton.BeginPosition, "Wizard");
        Add(wizardStats);
        Add(wizardButton);

        warriorButton = new Button("statSprites/WarriorStats", 2);
        warriorButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4 * 3, 500);
        warriorStats = new PlayerStats(warriorButton.BeginPosition, "Warrior");
        Add(warriorStats);
        Add(warriorButton);

        startButton = new Button("Sprites/Buttons/HelpButton", 1);
        startButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - (startButton.Width / 2)), 750);
        Add(startButton);

        message = "0000";

        elfChosen = false;
        valkeryChosen = false;
        wizardChosen = false;
        warriorChosen = false;


    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        //if a button is pressed set the fitting class and go to the level
        if (elfButton.Pressed && !elfChosen)
        {
            currentSelected = "Elf";
            message = "1000";
        }

        if (valkeryButton.Pressed && !valkeryChosen)
        {
            currentSelected = "Valkery";
            message = "0100";
        }

        if (wizardButton.Pressed && !wizardChosen)
        {
            currentSelected = "Wizard";
            message = "0010";
        }

        if (warriorButton.Pressed && !warriorChosen)
        {
            currentSelected = "Warrior";
            message = "0001";
        }

        if (startButton.Pressed && currentSelected != null)
        {
            GameEnvironment.SelectedClass = currentSelected;
            GameEnvironment.GameStateManager.SwitchTo("multiPlayerState");
        }

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //GameEnvironment.Connection.Send(message);
        //GameEnvironment.Connection.Update();
        /*
        if(GameEnvironment.Connection.receivedMSG.Substring(0, 15) == "Selected class:")
        {
            if(GameEnvironment.Connection.receivedMSG.Substring(15)=="1111")
            {
                GameEnvironment.SelectedClass = currentSelected;
                GameEnvironment.GameStateManager.SwitchTo("multiPlayerState");
            }
            else
            {
                if(GameEnvironment.Connection.receivedMSG.Substring(15)[0] == 1)
                {
                    elfChosen = true;
                }
                else
                {
                    elfChosen = false;
                }

                if (GameEnvironment.Connection.receivedMSG.Substring(15)[1] == 1)
                {
                    valkeryChosen = true;
                }
                else
                {
                    valkeryChosen = false;
                }

                if (GameEnvironment.Connection.receivedMSG.Substring(15)[2] == 1)
                {
                    wizardChosen = true;
                }
                else
                {
                    wizardChosen = false;
                }

                if (GameEnvironment.Connection.receivedMSG.Substring(15)[3] == 1)
                {
                    warriorChosen = true;
                }
                else
                {
                    warriorChosen = false;
                }
            }
        }
        */
     


    }
}




