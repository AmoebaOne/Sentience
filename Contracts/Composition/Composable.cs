using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Contracts.Composition
{
    /// <summary>
    /// Base composable class, abstract as Compose requires an implemetation
    /// but not an interface because Contain is standard on ComposablePartCatalog
    /// </summary>
    public abstract class Composable
    {
        /// <summary>
        /// The Compose function to start the composition, which should be called
        /// where required by the Composable class
        /// </summary>
        abstract public void Compose();

        /// <summary>
        /// A helper for Compose which runs the container for us
        /// </summary>
        /// <param name="container">The container to use</param>
        protected void Contain(CompositionContainer container)
        {
            container.ComposeParts(this);
        }
    }
}
