#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUseVectors.ViewModels {

    /// <summary>
    /// Interface of <see cref="MainView"/> notify (MainView通知的接口).
    /// </summary>
    public interface IMainViewNotify {

        /// <summary>
        /// Output log (输出日志).
        /// </summary>
        /// <param name="text">The text (文本).</param>
        public void OutputLog(string? text);

        /// <summary>
        /// Clear output (清空输出).
        /// </summary>
        public void OutputClear();

        /// <summary>
        /// Copy output (复制输出).
        /// </summary>
        public void OutputCopy();

    }
}
