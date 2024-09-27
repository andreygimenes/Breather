using System;
using System.Collections.Generic;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Breather.Desktop.Helpers;

namespace Breather.Desktop.Models;

public class SettingsViewModel : ReactiveObject
{
    [Reactive]
    public int Speed { get; set; }
    [Reactive]
    public int Size { get; set; }
    [Reactive]
    public int Position { get; set; }

    public List<string> SpeedItems { get; set; }
    public List<string> SizeItems { get; set; }
    public List<string> PositionItems { get; set; }

    public SettingsViewModel()
    {
        switch (Settings.Instance.FPS)
        {
            case 15:
                Speed = 0;
                break;
            case 20:
                Speed = 1;
                break;
            case 25:
                Speed = 2;
                break;
            case 35:
                Speed = 3;
                break;
            case 50:
                Speed = 4;
                break;
            default:
                Speed = 2;
                break;
        }

        switch (Settings.Instance.Width)
        {
            case 100:
                Size = 0;
                break;
            case 150:
                Size = 1;
                break;
            case 200:
                Size = 2;
                break;
            case 300:
                Size = 3;
                break;
            case 400:
                Size = 4;
                break;
            default:
                Size = 2;
                break;
        }
        Position = 1;

        this.WhenAnyValue(vm => vm.Speed).Subscribe(_ => UpdateSpeed());
        this.WhenAnyValue(vm => vm.Size).Subscribe(_ => UpdateSize());
        this.WhenAnyValue(vm => vm.Position).Subscribe(_ => UpdatePosition());

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

    public void UpdateSpeed()
    {
        switch (Speed)
        {
            case 0:
                Settings.Instance.FPS = 15;
                break;
            case 1:
                Settings.Instance.FPS = 25;
                break;
            case 2:
                Settings.Instance.FPS = 30;
                break;
            case 3:
                Settings.Instance.FPS = 35;
                break;
            case 4:
                Settings.Instance.FPS = 50;
                break;
            default:
                Settings.Instance.FPS = 25;
                break;
        }
        Settings.Save();
    }

    public void UpdateSize()
    {
        switch (Size)
        {
            case 0:
                Settings.Instance.Width = 100;
                Settings.Instance.Height = 100;
                break;
            case 1:
                Settings.Instance.Width = 150;
                Settings.Instance.Height = 150;
                break;
            case 2:
                Settings.Instance.Width = 200;
                Settings.Instance.Height = 200;
                break;
            case 3:
                Settings.Instance.Width = 300;
                Settings.Instance.Height = 300;
                break;
            case 4:
                Settings.Instance.Width = 400;
                Settings.Instance.Height = 400;
                break;
            default:
                Settings.Instance.Width = 200;
                Settings.Instance.Height = 200;
                break;
        }
        Settings.Save();
    }

    public void UpdatePosition()
    {
        Settings.Save();
    }
}
