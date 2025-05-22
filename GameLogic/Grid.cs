using System.Collections.Generic;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic;

public class Grid
{
    public static double[,] Pheramons;
    public static List<Cell> Cells = [];
    public static List<Ant> Ants = [];
    public static Food food;
    public static Hive hive;

    public Grid(int lines, int collums, int ants, Hive hiveUI, Food foodUI)
    {
        ListInit(lines, collums);
        SwarmInit(hiveUI, ants);
        FoodInit(foodUI);
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

    public void SwarmInit(Hive hiveUI, int ants)
    {
        var index = Cells.FindIndex(h => h.X == hiveUI.X && h.Y == hiveUI.Y);
        Cells[index] = new Hive { X = hiveUI.X, Y = hiveUI.Y };
        hive = new Hive { X = hiveUI.X, Y = hiveUI.Y };
        for (int i = 0; i < ants; i++)
        {
            Ants.Add(
                new Ant
                {
                    X = hiveUI.X,
                    Y = hiveUI.Y,
                    Steps = new List<Coordinate>(),
                    CarryFood = false,
                }
            );
        }
    }

    public void FoodInit(Food foodUI)
    {
        var index = Cells.FindIndex(h => h.X == foodUI.X && h.Y == foodUI.Y);
        Cells[index] = new Food { X = foodUI.X, Y = foodUI.Y };
        food = new Food { X = foodUI.X, Y = foodUI.Y };
    }
}
