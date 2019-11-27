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
        

        // check if we ran out of time
        
        
    }

    public override void Reset()
    {
        base.Reset();
        
    }
}
