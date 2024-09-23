using System.Reactive;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using Breather.Desktop.Windows;

namespace Breather.Desktop.Models;

public class AppViewModel : ReactiveObject
{
    public ReactiveCommand<Unit, Unit> ShowSettingsCommand { get; }
    public ReactiveCommand<Unit, Unit> CloseCommand { get; }

    private SettingsWindow? SettingsWindow { get; set; }

    public AppViewModel()
    {
        ShowSettingsCommand = ReactiveCommand.Create(ShowSettings);
        CloseCommand = ReactiveCommand.Create(Close);
    }

    #region Commands
    private void ShowSettings()
    {
        if (SettingsWindow == null)
        {
            SettingsWindow = new SettingsWindow();
            SettingsWindow.Closing += (s, e) =>
            {
                SettingsWindow = null;
            };
        }
        SettingsWindow.Hide();
        SettingsWindow.Show();
    }

    private void Close()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
    #endregion
}
