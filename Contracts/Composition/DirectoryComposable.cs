using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using Base;

namespace Contracts.Composition
{
    /// <summary>
    /// Adds directory composable functionality
    /// </summary>
    public class DirectoryComposable : Composable
    {
        /// <summary>
        /// A static dictionary of containers which we can re-reference
        /// </summary>
        private static Dictionary<string, CompositionContainer> Containers = new Dictionary<string, CompositionContainer>();

        /// <summary>
        /// The default directory to use
        /// </summary>
        public string Directory = @".\";

        /// <summary>
        /// Perform composition
        /// </summary>
        override public void Compose()
        {
            Log.Send(Log.Level.Debug, 10017, "Composing DirectoryComposable", "Composing DirectoryComposable container", this);
            if (!Containers.ContainsKey(Directory))
            {
                var catalog = new DirectoryCatalog(Directory);
                var container = new CompositionContainer(catalog);
                Containers[Directory] = container;
            }
            this.Contain(Containers[Directory]);
        }
    }
}
