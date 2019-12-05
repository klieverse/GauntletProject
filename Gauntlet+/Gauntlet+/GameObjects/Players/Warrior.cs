using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        : base(layer, id, start, level, speed: 350f, armor: 20f, magic: 2f, shotStrength: 1f, shotSpeed: 3f, melee: 2.5f)
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
