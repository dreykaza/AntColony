using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AntColony.Visualization
{
    public partial class MainWindow : Window
    {
        private const int GridSize = 20;
        public ObservableCollection<CellData> Cells { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            GridContainer.Children.Clear();
            Cells.Clear();

            for (int y = 0; y < GridSize; y++)
            {
                for (int x = 0; x < GridSize; x++)
                {
                    var cellData = new CellData
                    {
                        X = x,
                        Y = y,
                        Type = x == 0 || y == 0 || x == GridSize - 1 || y == GridSize - 1 ? 1 : 0,
                    };

                    var cell = new GridCell { Data = cellData };
                    GridContainer.Children.Add(cell);
                    Cells.Add(cellData);
                }
            }
        }

        private void StartButton_Click(object? sender, RoutedEventArgs e)
        {
            // Теперь можно напрямую использовать коллекцию Cells
            var antsCount = int.Parse(NumberOfAntsInput.Text);
            StartSimulation(antsCount, Cells.ToList());
        }

        private void StartSimulation(int antsCount, List<CellData> cellsData)
        {
            Core core = new(antsCount, cellsData);
        }
    }
}
