using System.Threading;
using AntColony.GameLogic.Algorithms;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic;

public class EmulationHandler()
{
    private static Timer _timer;

    public static void Start() => _timer = new Timer(Update, null, dueTime: 0, period: 150);

    public static void Update(object obj)
    {
        foreach (var ant in Grid.Ants)
        {
            PathFinder.Step(PathFinder.CheckDir(ant), ant);
            PathFinder.isFood(ant);
            AntCheck(ant);
        }

        PathFinder.PheramonDeacrese();
        Core.Receive();
    }

    public static void AntCheck(Ant ant)
    {
        if (PathFinder.isEnd(ant))
        {
            PathFinder.AntPheramon(ant);
            ant.CarryFood = false;
            ant.Steps.Clear();
        }
    }
}
