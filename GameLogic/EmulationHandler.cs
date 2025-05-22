using System.Collections.Generic;
using System.Threading;
using AntColony.GameLogic.Algorithms;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic;

public class EmulationHandler()
{
    private static Timer _timer;

    public static void Start()
    {
        _timer = new Timer(Update, null, dueTime: 0, period: 500);
    }

    public static void Update(object obj)
    {
        foreach (var ant in Grid.Ants)
        {
            List<Cell> AvalibaleCells = new()
            {
                new VoidCell { X = ant.X - 1, Y = ant.Y },
                new VoidCell { X = ant.X + 1, Y = ant.Y },
                new VoidCell { X = ant.X, Y = ant.Y - 1 },
                new VoidCell { X = ant.X, Y = ant.Y + 1 },
            };
            PathFinder.Step(AvalibaleCells, ant, Grid.food);
            PathFinder.isFood(Grid.food, ant);
        }

        Core.Receive();
    }

    public void AntCheck(Ant ant)
    {
        if (ant.CarryFood)
        {
            PathFinder.isEnd(Grid.hive, ant);
        }
    }
}
