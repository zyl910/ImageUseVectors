using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ImageUseVectors.ViewModels {

    /// <summary>
    /// Base ViewModel (基本视图模型).
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged, INotifyPropertyChanging {

        private event PropertyChangingEventHandler? PropertyChangingHandler;

        private event PropertyChangedEventHandler? PropertyChangedHandler;

        /// <inheritdoc/>
        public event PropertyChangingEventHandler? PropertyChanging {
            add {
                PropertyChangingHandler += value;
            }
            remove {
                PropertyChangingHandler -= value;
            }
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged {
            add {
                PropertyChangedHandler += value;
            }
            remove {
                PropertyChangedHandler -= value;
            }
        }

        /// <summary>
        /// Raise <see cref="PropertyChanging"/> event (触发 <see cref="PropertyChanging"/> 事件).
        /// </summary>
        /// <param name="args">Event arguments (事件参数).</param>
        public void RaisePropertyChanging(PropertyChangingEventArgs args) {
            this.PropertyChangingHandler?.Invoke(this, args);
        }

        /// <summary>
        /// Raise <see cref="PropertyChanged"/> event (触发 <see cref="PropertyChanged"/> 事件).
        /// </summary>
        /// <param name="args">Event arguments (事件参数).</param>
        public void RaisePropertyChanged(PropertyChangedEventArgs args) {
            this.PropertyChangedHandler?.Invoke(this, args);
        }

    }

}
