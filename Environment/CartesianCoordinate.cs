using System.Collections.Generic;

namespace Environment
{
    /// <summary>
    /// A cartesian (x, y, z) coordinate
    /// </summary>
    public class CartesianCoordinate : Coordinate
    {
        /// <summary>
        /// Define which components are permitted
        /// </summary>
        override protected List<char> PermittedComponents
        {
            get
            {
                return new List<char>() { 'x', 'y', 'z' };
            }
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public CartesianCoordinate() { }

        /// <summary>
        /// Constructor with optional x, y, z
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="z">z coordinate</param>
        public CartesianCoordinate(CoordinateComponent x = null, CoordinateComponent y = null, CoordinateComponent z = null)
        {
            if (x != null)
            {
                this.addComponent(x);
            }
            if (y != null)
            {
                this.addComponent(y);
            }
            if (z != null)
            {
                this.addComponent(z);
            }
        }
    }
}
