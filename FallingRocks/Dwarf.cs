using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingRocks
{
    public class Dwarf: GameObject
    {
        private const string _symbols = "{0}";
        private ConsoleColor _color;
        private int _boundaryX;
        public int livesCount;
        public bool isCollision;

        public Dwarf(int boundaryX, int boundaryY,int countOfLives){
            x = boundaryX/2;
            y = boundaryY-1;
            livesCount = countOfLives;
            _color = ConsoleColor.White;
        }

        public override void Draw ()
        {
            //if we have collided draw some symbol at the middle of our dwarf
            //otherwise set cursor at position x-1,y because the dwarf is 3 symbols {0} and the x position is the middle one
            // use Console.ForegroundColor to set the color;
            // use Console.SetCursorPosition;
            // then write the symbol to the console
        }

        public void MoveLeft()
        {
            //move left
        }

        public void MoveRight()
        {
            //move right
        }
    
    }
}
