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
        private static int MaximumNumberOfRocks = 15;
        static Random random = new Random();
        private static List<char> _possibleSymbols = new List<char>() { '^','*','%','&','$','#' }; //symbols for the rocks
        private static List<ConsoleColor> _possibleColors = new List<ConsoleColor>(){
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Yellow
        }; // possible colors
        private static List<ConsoleColor> colors = new List<ConsoleColor>();
        private static List<char> chars = new List<char>();

        private static void GetUserInput()
        {
            while (Console.KeyAvailable)
            {
                //get user input and according to the key move the dwarf(use its methods)
                while (Console.KeyAvailable) // key pressed
                {

                    //first read, then clear the buffer from old key
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    //moving the DWARF
                    if ((pressedKey.Key == ConsoleKey.LeftArrow) && ((_dwarf.x - 2) >= 0))
                    {
                        _dwarf.MoveLeft();
                    }
                    else if ((pressedKey.Key == ConsoleKey.RightArrow) && ((_dwarf.x + 1) <= WindowWidth - GameMenuWidth - 2))
                    {
                        _dwarf.MoveRight();
                    }
                }
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
                Console.SetCursorPosition(0, WindowHeight - 1);
                Console.Write(new string(' ',WindowWidth - GameMenuWidth));
                DrawGameMenu();
                _dwarf.Draw();
            }
            else
            {
                //gameover
                //wait for the user input and determine if to start a new game or exit this one

                _rocks.Clear();
                
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(19, 4);
                Console.Write("{0}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(17, 6);
                Console.Write("O,NO!");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(12, 8);
                Console.Write("YOU FAILED!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(15, 18);
                Console.Write("GAME OVER!!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(15, 22);
                Console.Write("Your score is {0}", gameScore);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(8, 25);
                Console.Write("press [ENTER] to play again");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(17, 6);
                Console.Write("press [ESCAPE] to exit");

                while (true)
                {
                    ConsoleKeyInfo result = Console.ReadKey();
                    if ((result.Key == ConsoleKey.Enter))
                    {
                        Console.Clear();
                        gameSpeed = 0d;
                        gameScore = 0;
                        Main();
                    }
                    else if ((result.Key == ConsoleKey.Escape))
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
        
        static void Main()
        {
            Console.BufferWidth = Console.WindowWidth = WindowWidth;
            Console.BufferHeight = Console.WindowHeight = WindowHeight;
            Console.Title = "Falling Rocks - Team Edmos";
            Console.CursorVisible = false;
            PrintIntro();
            ConsoleKeyInfo pressAnyKey = Console.ReadKey();
            if ((pressAnyKey.Key == ConsoleKey.Enter) || (pressAnyKey.Key != ConsoleKey.Enter))
            {
                Console.Clear();
                _dwarf = new Dwarf(WindowWidth - GameMenuWidth, WindowHeight, 5);
                GenerateNewRocks();
                while (true)
                {
                    System.Threading.Thread.Sleep(500 - 3 * (int)gameSpeed);
                    GetUserInput();
                    MoveRocks();
                    DetermineCollision();
                    Repaint();
                    gameSpeed = Math.Min(gameSpeed + Acceleration, MaximumGameSpeed);
                }
            }
        }

        private static void MoveRocks()
        {
            for (int p = 0; p < _rocks.Count; p++)
            {
                int check1 = _rocks[p].BoundaryY;
                int check = _rocks[p].BoundaryY + 4;
                if (_rocks[p].BoundaryY < 0)
                {
                    _rocks[p] = new Rock(_rocks[p].BoundaryX, _rocks[p].BoundaryY + 4);
                }
                else if (_rocks[p].BoundaryY == WindowHeight - 1)
                {
                    ClearRock(_rocks[p]);
                    _rocks[p] = new Rock(random.Next(0, WindowWidth - GameMenuWidth), 0);
                    colors[p] = _possibleColors[random.Next(0, _possibleColors.Count)];
                    chars[p] = _possibleSymbols[random.Next(0, _possibleSymbols.Count)];
                }
                else if (check > WindowHeight - 1)
                {
                    ClearRock(_rocks[p]);
                    _rocks[p] = new Rock(_rocks[p].x, WindowHeight - 1);
                }
                else
                {
                    ClearRock(_rocks[p]);
                    _rocks[p] = new Rock(_rocks[p].BoundaryX, _rocks[p].BoundaryY + 4);
                    GenerateRock(_rocks[p], chars[p], colors[p]);
                }
            }
            gameScore = gameScore + 9;
        }
        private static void GenerateRock(Rock rock, char symbol, ConsoleColor color)
        {
            Console.SetCursorPosition(rock.BoundaryX, rock.BoundaryY);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }
        private static void ClearRock(Rock rock, char ch = ' ', ConsoleColor color = ConsoleColor.Black)
        {
            Console.SetCursorPosition(rock.BoundaryX, rock.BoundaryY);
            Console.ForegroundColor = color;
            Console.Write(ch);
        }

        
        private static void GenerateNewRocks()
        {
            for (int i = 0; i < MaximumNumberOfRocks; i++)
            {
                _rocks.Add(new Rock(random.Next(0, WindowWidth - GameMenuWidth), 0 - i * 4));
                colors.Add(_possibleColors[random.Next(0, _possibleColors.Count)]);
                chars.Add(_possibleSymbols[random.Next(0, _possibleSymbols.Count)]);
            }
        }

        private static void DetermineCollision()
        {
            for (int i = 0; i < _rocks.Count; i++)
            {
                if ((_dwarf.x == _rocks[i].BoundaryX && _dwarf.y == _rocks[i].BoundaryY + 1)
                || (_dwarf.x + 1 == _rocks[i].BoundaryX && _dwarf.y == _rocks[i].BoundaryY + 1)
                || (_dwarf.x - 1 == _rocks[i].BoundaryX && _dwarf.y == _rocks[i].BoundaryY + 1))
                {
                    _dwarf.livesCount--;
                    _dwarf.isCollision = true;
                    break;
                }
            }
            if (_dwarf.isCollision)
            {
                _rocks.Clear();
                Console.Clear();
                GenerateNewRocks();
                _dwarf.Draw();
                System.Threading.Thread.Sleep(1000);
                _dwarf = new Dwarf(WindowWidth-GameMenuWidth, WindowHeight, _dwarf.livesCount);
                _dwarf.isCollision = false;
            }
        }
        private static void PrintIntro()
        {
            List<int>[] falling = new List<int>[] {
                new List<int> {20, 6, 4, 3, 5, 3, 5, 3, 5, 3, 2, 3, 3, 3, 4, 5},
                new List<int> {20, 3, 6, 5, 4, 3, 5, 3, 5, 3, 2, 4, 2, 3, 3, 3, 1, 3},
                new List<int> {20, 6, 2, 2, 3, 2, 3, 3, 5, 3, 5, 3, 2, 5, 1, 3, 2, 3},
                new List<int> {20, 3, 4, 9, 2, 3, 5, 3, 5, 3, 2, 3, 1, 5, 2, 3, 3, 4},
                new List<int> {20, 3, 4, 3, 3, 3, 2, 6, 2, 6, 2, 3, 2, 3, 2, 4, 3, 3, 2, 3},
                new List<int> {20, 3, 4, 3, 3, 3, 2, 6, 2, 6, 2, 3, 2, 3, 3, 3, 4, 6}};

            List<int>[] rocks = new List<int>[] {
                new List<int> {25, 7, 5, 5, 5, 5, 3, 3, 2, 3, 3, 6},
                new List<int> {25, 3, 2, 3, 3, 3, 1, 3, 3, 3, 1, 3, 2, 3, 1, 3, 3, 3, 2, 3},
                new List<int> {25, 3, 2, 3, 2, 3, 3, 3, 2, 3, 6, 6, 4, 4},
                new List<int> {25, 6, 4, 3, 3, 3, 2, 3, 6, 6, 8, 4},
                new List<int> {25, 3, 1, 3, 4, 3, 1, 3, 3, 3, 1, 3, 2, 3, 1, 3, 3, 3, 2, 3},
                new List<int> {25, 3, 2, 3, 4, 5, 5, 5, 3, 3, 2, 3, 3, 6}};

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < falling[i].Count; j++)
                {
                    if (j % 2 == 0)
                    {
                        Console.Write(new String(' ', falling[i][j]));
                    }
                    else
                    {
                        Console.Write(new String('*', falling[i][j]));
                    }
                }
                Console.WriteLine();
            }
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < rocks[i].Count; j++)
                {
                    if (j % 2 == 0)
                    {
                        Console.Write(new String(' ', rocks[i][j]));
                    }
                    else
                    {
                        Console.Write(new String('*', rocks[i][j]));
                    }
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(new String(' ', 42) + "by EDMOS TEAM");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(new String(' ', 37) + "PRESS ANY KEY TO PLAY!!!");
        }
    }
}