using System;
using System.Collections.Generic;
using Base;
using Environment.Exceptions;

namespace Environment
{
    /// <summary>
    /// Enumeration of general directions in environment
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Forwards direction
        /// </summary>
        Forwards,
        /// <summary>
        /// Backwards direction
        /// </summary>
        Backwards,
        /// <summary>
        /// Left direction
        /// </summary>
        Left,
        /// <summary>
        /// Right direction
        /// </summary>
        Right,
        /// <summary>
        /// No direction, or stop
        /// </summary>
        None
    }

    /// <summary>
    /// A coordinate is a collection of CoordinateComponents
    /// </summary>
    abstract public class Coordinate
    {
        /// <summary>
        /// The list of components of this class
        /// </summary>
        protected Dictionary<char, CoordinateComponent> Components;

        /// <summary>
        /// Abstract to force inheritors to define which components the coordinate implementation is permitted
        /// </summary>
        abstract protected List<char> PermittedComponents { get; }

        /// <summary>
        /// Helper to add components (checking they're valid first)
        /// </summary>
        /// <param name="cc"></param>
        public void addComponent(CoordinateComponent cc)
        {
            if (!this.PermittedComponents.Contains(cc.Dimension))
            {
                throw new InvalidDimensionCoordinateException(901,
                    new Base.Messages(
                        "The coordinate component added (" + cc.Dimension + ") was not within the permitted components list of this object",
                        "Prohibited coordinate component",
                        "The coordinate component (" + cc.Dimension + ") is not permitted in this type of coordinate",
                        "An error occurred in the coordinate space"
                        ),
                        this,
                        Base.Log.Level.Alert
                    );
            }
            Log.Send(Log.Level.Debug, 10011, "Component added to coordinate", "Component added in dimension " + cc.Dimension.ToString(), this);
            Components[cc.Dimension] = cc;
        }

        /// <summary>
        /// Returns a specific component from the coordinate
        /// </summary>
        /// <param name="dim">The dimension to get the coordinate for</param>
        /// <returns>The entire CoordinateComponent object</returns>
        public CoordinateComponent getComponent(char dim)
        {
            if (Components.ContainsKey(dim))
            {
                Log.Send(Log.Level.Debug, 10012, "Returning component from Coordinate", "Component " + dim.ToString() + " returned from coordinate", this);
                return Components[dim];
            }
            throw new InvalidDimensionCoordinateException(902,
                new Base.Messages(
                    "The coordinate component requested (" + dim.ToString() + ") does not exist in this coordinate",
                    "Unavailable coordinate component",
                    "The coordinate component (" + dim.ToString() + ") is not present in this type of coordinate",
                    "An error occurred in the coordinate space"
                    ),
                    this,
                    Base.Log.Level.Alert
                );
        }

        /// <summary>
        /// Returns the component value a specific dimension
        /// </summary>
        /// <param name="dim">The dimension to get value for</param>
        /// <returns>The value of that dimension</returns>
        public double getComponentValue(char dim)
        {
            if (Components.ContainsKey(dim))
            {
                Log.Send(Log.Level.Debug, 10013, "Returning component value from coordinate", "Component " + dim.ToString() + " returned from coordiante", this);
                return Components[dim].Value;
            }
            throw new InvalidDimensionCoordinateException(903,
                new Base.Messages(
                    "The coordinate component requested (" + dim.ToString() + ") does not exist in this coordinate",
                    "Unavailable coordinate component",
                    "The coordinate component (" + dim.ToString() + ") is not present in this type of coordinate",
                    "An error occurred in the coordinate space"
                    ),
                    this,
                    Base.Log.Level.Alert
                );
        }

        /// <summary>
        /// Returns the vector length of the coordinate
        /// </summary>
        /// <returns>Length float</returns>
        public double getVectorLength()
        {
            List<char> dimensions = PermittedComponents;
            double total = 0;
            foreach (char dimension in dimensions)
            {
                total += Math.Pow(this.getComponentValue(dimension), 2);
            }
            Log.Send(Log.Level.Debug, 10014, "Computed vector length of coordinate", "Coordinate length computed as " + Math.Sqrt(total).ToString(), this);
            return Math.Sqrt(total);
        }
    }
}
