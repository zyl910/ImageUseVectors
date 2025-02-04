using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using ImageUseVectors.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUseVectors.App.Views {
    partial class MainView {

        /// <summary>The DataContext of my type (我的类型的数据上下文) </summary>
        private MainViewModel? _dataContextMe = null;

        protected override void OnDataContextChanged(EventArgs e) {
            base.OnDataContextChanged(e);
            MainViewModel? newValue = DataContext as MainViewModel;
            if (object.ReferenceEquals(newValue, _dataContextMe)) return;
            // Free old.
            if (null != _dataContextMe) {
                _dataContextMe.Notify = null;
            }
            // Config new.
            _dataContextMe = newValue;
            if (null != _dataContextMe) {
                _dataContextMe.Notify = this;
            }
        }

        private void OnImageOutputPointerPressed(Image image, PointerPoint point) {
            if (null == DataContextMe) return;
            StringBuilder msg = new StringBuilder();
            Point pos = point.Position;
            //msg.Append(string.Format("({0:0.0}, {1:0.0})", pos.X, pos.Y));
            WriteableBitmap? bitmap = DataContextMe.BitmapCurrent;
            if (null != bitmap) {
                PixelSize pixelSize = bitmap.PixelSize;
                Rect bounds = image.Bounds;
                int pixelX = (int)(pos.X * pixelSize.Width / bounds.Width);
                int pixelY = (int)(pos.Y * pixelSize.Height / bounds.Height);
                msg.Append(string.Format(", Pixel:({0}, {1})", pixelX, pixelY));
                // TryGetPixelColor
                System.Drawing.Color color;
                int pixelByte;
                Span<byte> buffer = stackalloc byte[32];
                if (AvaloniaBitmapUtil.TryGetPixelColor(bitmap, pixelX, pixelY, out color, out pixelByte, buffer)) {
                    msg.Append(string.Format(", Color:#{0:X8}({1})", color.ToArgb(), color));
                    // buffer bytes.
                    if (pixelByte <= buffer.Length) {
                        msg.Append(", Bytes:(");
                        for (int i = 0; i < pixelByte; i++) {
                            byte n = buffer[i];
                            if (i > 0) {
                                msg.Append(' ');
                            }
                            msg.Append(string.Format("{0:X2}", n));
                        }
                        msg.Append(")");
                    }
                }
            }
            // Output.
            DataContextMe.StatusHint = string.Format("({0:N0}, {1:N0}){2}", pos.X, pos.Y, msg);
            DataContextMe.StatusHintFull = string.Format("({0}, {1}){2}", pos.X, pos.Y, msg);
        }

        public void OutputLog(string? text) {
            Debug.WriteLine(text);
            if (null == text) text = string.Empty;
            try {
                Dispatcher.UIThread.Post(() => OutputLogUi(text));
            } catch (Exception ex) {
                Debug.WriteLine("Fail on OutputLog!");
                Debug.WriteLine(ex);
            }
        }

        internal void OutputLogUi(string text) {
            try {
                if (!LogTextBox.IsLoaded) return;
                string oldText = LogTextBox.Text ?? "";
                bool oldReadOnly = LogTextBox.IsReadOnly;
                if (oldReadOnly) {
                    try {
                        LogTextBox.IsReadOnly = false;
                    } catch (Exception ex) {
                        Debug.WriteLine("Fail on set IsReadOnly to false!");
                        Debug.WriteLine(ex);
                    }
                }
                LogTextBox.SelectionStart = oldText.Length;
                LogTextBox.SelectionEnd = LogTextBox.SelectionStart;
                LogTextBox.SelectedText = text + Environment.NewLine;
                LogTextBox.SelectionStart = (LogTextBox.Text ?? "").Length;
                LogTextBox.SelectionEnd = LogTextBox.SelectionStart;
                if (oldReadOnly) {
                    try {
                        LogTextBox.IsReadOnly = oldReadOnly;
                    } catch (Exception ex) {
                        Debug.WriteLine("Fail on set IsReadOnly to true!");
                        Debug.WriteLine(ex);
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("Fail on OutputLogUi!");
                Debug.WriteLine(ex);
            }
        }

        public void OutputClear() {
            try {
                Dispatcher.UIThread.Post(() => OutputClearUi());
            } catch (Exception ex) {
                Debug.WriteLine("Fail on OutputClear!");
                Debug.WriteLine(ex);
            }
        }

        internal void OutputClearUi() {
            try {
                if (!LogTextBox.IsLoaded) return;
                LogTextBox.Text = string.Empty;
            } catch (Exception ex) {
                Debug.WriteLine("Fail on OutputClearUi!");
                Debug.WriteLine(ex);
            }
        }

        public void OutputCopy() {
            try {
                Dispatcher.UIThread.Post(() => OutputCopyUi());
            } catch (Exception ex) {
                Debug.WriteLine("Fail on OutputCopy!");
                Debug.WriteLine(ex);
            }
        }

        internal async void OutputCopyUi() {
            try {
                string text = LogTextBox.Text ?? "";
                var topLevel = TopLevel.GetTopLevel(this);
                if (null == topLevel) return;
                var clipboard = topLevel.Clipboard;
                if (null == clipboard) return;
                await clipboard.SetTextAsync(text);
                if (null == DataContextMe) return;
                DataContextMe.StatusHint = string.Format("Copy OK. {0}", DateTime.Now);
                DataContextMe.StatusHintFull = DataContextMe.StatusHint;
            } catch (Exception ex) {
                Debug.WriteLine("Fail on OutputCopyUi!");
                Debug.WriteLine(ex);
            }
        }

        /// <inheritdoc cref="_dataContextMe"/>
        internal MainViewModel? DataContextMe { get => _dataContextMe; }

    }
}
