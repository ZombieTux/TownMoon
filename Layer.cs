using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TownMoon
{
    class Layer
    {
        public StaticBlock[,] blocks;

        public Layer(int x, int y)
        {
            blocks = new StaticBlock[x, y];
        }

    }
}
