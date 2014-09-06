
namespace Environment
{
    /// <summary>
    /// Represents a component in a coordinate
    /// </summary>
    public class CoordinateComponent
    {
        /// <summary>
        /// Character defining which dimension the coordinate is in
        /// </summary>
        public char Dimension { get; set; }

        /// <summary>
        /// The value of the component
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The uncertainty of the position, unknown value -1
        /// </summary>
        public double Uncertainty = -1;

        /// <summary>
        /// Get the sign of the value
        /// </summary>
        public int Sign
        {
            get
            {
                if (Value > 0)
                {
                    return 1;
                }
                else if (Value < 0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            private set {}
        }

        /// <summary>
        /// Constructor for no parameters
        /// </summary>
        public CoordinateComponent() { }

        /// <summary>
        /// Constructor for value only
        /// </summary>
        /// <param name="value">The value</param>
        public CoordinateComponent(double value)
        {
            this.Value = value;
        }
        
        /// <summary>
        /// Constructor for value and uncertainty
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="uncertainty">The value's uncertainty</param>
        public CoordinateComponent(double value, double uncertainty)
        {
            this.Value = value;
            this.Uncertainty = uncertainty;
        }

        /// <summary>
        /// Constructor for dimension only
        /// </summary>
        /// <param name="dim">The dimension character</param>
        public CoordinateComponent(char dim)
        {
            this.Dimension = dim;
        }

        /// <summary>
        /// Constructor for both value and dimension
        /// </summary>
        /// <param name="value">The value of the component</param>
        /// <param name="dim">The direction</param>
        public CoordinateComponent(char dim, double value)
        {
            this.Value = value;
            this.Dimension = dim;
        }

        /// <summary>
        /// Constructor for all parameters
        /// </summary>
        /// <param name="dim">The dimension</param>
        /// <param name="value">The value in that dimension</param>
        /// <param name="uncertainty">The uncertainty of the value</param>
        public CoordinateComponent(char dim, double value, double uncertainty)
        {
            this.Dimension = dim;
            this.Value = value;
            this.Uncertainty = uncertainty;
        }
    }
}
