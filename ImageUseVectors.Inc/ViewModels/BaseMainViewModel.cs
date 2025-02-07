#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageUseVectors.ViewModels {
    /// <summary>
    /// Base main ViewModel (基本主视图模型).
    /// </summary>
    /// <typeparam name="TBitmap">Type of bitmap (位图的类型).</typeparam>
    public partial class BaseMainViewModel<TBitmap> : BaseViewModel where TBitmap : class {

        /// <summary>The filename of bitmap</summary>
        private string? _bitmapFilename;
        /// <summary>Current bitmap (当前位图).</summary>
        private TBitmap? _bitmapCurrent;
        /// <summary>Last modify bitmap (最后修改位图)</summary>
        private TBitmap? _bitmapLast;
        /// <summary>Raw bitmap (原始位图).</summary>
        private TBitmap? _bitmapRaw;

        /// <summary>Brightness - Auto apply</summary>
        private bool _brightnessAuto = false;
        /// <summary>Brightness - Red</summary>
        private short _brightnessR = 0;
        /// <summary>Brightness - Greed</summary>
        private short _brightnessG = 0;
        /// <summary>Brightness - Blue</summary>
        private short _brightnessB = 0;

        /// <summary>Cpu summary (CPU 简介)</summary>
        private string? _cpuSummary = ".";
        /// <summary>Cpu full summary (CPU 完整简介)</summary>
        private string? _cpuSummaryFull = ".";

        /// <summary>Fit image. When false, image is actual size (适合图像. 为false时图像是实际大小)</summary>
        private bool _fitImage = false;

        /// <summary>Show log panel (显示日志面板).</summary>
        private bool _showLog = true;
        /// <summary>Show raw image (显示原始图像).</summary>
        private bool _showRaw = true;

        /// <summary>Status text of Image</summary>
        private string _statusImage = "(StatusImage)";
        /// <summary>Status text of Hint</summary>
        private string _statusHint = "(StatusHint)";
        /// <summary>Status text of Hint full</summary>
        private string _statusHintFull = "(StatusHintFull)";

        /// <inheritdoc cref="_bitmapFilename"/>
        public string? BitmapFilename {
            get {
                return _bitmapFilename;
            }
            set {
                this.RaiseAndSetIfChanged(ref _bitmapFilename, value);
            }
        }

        /// <inheritdoc cref="_bitmapCurrent"/>
        public TBitmap? BitmapCurrent {
            get {
                return _bitmapCurrent;
            }
            set {
                this.RaiseAndSetIfChanged(ref _bitmapCurrent, value);
            }
        }

        /// <inheritdoc cref="_bitmapLast"/>
        public TBitmap? BitmapLast {
            get {
                return _bitmapLast;
            }
            set {
                this.RaiseAndSetIfChanged(ref _bitmapLast, value);
            }
        }

        /// <inheritdoc cref="_bitmapRaw"/>
        public TBitmap? BitmapRaw {
            get {
                return _bitmapRaw;
            }
            set {
                this.RaiseAndSetIfChanged(ref _bitmapRaw, value);
            }
        }

        /// <inheritdoc cref="_brightnessAuto"/>
        public bool BrightnessAuto {
            get {
                return _brightnessAuto;
            }
            set {
                this.RaiseAndSetIfChanged(ref _brightnessAuto, value);
            }
        }

        /// <inheritdoc cref="_brightnessR"/>
        public short BrightnessR {
            get {
                return _brightnessR;
            }
            set {
                this.RaiseAndSetIfChanged(ref _brightnessR, value);
            }
        }

        /// <inheritdoc cref="_brightnessG"/>
        public short BrightnessG {
            get {
                return _brightnessG;
            }
            set {
                this.RaiseAndSetIfChanged(ref _brightnessG, value);
            }
        }

        /// <inheritdoc cref="_brightnessB"/>
        public short BrightnessB {
            get {
                return _brightnessB;
            }
            set {
                this.RaiseAndSetIfChanged(ref _brightnessB, value);
            }
        }

        /// <inheritdoc cref="_cpuSummary"/>
        public string? CpuSummary {
            get {
                return _cpuSummary;
            }
            set {
                this.RaiseAndSetIfChanged(ref _cpuSummary, value);
            }
        }

        /// <inheritdoc cref="_cpuSummaryFull"/>
        public string? CpuSummaryFull {
            get {
                return _cpuSummaryFull;
            }
            set {
                this.RaiseAndSetIfChanged(ref _cpuSummaryFull, value);
            }
        }

        /// <inheritdoc cref="_fitImage"/>
        public bool FitImage {
            get {
                return _fitImage;
            }
            set {
                this.RaiseAndSetIfChanged(ref _fitImage, value);
            }
        }

        /// <inheritdoc cref="_showLog"/>
        public bool ShowLog {
            get {
                return _showLog;
            }
            set {
                this.RaiseAndSetIfChanged(ref _showLog, value);
            }
        }

        /// <inheritdoc cref="_showRaw"/>
        public bool ShowRaw {
            get {
                return _showRaw;
            }
            set {
                this.RaiseAndSetIfChanged(ref _showRaw, value);
            }
        }

        /// <inheritdoc cref="_statusImage"/>
        public string StatusImage {
            get {
                return _statusImage;
            }
            set {
                this.RaiseAndSetIfChanged(ref _statusImage, value);
            }
        }

        /// <inheritdoc cref="_statusHint"/>
        public string StatusHint {
            get {
                return _statusHint;
            }
            set {
                this.RaiseAndSetIfChanged(ref _statusHint, value);
            }
        }

        /// <inheritdoc cref="_statusHintFull"/>
        public string StatusHintFull {
            get {
                return _statusHintFull;
            }
            set {
                this.RaiseAndSetIfChanged(ref _statusHintFull, value);
            }
        }

    }

}
