using System.Collections.Generic;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic;

public class Grid
{
  public static double[,] Pheramons;
  public List<Cell> Cells = [];
  public static double ro = 0.5;
  public Grid(int lines, int collums)
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
        Cells.Add(new VoidCell
        {
          X = j,
          Y = i
        });
      }
    }
  }
}

