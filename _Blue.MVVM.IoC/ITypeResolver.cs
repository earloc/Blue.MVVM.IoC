using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.IoC {
    /// <summary>
    /// slick interface for resolving instances of a type
    /// </summary>
    public interface ITypeResolver {
        /// <summary>
        /// resolves and returns an instance for type T
        /// </summary>
        /// <typeparam name="T">type to be resolved</typeparam>
        /// <returns></returns>
        T Resolve<T>();
        /// <summary>
        /// resolves and returns an instance for type Type and ensures that this instance is castable to T
        /// </summary>
        /// <typeparam name="T">the type that is beeing cast to</typeparam>
        /// <param name="type">the type to be resolved</param>
        /// <returns></returns>
        T Resolve<T>(Type type);
    }
}
