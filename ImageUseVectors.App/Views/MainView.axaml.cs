using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using ImageUseVectors.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ImageUseVectors.App.Views;

public partial class MainView : UserControl, IMainViewNotify {
    public MainView() {
        InitializeComponent();
        // Async output system info.
        Task.Run(() => {
            Task.Delay(2000).ContinueWith(task => {
                DataContextMe?.OnOutputInfoSystem();
            });
        });
    }

    private void ActualImage_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e) {
        if (null == e) return;
        Image? image = sender as Image;
        if (null == image) return;
        PointerPoint point = e.GetCurrentPoint(image);
        if (point.Properties.IsLeftButtonPressed || point.Properties.IsRightButtonPressed) {
            OnImageOutputPointerPressed(image, point);
        }
    }

    private void ActualImage_PointerPressed(object? sender, PointerPressedEventArgs e) {
        if (null == e) return;
        Image? image = sender as Image;
        if (null == image) return;
        PointerPoint point = e.GetCurrentPoint(image);
        OnImageOutputPointerPressed(image, point);
        // Debug.
        //var x = point.Position.X;
        //var y = point.Position.Y;
        //var msg = $"Pointer press at {x}, {y} relative to sender.";
        //if (point.Properties.IsLeftButtonPressed) {
        //    msg += " Left button pressed.";
        //}
        //if (point.Properties.IsRightButtonPressed) {
        //    msg += " Right button pressed.";
        //}
        //if (null == DataContextMe) return;
        //DataContextMe.StatusHint = msg;
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e) {
        if (null == e) return;
        Size newSize = e.NewSize;
        Size oldSize = e.PreviousSize;
        //Console.WriteLine($"Size changed: Old Size = {oldSize}, New Size = {newSize}");
        //Debug.WriteLine($"Size changed: Old Size = {oldSize}, New Size = {newSize}");

        Dispatcher.UIThread.InvokeAsync(() => {
            StatusImageLabel.IsVisible = newSize.Width >= 400;
        });
    }

    public async void OpenButtonClicked(object source, RoutedEventArgs args) {
        Debug.WriteLine("OpenButtonClicked.");
        if (null == DataContextMe) return;
        // Open.
        // 从当前控件获取 TopLevel。或者，您也可以使用 Window 引用。
        var topLevel = TopLevel.GetTopLevel(this);
        if (null == topLevel) return;
        var storageProvider = topLevel.StorageProvider;
        if (!storageProvider.CanOpen) {
            DataContextMe.StatusHint = "Not support open file dialog!";
            DataContextMe.StatusHintFull = DataContextMe.StatusHint;
            return;
        }

        // 启动异步操作以打开对话框。
        IReadOnlyList<IStorageFile> files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
            //Title = "Open Image File",
            FileTypeFilter = new List<FilePickerFileType> {
                FilePickerFileTypes.ImageAll,
                FilePickerFileTypes.ImageJpg,
                FilePickerFileTypes.ImagePng,
                FilePickerFileTypes.ImageWebp,
                FilePickerFileTypes.All
            },
            AllowMultiple = false
        });

        if (files.Count >= 1) {
            OutputLog(string.Format("Count: {0}", files.Count));
            //// 打开第一个文件的读取流。
            //await using var stream = await files[0].OpenReadAsync();
            //using var streamReader = new StreamReader(stream);
            //// 将文件的所有内容作为文本读取。
            //var fileContent = await streamReader.ReadToEndAsync();
            IStorageFile file = files[0];
            Bitmap? bitmap = null;
            try {
                OutputLog(string.Format("File Name: {0}", file.Name));
                OutputLog(string.Format("File Path: {0}", file.Path));
                string? localPath = file.TryGetLocalPath() ?? file.Path.ToString();
                OutputLog(string.Format("File LocalPath: {0}", localPath));
                var properties = await file.GetBasicPropertiesAsync();
                OutputLog(string.Format("File Size: {0}", properties.Size));
                OutputLog(string.Format("File DateCreated: {0}", properties.DateCreated));
                OutputLog(string.Format("File DateModified: {0}", properties.DateModified));
                // Load.
                await using var stream = await file.OpenReadAsync();
                using MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                bitmap = new Bitmap(ms);
                // Show.
                if (null != bitmap) {
                    string statusImage = AvaloniaBitmapUtil.GetSummary(bitmap);
                    statusImage += ", " + bitmap.GetType().Name;
                    OutputLog(string.Format("Native Bitmap info: {0}", statusImage));
                    WriteableBitmap writeableBitmap = AvaloniaBitmapUtil.ToWriteableBitmap(bitmap);
                    if (object.ReferenceEquals(writeableBitmap, bitmap)) bitmap = null;
                    statusImage = AvaloniaBitmapUtil.GetSummary(writeableBitmap);
                    OutputLog(string.Format("Raw Bitmap info: {0}", statusImage));
                    DataContextMe.BitmapCurrent = writeableBitmap;
                    DataContextMe.BitmapRaw = writeableBitmap;
                    DataContextMe.BitmapFilename = file.Name;
                    DataContextMe.StatusHint = string.Format("Image successfully opened. `{0}`: ({1})", localPath, statusImage);
                    DataContextMe.StatusHintFull = DataContextMe.StatusHint;
                    DataContextMe.StatusImage = statusImage;
                }
            } catch (Exception ex) {
                OutputLog(ex.ToString());
                DataContextMe.StatusHint = ex.Message;
                DataContextMe.StatusHintFull = ex.ToString();
            } finally {
                if (null != bitmap) {
                    bitmap.Dispose();
                }
            }
        }
    }
}
