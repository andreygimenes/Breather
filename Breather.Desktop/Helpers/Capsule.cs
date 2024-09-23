using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Text.Json.Serialization;
using Avalonia;

namespace Breather.Desktop.Helpers;

public class Capsule : IDisposable
{
    public CapsuleArchive Archive { get; set; }
    public CapsuleSpritesheet Spritesheet { get; set; }
    public List<CapsuleSprite> Sprites { get; set; }
    public CapsuleMetadata Metadata { get; set; }

    public Capsule(string path)
    {
        Archive = new CapsuleArchive(path);
        Sprites = new List<CapsuleSprite>();

        foreach (var entry in Archive.Zip.Entries)
        {
            using var entryStream = entry.Open();

            using var stream = new MemoryStream();
            entryStream.CopyTo(stream);
            stream.Seek(0, SeekOrigin.Begin);

            var fileName = Path.GetFileNameWithoutExtension(entry.Name);
            var fileExtension = Path.GetExtension(entry.Name);
            if (fileExtension == ".png" && fileName == "spritesheet")
            {
                Spritesheet = new CapsuleSpritesheet(new Bitmap(stream));
            }
            if (fileExtension == ".json" && fileName == "metadata")
            {
                var json = "{}";
                using (var reader = new StreamReader(stream, Encoding.UTF8)) {
                    json = reader.ReadToEnd();
                }
                Metadata = JsonSerializer.Deserialize<CapsuleMetadata>(json);
            }
        }

        int i = 0;
        foreach (var sprite in Spritesheet.Sprites(Metadata.Frames.Width, Metadata.Frames.Height)) {
            Sprites.Add(new CapsuleSprite(i, sprite));
            i++;
        }
    }

    public CapsuleSprite GetFrame(int index)
    {
        return Sprites.FirstOrDefault(x => x.Index == index);
    }

    public void Dispose()
    {
        Archive?.Dispose();
        Sprites?.ForEach(x => x?.Dispose());
    }
}

public class CapsuleArchive : IDisposable
{
    public Stream? Stream { get; set; }
    public ZipArchive? Zip { get; set; }

    public CapsuleArchive(string path)
    {
        Load(path);
    }

    public void Load(string path)
    {
        using var resStream = AssetLoader.Open(new Uri(path));
        resStream.Seek(0, SeekOrigin.Begin);

        Stream = new MemoryStream();
        resStream.CopyTo(Stream);

        Zip = new ZipArchive(Stream, ZipArchiveMode.Read);
    }

    public void Dispose()
    {
        Stream?.Dispose();
        Zip?.Dispose();
    }
}

public class CapsuleSpritesheet : IDisposable
{
    public Bitmap Bitmap { get; set; }

    public CapsuleSpritesheet(Bitmap bitmap)
    {
        Bitmap = bitmap;
    }

    public List<CroppedBitmap> Sprites(int width, int height)
    {
        int xOffset = 0;
        int yOffset = 0;
        var cropRect = new PixelRect(0, 0, width, height);
        var sprites = new List<CroppedBitmap>();

        if ((Bitmap.PixelSize.Height % height) == 0 && (Bitmap.PixelSize.Width % width) == 0)
        {
            int rows = Bitmap.PixelSize.Height / height;
            int columns = Bitmap.PixelSize.Width / width;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    int currentY = yOffset + row * height;
                    int currentX = xOffset + col * width;
                    cropRect = new PixelRect(currentX, currentY, width, height);

                    sprites.Add(new CroppedBitmap(Bitmap, cropRect));
                }
            }
        }
        return sprites;
    }

    public void Dispose()
    {
        Bitmap?.Dispose();
    }
}

public class CapsuleSprite : IDisposable
{
    public int Index { get; set; }
    public CroppedBitmap Bitmap { get; set; }

    public CapsuleSprite(int index, CroppedBitmap bitmap)
    {
        Index = index;
        Bitmap = bitmap;
    }

    public void Dispose()
    {
        Bitmap?.Dispose();
    }
}

public class CapsuleMetadata
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("frames")]
    public CapsuleFramesMetadata Frames { get; set; }

    public class CapsuleFramesMetadata
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("beginning")]
        public int Beginning { get; set; }
        [JsonPropertyName("middle")]
        public int Middle { get; set; }
        [JsonPropertyName("end")]
        public int End { get; set; }
    }
}
