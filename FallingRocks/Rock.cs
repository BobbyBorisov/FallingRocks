using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingRocks
{
    public class Rock:GameObject
    {
        private Random random = new Random();
        
        public bool isVisible;

        public Rock(int boundaryX, int boundaryY)
        {
            this.BoundaryX = boundaryX;
            this.BoundaryY = boundaryY;
            isVisible = true;
            x = random.Next(0, boundaryX); //we are generating random x coordinate for the rock
            y = 0; // y is always 0 because the rock starts falling from the top
        }
        public int BoundaryX { get; set; }
        public int BoundaryY { get; set; }

        public override void Draw()
        {
            //draw rock on the screen
        }
    }
}
