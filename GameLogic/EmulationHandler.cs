using System.Collections.Generic;
using System.Threading;
using AntColony.GameLogic.Algorithms;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic;

public class EmulationHandler(Grid grid)
{
    private Timer _timer;
    public PathFinder pathFinder = new(grid);

    public void Start()
    {
        _timer = new Timer(Update, null, dueTime: 0, period: 500);
    }

    public void Update(object obj)
    {
        foreach (var ant in grid.Ants)
        {
            List<Cell> AvalibaleCells = new()
            {
                new VoidCell { X = ant.X - 1, Y = ant.Y },
                new VoidCell { X = ant.X + 1, Y = ant.Y },
                new VoidCell { X = ant.X, Y = ant.Y - 1 },
                new VoidCell { X = ant.X, Y = ant.Y + 1 },
            };
            pathFinder.Step(AvalibaleCells, ant, grid.food);
            pathFinder.isFood(grid.food, ant);
            if (ant.CarryFood)
            {
                pathFinder.isEnd(grid.hive, ant);
            }
        }
    }
}
