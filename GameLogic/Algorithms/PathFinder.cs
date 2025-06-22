using System;
using System.Collections.Generic;
using System.Linq;
using AntColony.GameLogic.Models;
using AntColony.Visualization;

namespace AntColony.GameLogic.Algorithms;

public class PathFinder()
{
    public static int alpha = 2;
    public static int betta = 4;
    public static double ro = 0.01;
    public static int Q = 100;
    private static readonly Random _rng = new();

    public static double Euristic(Cell cell, Cell target)
    {
        double result = 1.0 / (Math.Abs(target.X - cell.X) + Math.Abs(target.Y - cell.Y));
        if (double.IsInfinity(result))
            result = 1;
        return result;
    }

    public static void PheramonDeacrese()
    {
        for (int i = 0; i < Grid.Pheramons.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.Pheramons.GetLength(1); j++)
            {
                Grid.Pheramons[i, j] *= 1 - ro;
            }
        }
    }

    public static void Step(List<Cell> cells, Ant Ant)
    {
        int n = cells.Count;
        double[] prefixSum = new double[n];
        double sum = 0;
        double x = _rng.NextDouble();
        Cell target;

        target = Ant.CarryFood
            ? new VoidCell { X = Grid.hive.X, Y = Grid.hive.Y }
            : new VoidCell { X = Grid.food.X, Y = Grid.food.Y };
        double denom = 0;
        foreach (var nbr in cells)
        {
            double t = Math.Pow(Grid.Pheramons[nbr.Y, nbr.X], alpha);
            double e = Math.Pow(Euristic(nbr, target), betta);
            denom += t * e;
        }

        for (int i = 0; i < n; i++)
        {
            var cell = cells[i];
            double t = Math.Pow(Grid.Pheramons[cell.Y, cell.X], alpha);
            double e = Math.Pow(Euristic(cell, target), betta);

            double p = t * e / denom;

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
        List<Cell> AvalibaleCells =
        [
            new VoidCell { X = ant.X - 1, Y = ant.Y },
            new VoidCell { X = ant.X + 1, Y = ant.Y },
            new VoidCell { X = ant.X, Y = ant.Y - 1 },
            new VoidCell { X = ant.X, Y = ant.Y + 1 },
        ];

        var cells = MainWindow.Cells.Where(c => c.Type == 1);

        var occupiedCoords = new HashSet<(int X, int Y)>(cells.Select(c => (c.X, c.Y)));

        AvalibaleCells.RemoveAll(av => occupiedCoords.Contains((av.X, av.Y)));
        return AvalibaleCells;
    }

    public static void isFood(Ant ant)
    {
        if (Grid.food.X == ant.X && Grid.food.Y == ant.Y)
            ant.CarryFood = true;
    }

    public static bool isEnd(Ant ant)
    {
        if (Grid.hive.X == ant.X && Grid.hive.Y == ant.Y && ant.CarryFood == true)
            return true;

        return false;
    }

    public static void AntPheramon(Ant ant)
    {
        double tao = Q / ant.Steps.Count;
        var visited = new HashSet<(int X, int Y)>();

        foreach (var step in ant.Steps)
        {
            if (visited.Add((step.X, step.Y)))
            {
                Grid.Pheramons[step.X, step.Y] += tao;
            }
        }
    }
}
