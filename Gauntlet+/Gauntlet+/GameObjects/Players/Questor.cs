using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Questor : Player
    {
        public Questor(int layer, string id, Vector2 start, Level level, bool isYou)
        : base(layer, id, start, level, speed:500f, armor:10f, magic: 3f, shotStrength:1f, shotSpeed:3.5f, melee:1.5f, isYou)
        {
        playerClass = "Elf";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }