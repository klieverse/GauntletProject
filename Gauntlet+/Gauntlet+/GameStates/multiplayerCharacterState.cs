using Microsoft.Xna.Framework;

class MultiplayerCharacterState: GameObjectList
{
    protected Button elfButton, valkeryButton, wizardButton, warriorButton, startButton;
    protected PlayerStats elfStats, valkeryStats, wizardStats, warriorStats;
    string currentSelected, previousSelected;
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

        startButton = new Button("Sprites/Buttons/StartButton", 1);
        startButton.BeginPosition = new Vector2((GameEnvironment.Screen.X / 2- (startButton.Width / 2)), 700);
        Add(startButton);
        

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
            previousSelected = currentSelected;
            currentSelected = "Elf";
        }

        if (valkeryButton.Pressed && !valkeryChosen)
        {
            currentSelected = "Valkery";
        }

        if (wizardButton.Pressed && !wizardChosen)
        {
            currentSelected = "Wizard";
        }

        if (warriorButton.Pressed && !warriorChosen)
        {
            currentSelected = "Warrior";
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

        GameEnvironment.Connection.Send("CurrentSelected = " + currentSelected + " PreviousSelected = " + previousSelected);
        GameEnvironment.Connection.Update();
     


    }

    public void receiveMessage(string msg)
    {
        //CurrentSelected
        if (msg.Contains("CurrentSelected = Elf"))
        {
            elfChosen = true;
        }
        else if (msg.Contains("CurrentSelected = Valkery"))
        {
            valkeryChosen = true;
        }
        else if (msg.Contains("CurrentSelected = Wizard"))
        {
            wizardChosen = true;
        }
        else if (msg.Contains("CurrentSelected = Warrior"))
        {
            warriorChosen = true;
        }

        //PreviousSelected
        if (msg.Contains("PreviousSelected = Elf"))
        {
            elfChosen = false;
        }
        else if (msg.Contains("PreviousSelected = Valkery"))
        {
            valkeryChosen = false;
        }
        else if (msg.Contains("PreviousSelected = Wizard"))
        {
            wizardChosen = false;
        }
        else if (msg.Contains("PreviousSelected = Warrior"))
        {
            warriorChosen = false;
        }

    }
}




