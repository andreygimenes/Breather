using System.Reactive;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using ReactiveUI;

namespace Breather.Desktop;

public class AppViewModel : ReactiveObject
{
    public ReactiveCommand<Unit, Unit> ShowSettingsCommand { get; }
    public ReactiveCommand<Unit, Unit> CloseCommand { get; }

    private SettingsWindow settingsWindow { get; set; }

    public AppViewModel()
    {
        ShowSettingsCommand = ReactiveCommand.Create(ShowSettings);
        CloseCommand = ReactiveCommand.Create(Close);
    }

    #region Commands
    private void ShowSettings()
    {
        if (settingsWindow == null)
        {
            settingsWindow = new SettingsWindow();
            settingsWindow.Closing += (s, e) =>
            {
                settingsWindow = null;
            };
        }
        settingsWindow.Hide();
        settingsWindow.Show();
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
