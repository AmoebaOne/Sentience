using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using Base;

namespace Contracts.Composition
{
    /// <summary>
    /// Provides type composability
    /// </summary>
    public class TypeComposable : Composable
    {
        /// <summary>
        /// A static dictionary of containers which we can re-reference
        /// </summary>
        private static Dictionary<Type, CompositionContainer> Containers = new Dictionary<Type, CompositionContainer>();

        /// <summary>
        /// The type to compose on (rubbish default but can't be helped)
        /// </summary>
        public Type Type = typeof(TypeComposable);

        /// <summary>
        /// Handles composing
        /// </summary>
        override public void Compose()
        {
            Log.Send(Log.Level.Debug, 10018, "Composing TypeComposable", "Composing TypeComposable container", this);
            if (!Containers.ContainsKey(Type))
            {
                var catalog = new TypeCatalog(Type);
                var container = new CompositionContainer(catalog);
                Containers[Type] = container;
            }
            this.Contain(Containers[Type]);
        }
    }
}
