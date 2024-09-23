using System;
using System.Text.Json.Serialization;

namespace Breather.Desktop.Helpers;

public class Settings
{
    [JsonPropertyName("speed")]
    public string Speed { get; set; }
    [JsonPropertyName("size")]
    public string Size { get; set; }
    [JsonPropertyName("position")]
    public string Position { get; set; }

    public Settings()
    {
        
    }
}
