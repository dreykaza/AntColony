using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace AntColony.Visualization;

public partial class GridCell : UserControl
{
    public static readonly StyledProperty<CellData?> DataProperty = AvaloniaProperty.Register<
        GridCell,
        CellData?
    >(nameof(Data));

    public CellData? Data
    {
        get => GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public GridCell()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void CellBorder_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (Data == null)
            return;

        var point = e.GetCurrentPoint(this);
        Data.Type = point.Properties.IsLeftButtonPressed ? 2 : 4;
        e.Handled = true;
    }
}
