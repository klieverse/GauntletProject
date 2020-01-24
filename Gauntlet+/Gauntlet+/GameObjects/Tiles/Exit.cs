using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


class Exit : Tile
{
    int lvlIndex;
    bool collided= false;

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
        
        //check playercollision
        List<GameObject> players = (GameWorld.Find("players") as GameObjectList).Children;
        if (players != null)
            foreach (Player player in players)
                if (CollidesWith(player))
                {
                    //(GameWorld.Find("players") as GameObjectList).Children.Remove(GameWorld.Find(GameEnvironment.SelectedClass));
                    //cj
                    if ((GameWorld as Level).secretValue2 == (GameWorld as Level).goalSecretValue2 ||
                            (GameWorld as Level).secretValue1 == (GameWorld as Level).goalSecretValue1)
                        PlayingState.HiddenLevel();
                    else 
                        PlayingState.NextLevel(lvlIndex);
                    if (collided == false)
                    GameEnvironment.AssetManager.PlaySound("Stage_Exit");
                    collided = true;
                }
    }
}