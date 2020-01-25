using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ChooseCharacterState : GameObjectList
{
    protected Button elfButton, valkeryButton, wizardButton, warriorButton, backButton;
    protected PlayerStats elfStats, valkeryStats, wizardStats, warriorStats;

    public ChooseCharacterState()
    {

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
        warriorButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4*3, 500);
        warriorStats = new PlayerStats(warriorButton.BeginPosition, "Warrior");
        Add(warriorStats);
        Add(warriorButton);

        // add a back but.ton
        backButton = new Button("Sprites/Exit", 100);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Width) / 2, 750);
        Add(backButton);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        //if a button is pressed set the matching class and go to the level
        if (elfButton.Pressed)
        {
            GameEnvironment.SelectedClass = "Elf";
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

        if (valkeryButton.Pressed)
        {
            GameEnvironment.SelectedClass = "Valkery";
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

        if (wizardButton.Pressed)
        {
            GameEnvironment.SelectedClass = "Wizard";
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

        if (warriorButton.Pressed)
        {
            GameEnvironment.SelectedClass = "Warrior";
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }

        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("titleMenu");
        }

    }
}

