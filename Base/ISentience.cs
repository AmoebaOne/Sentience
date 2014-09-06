
namespace Base
{
    /// <summary>
    /// Base interface for just about everything working with Sentience contracts
    /// </summary>
    public interface ISentience
    {
        /// <summary>
        /// Provides the configuration to the interface element
        /// 
        /// Must be called *before* initialisation
        /// </summary>
        /// <param name="Configuration"></param>
        void Configure(SentienceConfiguration Configuration);

        /// <summary>
        /// Initialises the interface element
        /// </summary>
        void Initialise();

        /// <summary>
        /// Deactivates the component
        /// </summary>
        void Deactivate();
    }
}
