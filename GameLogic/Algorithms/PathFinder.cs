using System;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic.Algorithms;

public class PathFinder(int a, int b)
{
  public int alpha = a;
  public int betta = b;

  public static int N_appeal(Cell Ant, Cell Food) => 1 / (Math.Abs(Food.X - Ant.X) + Math.Abs(Food.Y - Ant.Y));

  public static void GlobalPheramon()
  {
    for (int i = 0; i < Grid.Pheramons.GetLength(0); i++)
    {
      for (int j = 0; j < Grid.Pheramons.GetLength(1); j++)
      {
        Grid.Pheramons[i, j] = Grid.Pheramons[i, j] * (1 - Grid.ro);
      }
    }
  }




}

