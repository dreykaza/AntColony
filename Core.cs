using System.Collections.Generic;

namespace AntColony;

public class Core
{
    public List<CellData> cells;

    public Core(int antsCount, List<CellData> cell)
    {
        cells = cell;
    }
}
