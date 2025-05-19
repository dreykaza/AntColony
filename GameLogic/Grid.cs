using System.Collections.Generic;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic;

public class Grid
{
    public double[,] Pheramons;
    public List<Cell> Cells = [];
    public List<Ant> Ants = [];

    public Grid(int lines, int collums, int ants)
    {
        ListInit(lines, collums);
    }

    public void ListInit(int lines, int collums)
    {
        Pheramons = new double[collums, lines];
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < collums; j++)
            {
                Pheramons[i, j] = 0.1;
                Cells.Add(new VoidCell { X = j, Y = i });
            }
        }
    }

    public void SwarmInit(int x, int y, int ants)
    {
        var index = Cells.FindIndex(h => h.X == x && h.Y == y);
        Cells[index] = new Hive { X = x, Y = y };
        for (int i = 0; i < ants; i++)
        {
            Ants.Add(
                new Ant
                {
                    X = x,
                    Y = y,
                    Steps = new List<int[,]>(),
                }
            );
        }
    }

    public void FoodInit(int x, int y)
    {
        var index = Cells.FindIndex(h => h.X == x && h.Y == y);
        Cells[index] = new Food { X = x, Y = y };
    }
}
