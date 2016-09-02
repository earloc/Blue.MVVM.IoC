#if !NO_TPL

using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.IoC {

    /// <summary>
    /// Extensions for ITypeResolver
    /// </summary>
    public static class ITypeResolverExtensions {
        /// <summary>
        /// resolves T and executes constructionConfig on the resolved instance prior it is returned. This can be used as an async, DI - aware constructor equivalent
        /// </summary>
        /// <typeparam name="T">the type to be resolved</typeparam>
        /// <param name="source">the discrete ITypeResolver implementation that actually resolves type T</param>
        /// <param name="constructionConfig">the 'construction' logic that should be executed directly after resolving the instance of type T</param>
        /// <returns></returns>
        public static async Task<T> ResolveAsync<T>(this ITypeResolver source, Func<T, Task> constructionConfig = null) {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "must not be null");

            var instance = source.Resolve<T>();

            if (constructionConfig != null)
                await constructionConfig((T)instance);
            return instance;
        }

        /// <summary>
        /// resolves T and executes constructionConfig on the resolved instance prior it is returned. This can be used as a sync, DI - aware constructor equivalent
        /// also ensures, that Type is castable to T
        /// </summary>
        /// <typeparam name="T">the type to be resolved</typeparam>
        /// <param name="source">the discrete ITypeResolver implementation that actually resolves type T</param>
        /// <param name="constructionConfig">the 'construction' logic that should be executed directly after resolving the instance of type T</param>
        /// <returns></returns>
        public static async Task<T> ResolveAsync<T>(this ITypeResolver source, Action<T> constructionConfig = null) {
            return await ResolveAsync<T>(source, async x => {
                constructionConfig?.Invoke(x);
#if BACKPORTED_TPL
                await TaskEx.Yield();
#else
                await Task.Yield();
#endif
            });
        }
    }
}
#endif
