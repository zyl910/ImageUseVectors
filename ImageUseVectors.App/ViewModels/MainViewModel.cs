using Avalonia.Media.Imaging;
using ImageUseVectors.ViewModels;

namespace ImageUseVectors.App.ViewModels;

public class MainViewModel : BaseMainViewModel<WriteableBitmap> {
    public string Greeting => "Welcome to Avalonia!";

}
