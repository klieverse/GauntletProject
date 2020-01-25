using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


class Exit : Tile
{
    int lvlIndex;

    public Exit(int layer, string id, Vector2 position, int lvlIndex)
    : base("Tiles/Exit", TileType.Exit, layer, id)
    {
        this.lvlIndex = lvlIndex;
        this.position = position;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        CheckCollision();
    }

    private void CheckCollision()
    {
        bool Next = false;
        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                    visible = false;
                    Next = true;
                    GameEnvironment.AssetManager.PlaySound("Stage_Exit");
                }

        if (Next)
        {
            PlayingState.NextLevel(lvlIndex);
        }
    
    }
}