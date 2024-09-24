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
    public Settings Settings { get; set; }

    public BreatherViewModel()
    {
        Capsule = new Capsule("avares://Breather.Desktop/Assets/Capsules/breather_0.zip");
        Settings = Settings.Instance;

        _ = Task.Run(async () =>
        {
            var direction = "";
            var frame = 0;
            while (true)
            {
                if (frame == Capsule.Metadata.Frames.Beginning)
                {
                    direction = "INHALE";
                    Thread.Sleep(Settings.InhaleDelay);
                }
                if (frame == Capsule.Metadata.Frames.End)
                {
                    direction = "EXHALE";
                    Thread.Sleep(Settings.ExhaleDelay);
                }

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Frame = Capsule.GetFrame(frame).Bitmap;
                });
                Thread.Sleep(1000 / Settings.FPS);

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
