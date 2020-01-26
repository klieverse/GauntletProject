using Microsoft.Xna.Framework;
using System.Collections.Generic;

class MultiplayerCharacterState: GameObjectList
{
    protected Button elfButton, valkeryButton, wizardButton, warriorButton, startButton, backButton;
    protected PlayerStats elfStats, valkeryStats, wizardStats, warriorStats;
    string currentSelected, previousSelected;
    public static bool elfChosen, valkeryChosen, wizardChosen, warriorChosen;

    public MultiplayerCharacterState()
    {
        ChosenCharacters = new List<string>();

        SpriteGameObject background = new SpriteGameObject("Backgrounds/spr_settings", 0, "background");
        Add(background);

        elfButton = new Button("statSprites/ElfStats", 2); // create classbutton
        elfButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4, 220); //give the button a position
        elfStats = new PlayerStats(elfButton.BeginPosition, "Elf"); // create statsList for the class
        //add stats and button to the screen
        Add(elfStats);
        Add(elfButton);


        valkeryButton = new Button("statSprites/ValkeryStats", 2);
        valkeryButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4 * 3, 220);
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

        // add a back but.ton
        backButton = new Button("Sprites/Exit", 100);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Width) / 2, 750);
        Add(backButton);


    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        //if a button is pressed set the fitting class and go to the level
        if (elfButton.Pressed && !ChosenCharacters.Contains("Elf"))
        {
            previousSelected = currentSelected;
            currentSelected = "Elf";
        }

        if (valkeryButton.Pressed && !ChosenCharacters.Contains("Valkery"))
        {
            previousSelected = currentSelected;
            currentSelected = "Valkery";
        }

        if (wizardButton.Pressed && !ChosenCharacters.Contains("Wizard"))
        {
            previousSelected = currentSelected;
            currentSelected = "Wizard";
        }

        if (warriorButton.Pressed && !ChosenCharacters.Contains("Warrior"))
        {
            previousSelected = currentSelected;
            currentSelected = "Warrior";
        }

        if (startButton.Pressed && currentSelected != null)
        {
            GameEnvironment.SelectedClass = currentSelected;
            GameEnvironment.GameStateManager.SwitchTo("multiPlayerState");
        }

        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        GameEnvironment.Connection.Send("CurrentSelected = " + currentSelected + " PreviousSelected = " + previousSelected);
        previousSelected = "";
        GameEnvironment.Connection.Update();
     


    }

    public static void receiveMessage(string msg)
    {
        //CurrentSelected
        if (msg.Contains("CurrentSelected = Elf"))
        {
            if(!ChosenCharacters.Contains("Elf"))
            {
                ChosenCharacters.Add("Elf");
            }
        }
        else if (msg.Contains("CurrentSelected = Valkery"))
        {
            if (!ChosenCharacters.Contains("Valkery"))
            {
                ChosenCharacters.Add("Valkery");
            }
        }
        else if (msg.Contains("CurrentSelected = Wizard"))
        {
            if (!ChosenCharacters.Contains("Wizard"))
            {
                ChosenCharacters.Add("Wizard");
            }
        }
        else if (msg.Contains("CurrentSelected = Warrior"))
        {
            if (!ChosenCharacters.Contains("Warrior"))
            {
                ChosenCharacters.Add("Warrior");
            }
        }

        //PreviousSelected
        if (msg.Contains("PreviousSelected = Elf"))
        {
            if (ChosenCharacters.Contains("Elf"))
            {
                ChosenCharacters.Remove("Elf");
            }
        }
        else if (msg.Contains("PreviousSelected = Valkery"))
        {
            if (ChosenCharacters.Contains("Valkery"))
            {
                ChosenCharacters.Remove("Valkery");
            }
        }
        else if (msg.Contains("PreviousSelected = Wizard"))
        {
            if (ChosenCharacters.Contains("Wizard"))
            {
                ChosenCharacters.Remove("Wizard");
            }
        }
        else if (msg.Contains("PreviousSelected = Warrior"))
        {
            if (ChosenCharacters.Contains("Warrior"))
            {
                ChosenCharacters.Remove("Warrior");
            }
        }

    }

    public static List<string> ChosenCharacters
    {
        get;
        set;
    }
}




