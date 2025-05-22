using System.Linq;
using AntColony.GameLogic;
using AntColony.GameLogic.Models;
using AntColony.Visualization;

namespace AntColony;

public class Core
{
    public Core(int antsCount)
    {
        Parser(antsCount);
    }

    public void Parser(int antsCount)
    {
        Hive hive = new()
        {
            X = MainWindow.Cells.FirstOrDefault(c => c.Type == 4).X,
            Y = MainWindow.Cells.FirstOrDefault(c => c.Type == 4).Y,
        };
        Food food = new()
        {
            X = MainWindow.Cells.FirstOrDefault(c => c.Type == 2).X,
            Y = MainWindow.Cells.FirstOrDefault(c => c.Type == 2).Y,
        };
        Grid grid = new(MainWindow.Cells[399].Y, MainWindow.Cells[399].X, antsCount, hive, food);
    }

    public static void Receive()
    {
        foreach (var item in Grid.Ants)
        {
            int index =
                MainWindow
                    .Cells.Select((cell, i) => new { cell, i })
                    .FirstOrDefault(x => x.cell.X == item.X && x.cell.Y == item.Y)
                    ?.i ?? -1;
            MainWindow.Cells[index] = new CellData
            {
                X = item.X,
                Y = item.Y,
                Type = 3,
            };
        }
    }
}
