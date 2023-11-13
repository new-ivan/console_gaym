using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace console_gaym
{
    public class Game
    {
        bool stopgame = false;
        Thread readthread; //поток для ввода с асинхронностью
        int ticks = 0;
        int width = Settings.MapWidth;
        int height = Settings.MapHeight;
        public Snake Oleg;
        Map Map;
        bool ispause;
        Coord FoodCords;
        bool createfood = false;
        Random random;

        //starts the game
        public async Task Start()
        {
            random = new Random();
            stopgame = false;
            readthread.Start();
            Oleg = new Snake(width, height);
            Map = new Map(width, height);
            ispause = false;
            FoodCords = new(random.Next(1,Settings.MapWidth),random.Next(1,Settings.MapHeight));

            while (!stopgame)
            {
                Thread.Sleep(Settings.TickSpeed); //await some time and raise Tick func
                Tick();
            }

        }
        public Game()
        {
            readthread = new Thread(() => ReadKeyAsync());
            
        }

        //Tick func, updates everything
        public void Tick()
        {
            if(!ispause)
                ticks++;
            if (ticks % Settings.TicksUntillUpdate == 0 && ticks != 0)
                GameTick();
        }
        public void GameTick()
        {
            Oleg.Move(FoodCords);
            if (!Oleg.isalive)
            {
                EndGame();
                return;
            }
            if (Oleg.ate)
            {
                Oleg.Grow();
                Map.RemoveFood(FoodCords);
                createfood = true;
            }
            Map.AddSnake(Oleg);
            Map.AddFood(FoodCords);
            Console.Clear();
            Map.DrawMap();
            

            if(createfood)
            {
                createfood = false;
                FoodCords = new(random.Next(0,Settings.MapWidth),random.Next(0,Settings.MapHeight));
            }
        }

        private void EndGame()
        {
            Console.Clear();
            stopgame = true;
            Console.WriteLine("Game over.");
        }
        private void ReadKeyAsync() // функция для потока, считывает ввод с асинхронщиной
        {
            int readedkey = 0;
            while (!stopgame)
            {
                readedkey = (int)Console.ReadKey().Key;

                switch (readedkey)
                {
                    //end
                    case (int)ConsoleKey.Escape:
                        EndGame();
                        break;

                    case (int)ConsoleKey.DownArrow:
                    case (int)ConsoleKey.S:
                        Oleg.Turn(0, 1);
                        break;

                    case (int)ConsoleKey.UpArrow:
                    case (int)ConsoleKey.W:
                        Oleg.Turn(0, -1);
                        break;

                    case (int)ConsoleKey.LeftArrow:
                    case (int)ConsoleKey.A:
                        Oleg.Turn(-1, 0);
                        break;

                    case (int)ConsoleKey.RightArrow:
                    case (int)ConsoleKey.D:
                        Oleg.Turn(1, 0);
                        break;

                    case (int)ConsoleKey.P:
                        ispause = !ispause;
                        break;

                    //дальше идут отладочные
                    case (int)ConsoleKey.Enter:
                        Map.DrawMap();
                        break;

                    case (int)ConsoleKey.Delete:
                        Console.Clear();
                        break;

                    case (int)ConsoleKey.T:
                        Console.WriteLine("уже прошло {0} тиков", ticks);
                        break;

                    case (int)ConsoleKey.H:
                        Oleg.Move(FoodCords);
                        Map.AddSnake(Oleg);
                        break;

                    case (int)ConsoleKey.OemPlus: 
                        Oleg.Grow();
                        break;

                    default:
                        break;

                }
            }
        }
    }
}
