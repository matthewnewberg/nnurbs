using System;
using System.Collections.Generic;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a Bezier curve.
    /// <para>Note: as an exception, the bezier curve <b>is not</b> derived from <see cref="Curve"/>.</para>
    /// </summary>
    public class BezierCurve
    {

        public bool IsValid { get; set; }


        public BoundingBox BoundingBox { get; }
        public BoundingBox BoundingBoxAccurate { get; }



        /// <summary>
        /// Gets a value indicating whether or not the curve is rational. 
        /// Rational curves have control-points with custom weights.
        /// </summary>
        public bool IsRational { get; set; }

        /// <summary>
        /// Number of control vertices in this curve
        /// </summary>
        public int ControlVertexCount { get; set; }

        public System.Collections.Generic.List<Point2d> ControlVertex2d = new List<Point2d>();

        public System.Collections.Generic.List<Point3d> ControlVertex3d = new List<Point3d>();

        public System.Collections.Generic.List<Point4d> ControlVertex4d = new List<Point4d>();
    }
}
