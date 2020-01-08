using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ChooseCharacterState : GameObjectList
{
    protected Button elfButton, valkeryButton, wizardButton, warriorButton;
    public ChooseCharacterState()
    {
        elfButton = new Button("statSprites/ElfStats", 2);
        elfButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4, 200);
        Add(elfButton);

        valkeryButton = new Button("statSprites/ValkeryStats", 2);
        valkeryButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4 * 3, 200);
        Add(valkeryButton);

        wizardButton = new Button("statSprites/WizardStats", 2);
        wizardButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4, 500);
        Add(wizardButton);

        warriorButton = new Button("statSprites/WarriorStats", 2);
        warriorButton.BeginPosition = new Vector2((GameEnvironment.Screen.X - elfButton.Width) / 4*3, 500);
        Add(warriorButton);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
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

    }
}

