using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace System.ComponentModel.Composition.Hosting
// ReSharper restore CheckNamespace
{
    public class CompositionContainer
    {
        private DirectoryCatalog _catalog;

        public CompositionContainer(DirectoryCatalog catalog)
        {
            // TODO: Complete member initialization
            _catalog = catalog;
        }

        public IEnumerable<Lazy<T>> GetExports<T>()
        {
            return null;
        }
    }
}