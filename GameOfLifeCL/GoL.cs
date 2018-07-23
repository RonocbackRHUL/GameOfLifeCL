using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public enum Status { Dead, Alive }
    public class GameOfLife
    {
        static void Main()
        {
            Grid g = new Grid(100);
            g.cycleXTimes(15);
        }

    }

    public class Grid
    {
        Square[,] grid;
        Square[,] buffGrid;
        int max;
        Random rnd = new Random();
        int gen = 1;

        public Grid(int size)
        {
            grid = new Square[size, size];
            buffGrid = new Square[size, size];
            max = size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    Square sq = new Square(Status.Dead);
                    sq.randomizeStat(rnd);
                    grid[i, j] = sq;
                }
            }
            printGrid();
        }

        public Status getSquare(int i, int j)
        {
            if(i < 0 || j < 0 || i > max || j > max)
            {
                if(j > max)
                {
                    Console.WriteLine("Out1");
                    return this.grid[i, 0].status;
                }
                if(i > max)
                {
                    Console.WriteLine("Out2");
                    return this.grid[0, j].status;
                }
                if(0 > i)
                {
                    Console.WriteLine("Out3");
                    return this.grid[(max-2), j].status;
                }
                if(0 > j)
                {
                    Console.WriteLine("Out4");
                    return this.grid[i, (max-2)].status;
                }
            }
            return this.grid[i, j].status;
        }

        public void updateGridSquare(int i, int j)
        {
            int neighbours = 0;
            if(getSquare(i,j) == Status.Alive)
            {
                neighbours--;
            }
            for(int k = i-1; k < i + 1; k++)
            {
                for(int l = j-1; l < j + 1; l++)
                {
                    if (getSquare(k,l) == Status.Alive)
                    {
                        neighbours++;
                    }
                }
            }
            switch(neighbours)
            {
                case 3:
                    buffGrid[i, j] = new Square(Status.Alive);
                    break;
                case 2:
                    buffGrid[i, j] = grid[i, j];
                    break;
                default:
                    buffGrid[i, j] = new Square(Status.Dead);
                    break;
            }
        }

        public void updateGrid()
        {
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    updateGridSquare(i, j);
                    
                }
            }
            this.gen++;
            grid = buffGrid;
            printGrid();
        }

        public void cycleXTimes(int X)
        {
            for(int i = 0; X > i; i++)
            {
                updateGrid();
            }
        }

        public void printGrid()
        {
            Console.WriteLine("Generation " + gen + ":");
            int i = 0;
            foreach (Square cur in grid)
            {
                i++;
                
                if (cur.status == Status.Alive)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write("O");
                }
                if (i >= max)
                {
                    Console.WriteLine("");
                    i = 0;
                }
            }
        }
    }


    class Square
    {

        public Status status { get; set; } = Status.Dead;
        

        public Square(Status s)
        {
            this.status = s;
        }

        public Square(Random seed)
        {
            randomizeStat(seed);
        }

        public void randomizeStat(Random seed)
        {
            
            double val = seed.NextDouble();
            if (val > 0.8)
            {
                this.status = Status.Alive;
            }
            else
            {
                this.status = Status.Dead;
            }
        }

        public void update()
        {

        }
    }
}
