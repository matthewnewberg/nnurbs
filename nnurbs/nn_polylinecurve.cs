using System;
using NN.Display;
using System.Runtime.Serialization;

namespace NN.Geometry
{
    /// <summary>
    /// Represents the geometry of a set of linked line segments.
    /// <para>This is fundamentally a class that derives from <see cref="Curve"/>
    /// and internally contains a <see cref="Polyline"/>.</para>
    /// </summary>
    [Serializable]
    public class PolylineCurve : Curve
    {
        /// <summary>
        /// Initializes a new empty polyline curve.
        /// </summary>
        public PolylineCurve()
        {
        }

#if RHINO3DMIO
        public PolylineCurve(Rhino.Geometry.PolylineCurve f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.PolylineCurve from)
        {
            ComponentIndex = new ComponentIndex(from.ComponentIndex());

            if (from == null)
                return false;

            this.PointCount = from.PointCount;
            this.Domain = new NN.Geometry.Interval(from.Domain);
            this.Dimension = from.Dimension;
            this.SpanCount = from.SpanCount;
            this.Degree = from.Degree;
            this.IsClosed = from.IsClosed;
            this.IsPeriodic = from.IsPeriodic;

            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;


            this.Points = new System.Collections.Generic.List<Point3d>();

            for (int i = 0; i < this.PointCount; ++i) {
                
                Rhino.Geometry.Point3d p = from.Point(i);
                this.Points.Add(new Point3d(p.X, p.Y, p.Z));
            }

            return true;
        }


        public bool CopyTo(Rhino.Geometry.PolylineCurve to)
        {
            to.Domain = this.Domain.RhinoObject();

            return true;
        }

        public Rhino.Geometry.PolylineCurve RhinoObject()
        {
            var rhinoPoints = new System.Collections.Generic.List<Rhino.Geometry.Point3d>();

            foreach(var p in this.Points)
                rhinoPoints.Add(p.RhinoObject());

            var rhinoCurve = new Rhino.Geometry.PolylineCurve(rhinoPoints);

            rhinoCurve.Domain = this.Domain.RhinoObject();

            return rhinoCurve;

        }

        public override Rhino.Geometry.Curve RhinoCurveObject()
        {
            return RhinoObject();
        }
#endif

        /// Gets the number of points in this polyline.
        /// </summary>
        public int PointCount { get; set; }


        /// <summary>
        /// Gets a point at a specified index in the polyline curve.
        /// </summary>
        /// <param name="index">An index.</param>
        /// <returns>A point.</returns>
        /// 
        public System.Collections.Generic.List<Point3d> Points = new System.Collections.Generic.List<Point3d>();
    }
}
