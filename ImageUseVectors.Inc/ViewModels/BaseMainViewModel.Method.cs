#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zyl.VectorTraits;

namespace ImageUseVectors.ViewModels {
    partial class BaseMainViewModel<TBitmap> {

        /// <summary>The view notify (视图通知).</summary>
        private IMainViewNotify? _notify;

        /// <inheritdoc cref="IMainViewNotify.OutputLog(string?)"/>
        public void OutputLog(string? text) {
            _notify?.OutputLog(text);
        }

        /// <inheritdoc cref="IMainViewNotify.OutputClear()"/>
        public void OutputClear() {
            _notify?.OutputClear();
        }

        /// <inheritdoc cref="IMainViewNotify.OutputCopy()"/>
        public void OutputCopy() {
            _notify?.OutputCopy();
        }

        public void OnOutputInfo() {
            //OnOutputInfoSystem();
            //OutputLog(string.Empty);
            OnOutputInfoImage();
        }

        public void OnOutputInfoImage() {

        }

        public void OnOutputInfoSystem() {
            OutputLog("ImageOp");
            OutputLog(string.Empty);
            var writer = new StringWriter();
            TraitsOutput.OutputEnvironment(writer);
            //writer.WriteLine("AdvSimd.IsSupported: {0}", AdvSimd.IsSupported);
            //writer.WriteLine("PackedSimd.IsSupported: {0}", PackedSimd.IsSupported);

            // Show cpu info.
            writer.WriteLine();
            OnOutputInfoCpu(writer);

            // done.
            OutputLog(writer.ToString());
        }

        private void OnOutputInfoCpu(StringWriter writer) {
            writer.WriteLine("[CpuInfo]");
            writer.WriteLine("CpuModelName: {0}", VectorEnvironment.CpuModelName);
            writer.WriteLine("CpuFlags: {0}", VectorEnvironment.CpuFlags);
            writer.WriteLine("CpuDetectionException: {0}", VectorEnvironment.CpuDetectionException);
            writer.WriteLine("CpuDetectionCommand: {0}", VectorEnvironment.CpuDetectionCommand);
            writer.Write("CpuDetectionResult:\t");
            VectorTextUtil.WriteLines(writer, VectorEnvironment.CpuDetectionResult);
            writer.WriteLine();
            // CpuSummary
            CpuSummary = VectorEnvironment.CpuModelName;
            CpuSummaryFull = string.Format("{0} ({1})", VectorEnvironment.CpuModelName, VectorEnvironment.SupportedInstructionSets);
            if (string.IsNullOrWhiteSpace(CpuSummary)) CpuSummary = "(Unknown)";
        }

        /// <inheritdoc cref="_notify"/>
        public IMainViewNotify? Notify {
            get {
                return _notify;
            }
            set {
                _notify = value;
            }
        }

    }
}
