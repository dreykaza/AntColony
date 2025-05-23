using System;
using System.Collections.Generic;
using System.Linq;
using AntColony.GameLogic.Models;
using AntColony.Visualization;

namespace AntColony.GameLogic.Algorithms;

public class PathFinder()
{
    public static int alpha = 1;
    public static int betta = 1;
    public static double ro = 0.5;
    public static int Q = 10;
    private static readonly Random _rng = new();

    public static double Euristic(Cell cell) =>
        1.0 / (Math.Abs(Grid.food.X - cell.X) + Math.Abs(Grid.food.Y - cell.Y));

    public void PheramonDeacrese()
    {
        for (int i = 0; i < Grid.Pheramons.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.Pheramons.GetLength(1); j++)
            {
                Grid.Pheramons[i, j] = Grid.Pheramons[i, j] * (1 - ro);
            }
        }
    }

    public static void Step(List<Cell> cells, Ant Ant, Food Food)
    {
        int n = cells.Count;
        double[] prefixSum = new double[n];
        double sum = 0;
        double x = _rng.NextDouble();

        double denom = 0;
        foreach (var nbr in cells)
        {
            double t = Math.Pow(Grid.Pheramons[nbr.Y, nbr.X], alpha);
            double e = Math.Pow(Euristic(nbr), betta);
            denom += t * e;
        }

        for (int i = 0; i < n; i++)
        {
            var cell = cells[i];
            double t = Math.Pow(Grid.Pheramons[cell.Y, cell.X], alpha);
            double e = Math.Pow(Euristic(cell), betta);

            double p = (t * e) / denom;

            sum += p;
            prefixSum[i] = sum;
        }

        int idx = Array.BinarySearch(prefixSum, x);
        if (idx < 0)
            idx = ~idx;

        Ant.X = cells[idx].X;
        Ant.Y = cells[idx].Y;
        Ant.Steps.Add(new Coordinate { X = Ant.X, Y = Ant.Y });
    }

    public static List<Cell> CheckDir(Ant ant)
    {
        var cellsList = MainWindow.Cells.Where(c => c.Type == 1).ToList();
        List<Cell> AvalibaleCells = new()
        {
            new VoidCell { X = ant.X - 1, Y = ant.Y },
            new VoidCell { X = ant.X + 1, Y = ant.Y },
            new VoidCell { X = ant.X, Y = ant.Y - 1 },
            new VoidCell { X = ant.X, Y = ant.Y + 1 },
        };

        var cells = MainWindow.Cells.Where(c => c.Type == 1);

        var occupiedCoords = new HashSet<(int X, int Y)>(cells.Select(c => (c.X, c.Y)));

        AvalibaleCells.RemoveAll(av => occupiedCoords.Contains((av.X, av.Y)));
        return AvalibaleCells;
    }

    public static void isFood(Food food, Ant ant)
    {
        if (food.X == ant.X && food.Y == ant.Y)
        {
            ant.CarryFood = true;
        }
    }

    public static bool isEnd(Hive hive, Ant ant)
    {
        if (hive.X == ant.X && hive.Y == ant.Y && ant.CarryFood)
        {
            return true;
        }
        return false;
    }

    public void AntPheramon(Ant ant)
    {
        int Count = 0;
        double tao = Q / ant.Steps.Count;
        foreach (var i in ant.Steps)
        {
            Grid.Pheramons[i.X, i.Y] += tao;
            Count++;
        }
    }
}
