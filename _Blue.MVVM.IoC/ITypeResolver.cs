using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.IoC {
    public interface ITypeResolver {
        T Resolve<T>();
        T Resolve<T>(Type type) where T : class;
    }
}
