using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Conways Game of Life.
 */
namespace GameOfLife
{
    public enum Status { Dead, Alive } //Possible states of a cell
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

        /*
         * Creates a GoL grid of the specified x and y dimensions.
         * Args - int size : x and y dimension
         * 
         */
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

        /*
         * Return status of a grid square.
         * Args: int i : x coor, int j :y coor 
         */
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

        /*
         * Updates the state of a single grid square.
        */ 
        public void updateGridSquare(int i, int j)
        {
            int neighbours = 0;
            if(getSquare(i,j) == Status.Alive)
            {
                neighbours--;
            }

            //Counts number of living neighbours 
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

            //Sets state of cell based on number of neighbours
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

        /*
         * Updates every square of the grid.
         */
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
            printGrid(); //Prints grid state
        }

        /*
         * Updates grid a designated number of times.
         * Args - int x : number of cycles 
         */
        public void cycleXTimes(int X)
        {
            for(int i = 0; X > i; i++)
            {
                updateGrid();
            }
        }

        /*
         * Prints grid to command line.
         */
        public void printGrid()
        {
            Console.WriteLine("Generation " + gen + ":");
            int i = 0;
            foreach (Square cur in grid)
            {
                i++;
                //Prints appropriate symbol for cell state
                if (cur.status == Status.Alive)
                {
                    Console.Write("X"); //Living cell
                }
                else
                {
                    Console.Write("O"); //Dead cell
                }
                if (i >= max)
                {
                    Console.WriteLine(""); //Start new line 
                    i = 0;
                }
            }
        }
    }

    /*
     * Cell object that together form a grid.
     */
    class Square
    {

        public Status status { get; set; } = Status.Dead; //State of the square 
        
        /*
         * Creates square of the argument state.
         * Args - Status s : designated sate
         */
        public Square(Status s)
        {
            this.status = s;
        }

        /*
         * Creates a random square based on a seed.
         * Args - Random seed : seed for rng
         */
        public Square(Random seed)
        {
            randomizeStat(seed);
        }

        /*
         * Randomise the state of the 
         * Args - Random seed : seed for rng
         */
        public void randomizeStat(Random seed)
        {
            
            double val = seed.NextDouble();
            if (val > 0.8) //20% generated as living
            {
                this.status = Status.Alive;
            }
            else
            {
                this.status = Status.Dead;
            }
        }

    }
}
