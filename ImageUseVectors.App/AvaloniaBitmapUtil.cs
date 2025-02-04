using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zyl.VectorTraits;

namespace ImageUseVectors.App {
    /// <summary>
    /// Avalonia <see cref="Bitmap"/> util.
    /// </summary>
    internal static class AvaloniaBitmapUtil {

        /// <summary>
        /// Get <see cref="Bitmap"/> summary.
        /// </summary>
        /// <param name="source">Source <see cref="Bitmap"/>.</param>
        /// <returns>Returns summary text (返回摘要文本).</returns>
        public static string GetSummary(Bitmap source) {
            var size = source.PixelSize;
            var dpi = source.Dpi;
            string rt = string.Format("{0}, {1}, {2}, {3}, {4}, {5}", size.Width, size.Height, source.Format, source.AlphaFormat, dpi.X, dpi.Y);
            return rt;
        }

        /// <summary>
        /// Is supported pixel format (是支持的像素格式吗).
        /// </summary>
        /// <param name="format">The pixel format (像素格式).</param>
        /// <returns>Return true to indicate support, otherwise it is not supported (返回true表明支持, 否则为不支持).</returns>
        public static bool IsSupportedFormat(PixelFormat? format) {
            bool rt = false;
            if (null == format) return rt;
            if (PixelFormats.Rgba8888 == format
                || PixelFormats.Bgra8888 == format
                || PixelFormats.Rgb24 == format
                || PixelFormats.Rgb32 == format
                || PixelFormats.Bgr24 == format
                || PixelFormats.Bgr32 == format
                ) {
                rt = true;
            }
            return rt;
        }

        /// <summary>
        /// Convert <see cref="Bitmap"/> to <see cref="WriteableBitmap"/> (将 <see cref="Bitmap"/> 转为 <see cref="WriteableBitmap"/>).
        /// </summary>
        /// <param name="source">Source <see cref="Bitmap"/> (源位图).</param>
        /// <param name="allowDispose">Do you dispose <paramref name="source"/> after created Writeablebitmap (当创建 WriteableBitmap后，是否释放 <paramref name="source"/>).</param>
        /// <returns>Returns created <see cref="WriteableBitmap"/> (返回创建的 <see cref="WriteableBitmap"/>).</returns>
        public static WriteableBitmap ToWriteableBitmap(this Bitmap source, bool allowDispose = false) {
            if (source is WriteableBitmap temp) {
                return temp;
            }
            WriteableBitmap writeableBitmap = new WriteableBitmap(
                source.PixelSize,
                source.Dpi,
                source.Format,
                source.AlphaFormat
            );
            AlphaFormat alphaFormat = writeableBitmap.AlphaFormat ?? AlphaFormat.Opaque;
            using (var fb = writeableBitmap.Lock()) {
                source.CopyPixels(fb, alphaFormat);
            }
            if (allowDispose) {
                source.Dispose();
            }
            return writeableBitmap;
        }

        public static bool TryGetPixelColor(this WriteableBitmap source, int x, int y, out System.Drawing.Color color) {
            return TryGetPixelColor(source, x, y, out color, out _, Span<byte>.Empty);
        }

        public static unsafe bool TryGetPixelColor(this WriteableBitmap source, int x, int y, out System.Drawing.Color color, out int pixelByte, Span<byte> buffer) {
            color = default;
            pixelByte = 0;
            PixelSize pixelSize = source.PixelSize;
            if (0 <= x && x < pixelSize.Width && 0 <= y && y < pixelSize.Height) {
                PixelFormat? format = source.Format;
                bool isSupported = IsSupportedFormat(format);
                if (isSupported) {
                    using (var fb = source.Lock()) {
                        byte* p = (byte*)fb.Address + fb.RowBytes * y;
                        if (PixelFormats.Rgba8888 == format) {
                            pixelByte = 4;
                            p += pixelByte * x;
                            color = System.Drawing.Color.FromArgb(p[3], p[0], p[1], p[2]);
                        } else if (PixelFormats.Bgra8888 == format) {
                            pixelByte = 4;
                            p += pixelByte * x;
                            color = System.Drawing.Color.FromArgb(p[3], p[2], p[1], p[0]);
                        } else if (PixelFormats.Rgb24 == format) {
                            pixelByte = 3;
                            p += pixelByte * x;
                            color = System.Drawing.Color.FromArgb(p[0], p[1], p[2]);
                        } else if (PixelFormats.Rgb32 == format) {
                            pixelByte = 4;
                            p += pixelByte * x;
                            color = System.Drawing.Color.FromArgb(p[0], p[1], p[2]);
                        } else if (PixelFormats.Bgr24 == format) {
                            pixelByte = 3;
                            p += pixelByte * x;
                            color = System.Drawing.Color.FromArgb(p[2], p[1], p[0]);
                        } else if (PixelFormats.Bgr32 == format) {
                            pixelByte = 4;
                            p += pixelByte * x;
                            color = System.Drawing.Color.FromArgb(p[2], p[1], p[0]);
                        }
                        if (pixelByte > 0) {
                            // Copy to buffer.
                            if (buffer.Length >= pixelByte) {
                                Span<byte> sourceSpan = new Span<byte>(p, pixelByte);
                                sourceSpan.CopyTo(buffer);
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }

}
