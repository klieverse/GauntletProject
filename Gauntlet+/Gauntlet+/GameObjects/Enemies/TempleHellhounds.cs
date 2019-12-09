using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TempleHellhounds : SpawnObject
{
    
    public TempleHellhounds(Vector2 startPosition) : base(startPosition)
    {
        position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
       
    }

    public override void Spawn()
    {
        (GameWorld.Find("enemies") as GameObjectList).Add( new Hellhound(spawnLocation));
    }
}

