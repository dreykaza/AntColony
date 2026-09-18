# AntColony
![ui](grid.png)

I made this project mostly as an algorithmic challenge for myself.

I wanted to take something actually interesting — an ant colony optimization approach — and see if I could build a small working simulation around it instead of just reading about the algorithm.

The main idea is that ants search for food, leave pheromones along their paths, and use those pheromones when deciding where to go next. Pheromones also evaporate over time, so the colony can gradually move away from paths that aren't useful anymore.

What I liked most about this project was keeping the algorithm itself independent from the UI.

The simulation has its own `Grid`, `Ant`, `Food`, `Hive` and pheromone map, while the visualization is basically another layer sitting on top of it. `PathFinder` handles the actual decision making, and `EmulationHandler` controls the simulation loop.

I also split the cell types into their own classes instead of representing everything with random numbers. So a cell can actually be a `Wall`, `Food`, `Ant`, `Hive` or just an empty cell.

I just wanted to take an algorithm that looked interesting, understand how it actually works by implementing it myself, and see whether I could make the whole thing fit together cleanly.

### Structure

```text
AntColony
├── GameLogic
│   ├── Algorithms
│   │   └── PathFinder
│   ├── Models
│   ├── Grid
│   └── EmulationHandler
│
├── Visualization
│   ├── MainWindow
│   ├── GridCell
│   └── CellData
│
└── Core
```

The interesting part is watching the algorithm do its thing rather than telling the ants exactly where to go.
