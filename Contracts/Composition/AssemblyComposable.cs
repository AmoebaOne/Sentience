using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Base;

namespace Contracts.Composition
{
    /// <summary>
    /// Adds assembly composability
    /// </summary>
    public class AssemblyComposable : Composable
    {
        /// <summary>
        /// A static dictionary of containers which we can re-reference
        /// </summary>
        private static Dictionary<Assembly, CompositionContainer> Containers = new Dictionary<Assembly, CompositionContainer>();

        /// <summary>
        /// The default assembly to use (the current)
        /// </summary>
        public Assembly Assembly = Assembly.GetExecutingAssembly();

        /// <summary>
        /// Compose handler
        /// </summary>
        override public void Compose()
        {
            Log.Send(Log.Level.Debug, 10016, "Composing AssemblyComposable", "Composing AssemblyComposable container", this);
            if (!Containers.ContainsKey(Assembly))
            {
                var catalog = new AssemblyCatalog(Assembly);
                var container = new CompositionContainer(catalog);
                Containers[Assembly] = container;
            }
            this.Contain(Containers[Assembly]);
        }
    }
}
