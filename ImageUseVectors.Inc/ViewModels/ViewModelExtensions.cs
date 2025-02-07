#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ImageUseVectors.ViewModels {
    /// <summary>
    /// Extensions of ViewModel (视图模型的扩展).
    /// </summary>
    public static class ViewModelExtensions {

        /// <summary>
        ///     RaiseAndSetIfChanged fully implements a Setter for a read-write property on a
        ///     ReactiveObject, using CallerMemberName to raise the notification and the ref
        ///     to the backing field to set the property.
        /// </summary>
        /// <typeparam name="TObj">The type of the This.</typeparam>
        /// <typeparam name="TRet">The type of the return value.</typeparam>
        /// <param name="reactiveObject">The ReactiveUI.ReactiveObject raising the notification.</param>
        /// <param name="backingField">A Reference to the backing field for this property.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">The name of the property, usually automatically provided through the CallerMemberName attribute.</param>
        /// <returns>The newly set value, normally discarded.</returns>
        public static TRet RaiseAndSetIfChanged<TObj, TRet>(this TObj reactiveObject, ref TRet backingField, TRet newValue, [CallerMemberName] string? propertyName = null) where TObj : BaseViewModel {
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName");
            }

            if (EqualityComparer<TRet>.Default.Equals(backingField, newValue)) {
                return newValue;
            }

            reactiveObject.RaisingPropertyChanging(propertyName);
            backingField = newValue;
            reactiveObject.RaisingPropertyChanged(propertyName);
            return newValue;
        }

        /// <summary>
        /// Use this method in your ReactiveObject classes when creating custom properties where raiseAndSetIfChanged doesn't suffice.
        /// </summary>
        /// <typeparam name="TSender">The sender type.</typeparam>
        /// <param name="reactiveObject">The instance of ReactiveObject on which the property has changed.</param>
        /// <param name="propertyName">A string representing the name of the property that has been changed. Leave null to let the runtime set to caller member name.</param>
        public static void RaisePropertyChanged<TSender>(this TSender reactiveObject, [CallerMemberName] string? propertyName = null) where TSender : BaseViewModel {
            if (propertyName != null) {
                reactiveObject.RaisingPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Use this method in your ReactiveObject classes when creating custom properties where raiseAndSetIfChanged doesn't suffice.
        /// </summary>
        /// <typeparam name="TSender">The sender type.</typeparam>
        /// <param name="reactiveObject">The instance of ReactiveObject on which the property has changed.</param>
        /// <param name="propertyName">A string representing the name of the property that has been changed. Leave null to let the runtime set to caller member name.</param>
        public static void RaisePropertyChanging<TSender>(this TSender reactiveObject, [CallerMemberName] string? propertyName = null) where TSender : BaseViewModel {
            if (propertyName != null) {
                reactiveObject.RaisingPropertyChanging(propertyName);
            }
        }

        internal static void RaisingPropertyChanging<TSender>(this TSender reactiveObject, string propertyName) where TSender : BaseViewModel {
            TSender reactiveObject2 = reactiveObject;
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName");
            }

            reactiveObject2.RaisePropertyChanging(propertyName);
        }

        internal static void RaisingPropertyChanged<TSender>(this TSender reactiveObject, string propertyName) where TSender : BaseViewModel {
            TSender reactiveObject2 = reactiveObject;
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName");
            }

            reactiveObject2.RaisePropertyChanged(propertyName);
        }

    }
}
