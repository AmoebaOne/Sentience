using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using Base;

namespace Contracts.Composition
{
    /// <summary>
    /// Provides aggregate composability
    /// </summary>
    public class AggregateComposable : Composable
    {
        /// <summary>
        /// A static dictionary of containers which we can re-reference
        /// </summary>
        private static Dictionary<string, CompositionContainer> Containers = new Dictionary<string, CompositionContainer>();

        /// <summary>
        /// The aggregate catalog to use
        /// </summary>
        public AggregateCatalog Catalog = new AggregateCatalog();

        /// <summary>
        /// The catalog name to use
        /// </summary>
        public string CatalogKey = "Default";

        /// <summary>
        /// Handles composition
        /// </summary>
        override public void Compose()
        {
            Log.Send(Log.Level.Debug, 10015, "Composing AggregateComposable", "Composing AggregateComposable container", this);
            if (!Containers.ContainsKey(CatalogKey))
            {
                var container = new CompositionContainer(Catalog);
                Containers[CatalogKey] = container;
            }
            this.Contain(Containers[CatalogKey]);
        }
    }
}
