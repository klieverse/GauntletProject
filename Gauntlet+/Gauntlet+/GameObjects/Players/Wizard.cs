using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauntlet_.GameObjects.Players
{
    class Wizard : PlayerObject
    {
        public Wizard(int layer, string id, Vector2 start, Level level)
        : base(layer, id, start, level, 300f, 0f, 3f, 1.5f, 3.5f)
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
