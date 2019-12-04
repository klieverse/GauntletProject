using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauntlet_.GameObjects.Players
{
    class Warrior : PlayerObject
    {
        public Warrior(int layer, string id, Vector2 start, Level level)
        : base(layer, id, start, level, 350f, 20f, 2f, 1f, 3f)
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
