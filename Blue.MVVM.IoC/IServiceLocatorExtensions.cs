using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.IoC {

    /// <summary>
    /// Extensions for IServiceLocator
    /// </summary>
    public static class IServiceLocatorExtensions {
        /// <summary>
        /// resolves T and executes constructionConfig on the resolved instance prior it is returned. This can be used as an async, DI - aware constructor equivalent
        /// </summary>
        /// <typeparam name="T">the type to be resolved</typeparam>
        /// <param name="source">the discrete IServiceLocator implementation that actually resolves type T</param>
        /// <param name="asyncInitializer">the async 'initialization' logic that should be executed directly after resolving the instance of type T</param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IServiceLocator source, Func<T, Task> asyncInitializer = null) where T : class {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "must not be null");

            var instance = source.Get<T>();

            return await InitializeAsync(instance, asyncInitializer);
        }

        public static async Task<T> GetAsAsync<T>(this IServiceLocator source, Type type, Func<T, Task> asyncInitializer = null) where T : class {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "must not be null");

            var instance = source.GetAs<T>(type);

            return await InitializeAsync(instance, asyncInitializer);
        }

        public static T GetAs<T>(this IServiceLocator source, Type type, bool allownull = false) where T : class {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "must not be null");

            var instance = source.Get(type);

            var typedInstance = instance as T;

            if (instance == null && !allownull)
                throw new Exception($"expected dependency of type {type.FullName} to be compatible with {typeof(T).FullName}");

            return typedInstance;
        }


        private static async Task<T> InitializeAsync<T>(T instance, Func<T, Task> asyncInitializer) {
            if (asyncInitializer != null)
                await asyncInitializer(instance);
            return instance;
        }
    }
}
