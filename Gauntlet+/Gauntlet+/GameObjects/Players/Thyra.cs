using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Thyra : Player
    {
        public Thyra(int layer, string id, Vector2 start, Level level, bool isYou)
        : base(layer, id, start, level, speed: 350f, armor: 30f, magic: 1.5f, shotStrength: 1f, shotSpeed: 3f, melee: 2f, isYou)
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

