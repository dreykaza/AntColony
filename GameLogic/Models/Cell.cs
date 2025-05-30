using System.Collections.Generic;

namespace AntColony.GameLogic.Models;

public abstract class Cell
{
    public int X { get; set; }
    public int Y { get; set; }

    public abstract int Type { get; }
}

public class VoidCell : Cell
{
    public override int Type => 0;
}

public class Wall : Cell
{
    public override int Type => 1;
}

public class Food : Cell
{
    public override int Type => 2;
}

public class Ant : Cell
{
    public override int Type => 3;
    public required List<Coordinate> Steps { get; set; }
    public required bool CarryFood { get; set; }
}

public class Hive : Cell
{
    public override int Type => 4;
}
