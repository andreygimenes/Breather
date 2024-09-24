using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Breather.Desktop.Helpers;

public class Settings
{
    [JsonPropertyName("inhale_delay")]
    public int InhaleDelay { get; set; } = 600;
    [JsonPropertyName("exhale_delay")]
    public int ExhaleDelay { get; set; } = 800;

    [JsonPropertyName("fps")]
    public int FPS { get; set; } = 25;
    [JsonPropertyName("width")]
    public int Width { get; set; } = 200;
    [JsonPropertyName("height")]
    public int Height { get; set; } = 200;
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }

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
            if (json == null) throw new Exception("Error loading settings from file");
        } catch (Exception ex)
        {
            Console.WriteLine($"Breather: {ex.Message}");
        }

        return new Settings();
    }

    public static void Save()
    {
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
}
