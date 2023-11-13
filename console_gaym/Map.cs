using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace console_gaym
{
    internal class Map
    {
        int width, height;
        public char[,] TextMap;
        public Map(int width, int height) {
            this.width = width;
            this.height = height;
            TextMap = new char[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    TextMap[x, y] = Settings.EmptyMapSymbol;
        }
        public void DrawMap() //draw map to console
        {
            for (int y = 0;y < height;y++)
            {
                for (int x = 0;x < width;x++)
                    Console.Write(TextMap[x, y]);
                Console.WriteLine();
            }
        }
        public void AddSnake(Snake Oleg) //draw snake in map
        {
            ClearNear(Oleg.Cords.Last());
            foreach(var cord in Oleg.Cords)
            {
                TextMap[cord.X, cord.Y] = Settings.SnakeSymbol;
            }
        }
        //draw bonus
        public void AddFood(Coord food)
        {
            TextMap[food.X, food.Y] = Settings.FoodSymbol;
        }
        public void RemoveFood(Coord food)
        {
            TextMap[food.X, food.Y] = Settings.EmptyMapSymbol;
        }

        //очищаем символы вокруг заданного. Гораздо проще стереть последний сектор змеи, чем стирать всю таблицу каждый раз
        private void ClearNear(Coord goal)
        {
            if(goal.X >= 0 && goal.Y >= 0 && goal.X < width && goal.Y < height)
            {
                TextMap[goal.X, goal.Y] = Settings.EmptyMapSymbol;
                if (goal.X - 1 >= 0)
                    TextMap[goal.X - 1, goal.Y] = Settings.EmptyMapSymbol;
                if (goal.Y - 1 >= 0)
                    TextMap[goal.X, goal.Y - 1] = Settings.EmptyMapSymbol;
                if (goal.X + 1 < width)
                    TextMap[goal.X + 1, goal.Y] = Settings.EmptyMapSymbol;
                if (goal.Y + 1 < height)
                    TextMap[goal.X, goal.Y + 1] = Settings.EmptyMapSymbol;
            }
        }
    }
}
