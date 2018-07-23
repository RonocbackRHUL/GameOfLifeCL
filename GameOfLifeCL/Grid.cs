

namespace GameOfLife
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    enum Status { Dead, Alive}
    
    public class Grid
    {
        Square[,] grid;
        int max;
        public Grid(int size)
        {
            grid = new Square[size, size];
            max = size;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {

                    Square sq = new Square(Status.Alive);
                    sq.randomizeStat();
                    grid[i, j] = sq;
                }
            }
            printGrid();
        }

        public void updateGrid()
        {

        }

        public void printGrid()
        {
            int i = 0;
            foreach(Square cur in grid)
            {
                i++;
                if(i >= max)
                {
                    Console.WriteLine("");
                    i = 0;
                }
                if(cur.status == Status.Alive)
                {
                    Console.Write("X");
                }
                else
                {
                    Console.Write("O");
                }
            }
        }
    }

    
    class Square
    {

        public Status status { get; set; }

        public Square(Status s)
        {
            status = s;
        }

        public void randomizeStat()
        {
            Random rnd = new Random();
            double val = rnd.NextDouble();
            if(val > 0.3)
            {
                status = Status.Alive;
            }
            else
            {
                status = Status.Dead;
            }
        }
    }

}