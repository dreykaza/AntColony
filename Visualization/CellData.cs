using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AntColony.GameLogic;
using AntColony.Visualization;

public class CellData : INotifyPropertyChanged
{
    private int _x;
    private int _y;
    private int _type;

    public int X
    {
        get => _x;
        set => SetField(ref _x, value);
    }

    public int Y
    {
        get => _y;
        set => SetField(ref _y, value);
    }

    public int Type
    {
        get => _type;
        set => SetField(ref _type, value);
    }
    private string _coordinates;

    public string Coordinates
    {
        get => _coordinates;
        private set => SetField(ref _coordinates, value);
    }

    public void UpdateCoordinates()
    {
        Coordinates = Grid.AntInCell(X, Y);
        OnPropertyChanged(nameof(Coordinates));
    }

    public static void RefreshAllCells()
    {
        foreach (var cell in MainWindow.Cells)
        {
            cell.UpdateCoordinates();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        if (propertyName is nameof(X) or nameof(Y))
            OnPropertyChanged(nameof(Coordinates));
        return true;
    }
}
