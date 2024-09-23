using ReactiveUI;
using Avalonia.Media.Imaging;
using Breather.Desktop.Helpers;
using Avalonia.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace Breather.Desktop.Models;

public class BreatherViewModel : ReactiveObject
{
    private CroppedBitmap? _frame;
    public CroppedBitmap? Frame { get => _frame; private set => this.RaiseAndSetIfChanged(ref _frame, value); }
    public Capsule? Capsule { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public BreatherViewModel()
    {
        Capsule = new Capsule("avares://Breather.Desktop/Assets/Capsules/breather_0.zip");
        Width = 200;
        Height = 200;

        _ = Task.Run(async () =>
        {
            var direction = "";
            var frame = 0;
            while (true)
            {
                if (frame == Capsule.Metadata.Frames.Beginning)
                {
                    direction = "INHALE";
                    Thread.Sleep(500);
                }
                if (frame == Capsule.Metadata.Frames.End)
                {
                    direction = "EXHALE";
                    Thread.Sleep(500);
                }

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Frame = Capsule.GetFrame(frame).Bitmap;
                });
                Thread.Sleep(40);

                switch (direction)
                {
                    case "INHALE":
                        frame++;
                        break;
                    case "EXHALE":
                        frame--;
                        break;
                }
            }
        });
    }
}
