using System.Collections.Generic;
using System.Threading;
using AntColony.GameLogic.Models;

namespace AntColony.GameLogic;

public class EmulationHandler(int lines, int collums, int ants)
{
    private Timer _timer;
    public Grid grid = new(lines, collums, ants);

    public void Start()
    {
        _timer = new Timer(Update, null, dueTime: 0, period: 500);
    }

    public void Update(object obj) { }
}
