using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingRocks
{
    public abstract class GameObject
    {
        public static int x { set; get; }
        public static int y { set; get; }

        public abstract void Draw();
    }
}
