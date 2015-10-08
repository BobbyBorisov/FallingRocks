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

        public override void Draw()
        {
            if (isCollision)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(x, y);
                Console.Write("X");
            }
            else
            {
                Console.ForegroundColor = _color;
                Console.SetCursorPosition(x - 1, y);
                Console.Write(_symbols);
            }
        }

        public void MoveLeft()
        {
            x = x - 1;
        }

        public void MoveRight()
        {
                x = x + 1;         
        }
    
    }
}
