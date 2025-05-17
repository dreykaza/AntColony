using System;
using System.Collections.Generic;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic.Algorithms;

public class PathFinder(int a, int b, Grid grid)
{
    public int alpha = a;
    public int betta = b;
    public double ro = 0.5;
    public int Q = 10;
    private readonly Random _rng;

    public static int Euristic(Cell Ant, Cell Food) =>
        1 / (Math.Abs(Food.X - Ant.X) + Math.Abs(Food.Y - Ant.Y));

    public void PheramonDeacrese()
    {
        for (int i = 0; i < grid.Pheramons.GetLength(0); i++)
        {
            for (int j = 0; j < grid.Pheramons.GetLength(1); j++)
            {
                grid.Pheramons[i, j] = grid.Pheramons[i, j] * (1 - ro);
            }
        }
    }

    public Cell Step(List<Cell> cells, Cell Ant, Cell Food)
    {
        double[] _prefixsum = new double[cells.Count];
        int head = 0;
        double x = _rng.NextDouble();
        foreach (var cell in cells)
        {
            double frst =
                Math.Pow(grid.Pheramons[cell.Y, cell.X], alpha)
                * Math.Pow(Euristic(Ant, Food), betta);
            double second = 0;
            for (int i = 0; i < cells.Count; i++)
            {
                second +=
                    Math.Pow(grid.Pheramons[cell.Y, cell.X], alpha)
                    * Math.Pow(Euristic(Ant, Food), betta);
            }
            _prefixsum[head] = frst / second;
            head++;
        }

        int idx = Array.BinarySearch(_prefixsum, x);
        if (idx >= 0)
        {
            return cells[idx];
        }
        else
        {
            return cells[~idx];
        }
    }

    public List<Cell> CheckDir(List<Cell> cells)
    {
        cells.RemoveAll(c => c.Type == 1);
        return cells;
    }

    public void AntPheramon(Ant ant)
    {
        double tao = Q / ant.Steps.GetLength(0);
        for (int i = 0; i < ant.Steps.GetLength(0); i++)
        {
            grid.Pheramons[ant.Steps[i, 0], ant.Steps[i, 1]] += tao;
        }
    }
}
