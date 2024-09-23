using System.Collections.Generic;
using ReactiveUI;

namespace Breather.Desktop.Models;

public class SettingsViewModel : ReactiveObject
{
    private int _speed;
    public int Speed { get => _speed; private set => this.RaiseAndSetIfChanged(ref _speed, value); }


    private int _size;
    public int Size { get => _size; private set => this.RaiseAndSetIfChanged(ref _size, value); }


    private int _position;
    public int Position { get => _position; private set => this.RaiseAndSetIfChanged(ref _position, value); }

    public List<string> SpeedItems { get; set; }
    public List<string> SizeItems { get; set; }
    public List<string> PositionItems { get; set; }

    public SettingsViewModel()
    {
        Speed = 2;
        Size = 2;
        Position = 1;

        SpeedItems = [
            "Ultra-Slow",
            "Slow",
            "Default",
            "Fast",
            "Ultra-Fast",
        ];

        SizeItems =
        [
            "Ultra-Small",
            "Small",
            "Default",
            "Large",
            "Ultra-Large",
        ];

        PositionItems = [
            "Top Left",
            "Top Middle",
            "Top Right",
            "Bottom Left",
            "Bottom Middle",
            "Bottom Right",
        ];
    }
}
