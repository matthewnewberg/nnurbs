using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a geometric point.
    /// <para>This is fundamentally a class that derives from
    /// <see cref="GeometryBase"/> and contains a single <see cref="Point3d"/> location.</para>
    /// </summary>
    [Serializable]

    [XmlType(TypeName = "PointGeometry")]
    public class Point : GeometryBase
    {
        public Point()
        { }

        /// <summary>
        /// Gets or sets the location (position) of this point.
        /// </summary>
        public Point3d Location;


    }
}
