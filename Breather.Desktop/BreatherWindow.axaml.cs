using System.Linq;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Breather.Desktop;

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
    }
}