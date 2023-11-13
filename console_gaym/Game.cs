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

        //starts the game
        public async Task Start()
        {
            stopgame = false;
            readthread.Start();
            Oleg = new Snake(width, height);
            Map = new Map(width, height);
            ispause = false;

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
            Oleg.Move();
            if (!Oleg.isalive)
            {
                EndGame();
                return;
            }
            Map.AddSnake(Oleg);
            Console.Clear();
            Map.DrawMap();
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
                        Console.WriteLine("уже прошло {0} тиков",ticks); 
                        break;

                    case (int)ConsoleKey.G:
                        Oleg.Move();
                        Map.AddSnake(Oleg);
                        break;

                    default:
                        break;

                }
            }
        }
    }
}
