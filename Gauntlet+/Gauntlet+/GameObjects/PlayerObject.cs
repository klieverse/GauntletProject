using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauntlet_.GameObjects
{
    class PlayerObject : AnimatedGameObject
    {
        protected List<PlayerObject> players;

        public PlayerObject(int layer = 0, string id = "")
        : base(layer, id)
        {
            players = new List<PlayerObject>();
        }
    }
}
