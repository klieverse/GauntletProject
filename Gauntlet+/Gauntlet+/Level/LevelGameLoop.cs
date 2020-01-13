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

        //Player player = Find("player") as Player;

        // check if we died

        /*
        // check if we ran out of time
        foreach (Player player in (Find("players") as GameObjectList).Children)
        {
            (Find(player.playerClass +"Stats") as PlayerStatField).Update(player);
        }
        */
        (Find("ElfStats") as PlayerStatField).Update(Find("Elf") as Player);
    }

    public override void Reset()
    {
        base.Reset();
        
    }
}
