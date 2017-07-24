using System;
using System.Collections.Generic;
using System.Text;

namespace Blue.MVVM.IoC {
    public class ServiceLocator {

        public static void Use(IServiceLocator serviceLocator) {
            var instance = Instance;
            if (instance != null)
                throw new Exception($"{nameof(ServiceLocator)} was already initialized");

            lock (_Lock) {
                instance = Instance;
                if (instance != null)
                    throw new Exception($"{nameof(ServiceLocator)} was already initialized");

                Instance = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator), "must not be null");
            }
        }

        private static object _Lock = new object();

        public static IServiceLocator Instance {get; private set;}

    }
}
