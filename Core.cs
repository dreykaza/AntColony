using System.Collections.Generic;
using System.Linq;
using AntColony.GameLogic;
using AntColony.GameLogic.Models;
using AntColony.Visualization;

namespace AntColony;

public class Core
{
    private static Dictionary<Ant, (int prevX, int prevY)> _antPositions = new();

    public Core(int antsCount)
    {
        Parser(antsCount);
        EmulationHandler.Start();
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
        Grid grid = new(20, 20, antsCount, hive, food);
    }

    public static void Receive()
    {
        var cells = MainWindow.Cells;

        foreach (var ant in Grid.Ants)
        {
            if (_antPositions.TryGetValue(ant, out var prevPos))
            {
                var prevCell = cells.FirstOrDefault(c =>
                    c.X == prevPos.prevX && c.Y == prevPos.prevY
                );

                prevCell.Type =
                    (prevCell.X == Grid.food.X && prevCell.Y == Grid.food.Y) ? 2
                    : (prevCell.X == Grid.hive.X && prevCell.Y == Grid.hive.Y) ? 4
                    : 0;

                _antPositions[ant] = (ant.X, ant.Y);
            }

            var currentCell = cells.FirstOrDefault(c => c.X == ant.X && c.Y == ant.Y);
            if (currentCell is null)
                return;

            currentCell.Type =
                (currentCell.X == Grid.hive.X && currentCell.Y == Grid.hive.Y) ? 4
                : (currentCell.X == Grid.food.X && currentCell.Y == Grid.food.Y) ? 2
                : 3;

            _antPositions[ant] = (ant.X, ant.Y);
        }
        CellData.RefreshAllCells();
    }
}
