using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Breather.Desktop.Helpers;

public class Settings
{
    [JsonPropertyName("inhale_delay")]
    public int InhaleDelay { get; set; }
    [JsonPropertyName("exhale_delay")]
    public int ExhaleDelay { get; set; }

    [JsonPropertyName("fps")]
    public int FPS { get; set; }
    [JsonPropertyName("width")]
    public int Width { get; set; }
    [JsonPropertyName("height")]
    public int Height { get; set; }
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }

    public event EventHandler Changed;

    private static Settings? _instance;
    public static Settings Instance {
        get {
            if (_instance == null)
            {
                _instance = Load();
            }
            return _instance;
        }
    }

    public static Settings Load()
    {
        try
        {
            var json = File.ReadAllText("Breather.json");
            var settings = JsonSerializer.Deserialize<Settings>(json);
            if (settings == null) throw new Exception("Error loading settings from file");
            return settings;
        } catch (Exception ex)
        {
            Console.WriteLine($"Breather: {ex.Message}");
        }

        return new Settings{
            InhaleDelay = 600,
            ExhaleDelay = 800,
            FPS = 25,
            Width = 200,
            Height = 200,
        };
    }

    public static void Save()
    {
        Instance.OnChanged(null);
        try
        {
            var json = JsonSerializer.Serialize(Instance);
            File.WriteAllText("Breather.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Breather: {ex.Message}");
        }
    }

    public void OnChanged(EventArgs e) {
        Changed?.Invoke(this, e);
    }
}
