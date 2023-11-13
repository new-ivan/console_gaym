using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_gaym
{
    public class Snake//oleg
    {
        public List<Coord> Cords { get; set; }
        bool isadding = false;
        int dx, dy, maxx, maxy;
        public bool isalive;
        public Snake(int maxx, int maxy)
        {
            Cords = new List<Coord>
            {
                new Coord(0, 0),
                new Coord(1, 0)
            };
            this.maxx = maxx;
            this.maxy = maxy;
            isalive = true;

        }


        //когда змейка двигается, то все её прошлые сегменты встают на место следующего, а первый двигается вперёд
        public void Move()
        {
            for(int i = Cords.Count - 1; i > 0; i--)
            {
                if(isadding && i == Cords.Count - 1)
                { //если змейка растёт, то её последний двойной сегмент остается на месте
                    isadding = false;
                    continue;
                }
                Cords[i].X = Cords[i - 1].X;
                Cords[i].Y = Cords[i - 1].Y;
            }
            Cords.First().X += dx;
            Cords.First().Y += dy;

            if(Cords.First().X < 0 || Cords.First().X >= maxx || Cords.First().Y < 0 || Cords.First().Y >= maxy)
                isalive = false;
        }
        public void Turn(int dx, int dy)
        {
            this.dx = dx;
            this.dy = dy;
        }
        public void Grow() //добавляем ещё один сегмент в тех же координатах, что и прошлый
        {
            isadding = true;
            Cords.Add(Cords.Last());
        }
    }
}
