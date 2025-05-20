using System.Collections.Generic;
using AntColony.GameLogic;
using AntColony.GameLogic.Models;

namespace AntColony;

public class Core
{
    Grid grid;

    public Core(int antsCount, List<CellData> CellUi)
    {
        Parser(CellUi, antsCount);
    }

    public void Parser(List<CellData> cellsUI, int antsCount)
    {
        Hive hive = new()
        {
            X = cellsUI.Find(c => c.Type == 4).X,
            Y = cellsUI.Find(c => c.Type == 4).Y,
        };
        Food food = new()
        {
            X = cellsUI.Find(c => c.Type == 2).X,
            Y = cellsUI.Find(c => c.Type == 2).Y,
        };
        grid = new(cellsUI[399].Y, cellsUI[399].X, antsCount, hive, food);
    }
}
