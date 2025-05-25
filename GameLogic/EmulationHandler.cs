using System;
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
            PathFinder.Step(PathFinder.CheckDir(ant), ant);
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
