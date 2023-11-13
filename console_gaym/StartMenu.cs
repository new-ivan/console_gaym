using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_gaym
{
    public class StartMenu
    {
        public enum MenuStatus
        {
            status_forceOut,
            status_startGame,
        }
        public MenuStatus ShowMenu()
        {
            Console.WriteLine("Нажмите enter для начала игры или esc для выхода");
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        return MenuStatus.status_startGame;
                    case ConsoleKey.Escape:
                        return MenuStatus.status_forceOut;
                    default:
                        break;
                }
            }           
        }
    }
}
