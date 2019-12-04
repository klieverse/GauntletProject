using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauntlet_.GameObjects.Players
{
    class Valkyrie : PlayerObject
    {
        public Valkyrie(int layer, string id, Vector2 start, Level level)
        : base(layer, id, start, level, 350f, 30f, 3f, 1f, 3f)
        {
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }
    }
}
