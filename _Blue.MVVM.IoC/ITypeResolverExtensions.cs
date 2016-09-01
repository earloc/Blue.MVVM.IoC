#if !NO_TPL

using Blue.MVVM.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Extensions {

    public static class ITypeResolverExtensions {
        public static async Task<T> ResolveAsync<T>(this ITypeResolver source, Func<T, Task> constructionConfig) {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "must not be null");

            var instance = source.Resolve<T>();

            if (constructionConfig != null)
                await constructionConfig(instance);
            return instance;
        }

        public static async Task<T> ResolveAsync<T>(this ITypeResolver source, Action<T> constructionConfig) {
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
