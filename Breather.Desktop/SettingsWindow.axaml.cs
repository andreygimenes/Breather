using System.Linq;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Breather.Desktop
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            breatherSpeed.ItemsSource = new List<string> {
                "Ultraslow",
                "Slow",
                "Default",
                "Fast",
                "Ultrafast",
            };
            breatherSpeed.SelectedIndex = 2;
        }
    }
}