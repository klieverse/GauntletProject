using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Merlin : Player
    {
        public Merlin(int layer, string id, Vector2 start, Level level, bool isyou)
        : base(layer, id, start, level, speed: 300f, armor: 0f, magic: 3f, shotStrength: 1.5f, shotSpeed: 3.5f, melee: 1f, isyou)
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

    public override void LoadAnimations()
    {
        LoadAnimation("Sprites/Player/spr_" + id + "idle@4", "idle", true);
        LoadAnimation("Sprites/Player/spr_" + id + "run@4", "run", true);
        LoadAnimation("Sprites/Player/spr_" + id + "shoot@3", "shoot", true);
        LoadAnimation("Sprites/Player/spr_" + id + "die@4", "die", false);
    }

}

