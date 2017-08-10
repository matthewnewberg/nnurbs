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
#if RHINO3DMIO || RHINOCOMMON
        public Point(Rhino.Geometry.Point f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Point from)
        {
            this.Location = new Point3d(from.Location);

            return true;
        }


        public bool CopyTo(Rhino.Geometry.Point to)
        {
            to.Location = Location.RhinoObject();

            return true;
        }

        public Rhino.Geometry.Point RhinoObject()
        {
            var rhinoPoint = new Rhino.Geometry.Point(Location.RhinoObject());

            return rhinoPoint;
        }

#endif

        public Point()
        { }

        /// <summary>
        /// Gets or sets the location (position) of this point.
        /// </summary>
        public Point3d Location;


    }
}
