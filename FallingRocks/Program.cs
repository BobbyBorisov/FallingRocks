using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FallingRocks
{
    class Program
    {
        private static int WindowWidth = 100;
        private static int WindowHeight = 50;
        private static int GameMenuWidth = 40;
        const double MaximumGameSpeed = 120d;  //The maximum speed the rocks can reach.
        const double Acceleration = 0.3d;      //How much the speed is increased after each moment.
        static double gameSpeed = 0d;          //Current speed.
        static ulong gameScore = 0ul;          //Current score.
        private static Dwarf _dwarf;
        private static List<Rock> _rocks = new List<Rock>();
        private static int MaximumNumberOfRocks = 9;
        static Random random = new Random();

        private static void GetUserInput()
        {
            while (Console.KeyAvailable)
            {
                //get user input and according to the key move the dwarf(use its methods)
            }
        }

        private static void DrawGameMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;  //Prints the current game difficulty.
            Console.SetCursorPosition(WindowWidth - GameMenuWidth + 10, (WindowHeight / 2) - 6);
            Console.Write("Difficulty: " + (int)((gameSpeed / 10d) + 1)); //Prints the current game score.
            Console.SetCursorPosition(WindowWidth - GameMenuWidth + 10, (WindowHeight / 2) - 3);
            Console.Write("Your score: " + gameScore);    //Prints the lives left.
            Console.SetCursorPosition(WindowWidth - GameMenuWidth + 10, (WindowHeight / 2));
            Console.Write("Lives left: " + _dwarf.livesCount);

        }

        private static void Repaint()
        {
            if (_dwarf.livesCount > 0)
            {
                Console.Clear();
                DrawGameMenu();
                _dwarf.Draw();

                foreach (var rock in _rocks)
                {
                    rock.Draw();
                }

                Thread.Sleep(100);
            }
            else
            {
                //gameover
                //wait for the user input and determine if to start a new game or exit this one
            }
        }

        
        static void Main(string[] args)
        {
            Console.BufferWidth = Console.WindowWidth = WindowWidth;
            Console.BufferHeight = Console.WindowHeight = WindowHeight;

            _dwarf = new Dwarf(WindowWidth, WindowHeight,10);
            while (true)
            {
                GetUserInput();
                MoveRocks();
                GenerateNewRocks();
                DetermineCollision();
                Repaint();

                //figure out how to exit from the cycle
            }
        }

        private static void MoveRocks()
        {
            var rocksIndexesToBeRemoved = new List<int>();

            //iterate over the rocks and move them down(Rock object has a method MoveDown()
            //check if rock is visible and add it for deletion

            //increase gameScore
            //increase gameSpeed by some acceleration and then use it to decrease Thread.Sleep argument in Repaint() method

            //remove rocks from the current array for deletion by indexes
        }

        
        private static void GenerateNewRocks()
        {
            //check whether we have the max number of rocks .. otherwise generate a new one and pass WindowsWidth and WIndowsHeight to
            //the constructor 
        }

        private static void DetermineCollision()
        {
            //determine collision between the _dwarf and rocks
        }
    }
}
