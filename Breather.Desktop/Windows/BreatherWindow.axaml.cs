using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace Breather.Desktop.Windows;

public partial class BreatherWindow : Window
{
    public BreatherWindow()
    {
        InitializeComponent();

        Closing += (s, e) =>
        {
            e.Cancel = true;
        };
        Topmost = true;
        ShowActivated = false;
    }

    private bool _mouseDownForWindowMoving = false;
    private PointerPoint _originalPoint;

    private void Display_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_mouseDownForWindowMoving) return;

        PointerPoint currentPoint = e.GetCurrentPoint(this);
        Position = new PixelPoint(Position.X + (int)(currentPoint.Position.X - _originalPoint.Position.X),
            Position.Y + (int)(currentPoint.Position.Y - _originalPoint.Position.Y));
    }

    private void Display_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (WindowState == WindowState.Maximized || WindowState == WindowState.FullScreen) return;

        _mouseDownForWindowMoving = true;
        _originalPoint = e.GetCurrentPoint(this);
    }

    private void Display_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _mouseDownForWindowMoving = false;
    }
}