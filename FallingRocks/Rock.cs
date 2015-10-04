using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingRocks
{
    public class Rock:GameObject
    {
        private static List<char> _possibleSymbols = new List<char>() { '^','*','%','&','$','#' };
        private static List<ConsoleColor> _possibleColors = new List<ConsoleColor>(){
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Yellow
        };

        private ConsoleColor _color;
        private char _symbol;
        private int _boundaryX;
        private int _boundaryY;
        private Random random = new Random();
        
        public bool isVisible;

        public Rock(int boundaryX, int boundaryY)
        {
            _boundaryX = boundaryX;
            _boundaryY = boundaryY;
            isVisible = true;
            x = random.Next(0, boundaryX); //we are generating random x coordinate for the rock
            y = 0; // y is always 0 because the rock starts falling from the top
            _symbol = _possibleSymbols[random.Next(0, _possibleSymbols.Count)]; // getting random symbol from the array
            _color = _possibleColors[random.Next(0, _possibleColors.Count)]; // same for color
        }

        public void MoveDown()
        {
            //determine until when the rock will increase y coordinate
            //hint: see rock properties
            //and set its property isVisible to false after that
        }

        public override void Draw()
        {
            Console.ForegroundColor = _color;
            Console.SetCursorPosition(x, y);
            Console.Write(_symbol);
        }
    }
}
