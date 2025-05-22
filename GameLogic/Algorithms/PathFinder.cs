using System;
using System.Collections.Generic;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic.Algorithms;

public class PathFinder()
{
    public static int alpha = 1;
    public static int betta = 1;
    public static double ro = 0.5;
    public static int Q = 10;
    private static readonly Random _rng;

    public static int Euristic(Ant Ant, Food Food) =>
        1 / (Math.Abs(Food.X - Ant.X) + Math.Abs(Food.Y - Ant.Y));

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
        double[] _prefixsum = new double[cells.Count];
        int head = 0;
        double x = _rng.NextDouble();
        foreach (var cell in cells)
        {
            double frst =
                Math.Pow(Grid.Pheramons[cell.Y, cell.X], alpha)
                * Math.Pow(Euristic(Ant, Food), betta);
            double second = 0;
            for (int i = 0; i < cells.Count; i++)
            {
                second +=
                    Math.Pow(Grid.Pheramons[cell.Y, cell.X], alpha)
                    * Math.Pow(Euristic(Ant, Food), betta);
            }
            _prefixsum[head] = frst / second;
            head++;
        }

        int idx = Array.BinarySearch(_prefixsum, x);
        if (idx >= 0)
        {
            Ant.Steps.Add(new Coordinate() { X = cells[idx].X, Y = cells[idx].Y });
        }
        else
        {
            Ant.Steps.Add(new Coordinate() { X = cells[~idx].X, Y = cells[~idx].Y });
        }
    }

    public List<Cell> CheckDir(List<Cell> cells)
    {
        cells.RemoveAll(c => c.Type == 1);
        return cells;
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
