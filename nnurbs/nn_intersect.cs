using System;
using System.Collections.Generic;

namespace NN.Geometry.Intersect
{
    /// <summary>
    /// Provides static methods for the computation of intersections, projections, sections and similar.
    /// </summary>
    public static class Intersection
    {
        #region analytic
        /// <summary>
        /// Intersects two lines.
        /// </summary>
        /// <param name="lineA">First line for intersection.</param>
        /// <param name="lineB">Second line for intersection.</param>
        /// <param name="a">
        /// Parameter on lineA that is closest to LineB. 
        /// The shortest distance between the lines is the chord from lineA.PointAt(a) to lineB.PointAt(b)
        /// </param>
        /// <param name="b">
        /// Parameter on lineB that is closest to LineA. 
        /// The shortest distance between the lines is the chord from lineA.PointAt(a) to lineB.PointAt(b)
        /// </param>
        /// <param name="tolerance">
        /// If tolerance > 0.0, then an intersection is reported only if the distance between the points is &lt;= tolerance. 
        /// If tolerance &lt;= 0.0, then the closest point between the lines is reported.
        /// </param>
        /// <param name="finiteSegments">
        /// If true, the input lines are treated as finite segments. 
        /// If false, the input lines are treated as infinite lines.
        /// </param>
        /// <returns>
        /// true if a closest point can be calculated and the result passes the tolerance parameter test; otherwise false.
        /// </returns>
        /// <remarks>
        /// If the lines are exactly parallel, meaning the system of equations used to find a and b 
        /// has no numerical solution, then false is returned. If the lines are nearly parallel, which 
        /// is often numerically true even if you think the lines look exactly parallel, then the 
        /// closest points are found and true is returned. So, if you care about weeding out "parallel" 
        /// lines, then you need to do something like the following:
        /// <code lang="cs">
        /// bool rc = Intersect.LineLine(lineA, lineB, out a, out b, tolerance, segments);
        /// if (rc)
        /// {
        ///   double angle_tol = RhinoMath.ToRadians(1.0); // or whatever
        ///   double parallel_tol = Math.Cos(angle_tol);
        ///   if ( Math.Abs(lineA.UnitTangent * lineB.UnitTangent) >= parallel_tol )
        ///   {
        ///     ... do whatever you think is appropriate
        ///   }
        /// }
        /// </code>
        /// <code lang="vb">
        /// Dim rc As Boolean = Intersect.LineLine(lineA, lineB, a, b, tolerance, segments)
        /// If (rc) Then
        ///   Dim angle_tol As Double = RhinoMath.ToRadians(1.0) 'or whatever
        ///   Dim parallel_tolerance As Double = Math.Cos(angle_tol)
        ///   If (Math.Abs(lineA.UnitTangent * lineB.UnitTangent) >= parallel_tolerance) Then
        ///     ... do whatever you think is appropriate
        ///   End If
        /// End If
        /// </code>
        /// </remarks>
        public static bool LineLine(Line lineA, Line lineB, out double a, out double b, double tolerance, bool finiteSegments)
        {
            bool rc = LineLine(lineA, lineB, out a, out b);
            if (rc)
            {
                if (finiteSegments)
                {
                    if (a < 0.0)
                        a = 0.0;
                    else if (a > 1.0)
                        a = 1.0;
                    if (b < 0.0)
                        b = 0.0;
                    else if (b > 1.0)
                        b = 1.0;
                }
                if (tolerance > 0.0)
                {
                    rc = (lineA.PointAt(a).DistanceTo(lineB.PointAt(b)) <= tolerance);
                }
            }
            return rc;
        }
        /// <summary>
        /// Finds the closest point between two infinite lines.
        /// </summary>
        /// <param name="lineA">First line.</param>
        /// <param name="lineB">Second line.</param>
        /// <param name="a">
        /// Parameter on lineA that is closest to lineB. 
        /// The shortest distance between the lines is the chord from lineA.PointAt(a) to lineB.PointAt(b)
        /// </param>
        /// <param name="b">
        /// Parameter on lineB that is closest to lineA. 
        /// The shortest distance between the lines is the chord from lineA.PointAt(a) to lineB.PointAt(b)
        /// </param>
        /// <returns>
        /// true if points are found and false if the lines are numerically parallel. 
        /// Numerically parallel means the 2x2 matrix:
        /// <para>+AoA  -AoB</para>
        /// <para>-AoB  +BoB</para>
        /// is numerically singular, where A = (lineA.To - lineA.From) and B = (lineB.To-lineB.From)
        /// </returns>
        /// <example>
        /// <code source='examples\vbnet\ex_intersectlines.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_intersectlines.cs' lang='cs'/>
        /// <code source='examples\py\ex_intersectlines.py' lang='py'/>
        /// </example>
        public static bool LineLine(Line lineA, Line lineB, out double a, out double b)
        {
            a = 0;
            b = 0;
            // TODO
            return false;
        }
        /// <summary>
        /// Intersects a line and a plane. This function only returns true if the 
        /// intersection result is a single point (i.e. if the line is coincident with 
        /// the plane then no intersection is assumed).
        /// </summary>
        /// <param name="line">Line for intersection.</param>
        /// <param name="plane">Plane to intersect.</param>
        /// <param name="lineParameter">Parameter on line where intersection occurs. 
        /// If the parameter is not within the {0, 1} Interval then the finite segment 
        /// does not intersect the plane.</param>
        /// <returns>true on success, false on failure.</returns>
        public static bool LinePlane(Line line, Plane plane, out double lineParameter)
        {
            lineParameter = 0;

            // TODO
            return false;
        }
        /// <summary>
        /// Intersects two planes and return the intersection line. If the planes are 
        /// parallel or coincident, no intersection is assumed.
        /// </summary>
        /// <param name="planeA">First plane for intersection.</param>
        /// <param name="planeB">Second plane for intersection.</param>
        /// <param name="intersectionLine">If this function returns true, 
        /// the intersectionLine parameter will return the line where the planes intersect.</param>
        /// <returns>true on success, false on failure.</returns>
        public static bool PlanePlane(Plane planeA, Plane planeB, out Line intersectionLine)
        {
            intersectionLine = new Line();

            // TODO
            return false;
        }
        /// <summary>
        /// Intersects three planes to find the single point they all share.
        /// </summary>
        /// <param name="planeA">First plane for intersection.</param>
        /// <param name="planeB">Second plane for intersection.</param>
        /// <param name="planeC">Third plane for intersection.</param>
        /// <param name="intersectionPoint">Point where all three planes converge.</param>
        /// <returns>true on success, false on failure. If at least two out of the three planes 
        /// are parallel or coincident, failure is assumed.</returns>
        public static bool PlanePlanePlane(Plane planeA, Plane planeB, Plane planeC, out Point3d intersectionPoint)
        {
            intersectionPoint = new Point3d();
            // TODO
            return false;
        }

        /// <summary>
        /// Intersects a plane with a circle using exact calculations.
        /// </summary>
        /// <param name="plane">Plane to intersect.</param>
        /// <param name="circle">Circe to intersect.</param>
        /// <param name="firstCircleParameter">First intersection parameter on circle if successful or RhinoMath.UnsetValue if not.</param>
        /// <param name="secondCircleParameter">Second intersection parameter on circle if successful or RhinoMath.UnsetValue if not.</param>
        /// <returns>The type of intersection that occured.</returns>
        public static PlaneCircleIntersection PlaneCircle(Plane plane, Circle circle, out double firstCircleParameter, out double secondCircleParameter)
        {
            firstCircleParameter = RhinoMath.UnsetValue;
            secondCircleParameter = RhinoMath.UnsetValue;

            if (plane.ZAxis.IsParallelTo(circle.Plane.ZAxis, RhinoMath.ZeroTolerance * Math.PI) != 0)
            {
                if (Math.Abs(plane.DistanceTo(circle.Center)) < RhinoMath.ZeroTolerance)
                    return PlaneCircleIntersection.Coincident;
                return PlaneCircleIntersection.Parallel;
            }

            Line L;

            //At this point, the PlanePlane should never fail since I already checked for parallellillity.
            if (!PlanePlane(plane, circle.Plane, out L)) { return PlaneCircleIntersection.Parallel; }

            double Lt = L.ClosestParameter(circle.Center);
            Point3d Lp = L.PointAt(Lt);

            double d = circle.Center.DistanceTo(Lp);

            //If circle radius equals the projection distance, we have a tangent intersection.
            if (Math.Abs(d - circle.Radius) < RhinoMath.ZeroTolerance)
            {
                firstCircleParameter = 0;
                // TODO
                //circle.ClosestParameter(Lp, out firstCircleParameter);
                secondCircleParameter = firstCircleParameter;
                return PlaneCircleIntersection.Tangent;
            }

            //If circle radius too small to get an intersection, then abort.
            if (d > circle.Radius) { return PlaneCircleIntersection.None; }

            double offset = Math.Sqrt((circle.Radius * circle.Radius) - (d * d));
            Vector3d dir = offset * L.UnitTangent;

            // TODO
            //if (!circle.ClosestParameter(Lp + dir, out firstCircleParameter)) { return PlaneCircleIntersection.None; }
            //if (!circle.ClosestParameter(Lp - dir, out secondCircleParameter)) { return PlaneCircleIntersection.None; }

            return PlaneCircleIntersection.Secant;
        }
        /// <summary>
        /// Intersects a plane with a sphere using exact calculations.
        /// </summary>
        /// <param name="plane">Plane to intersect.</param>
        /// <param name="sphere">Sphere to intersect.</param>
        /// <param name="intersectionCircle">Intersection result.</param>
        /// <returns>If <see cref="PlaneSphereIntersection.None"/> is returned, the intersectionCircle has a radius of zero and the center point 
        /// is the point on the plane closest to the sphere.</returns>
        public static PlaneSphereIntersection PlaneSphere(Plane plane, Sphere sphere, out Circle intersectionCircle)
        {
            intersectionCircle = new Circle();

            // TODO
            int rc = 0;

            return (PlaneSphereIntersection)rc;
        }
        /// <summary>
        /// Intersects a line with a circle using exact calculations.
        /// </summary>
        /// <param name="line">Line for intersection.</param>
        /// <param name="circle">Circle for intersection.</param>
        /// <param name="t1">Parameter on line for first intersection.</param>
        /// <param name="point1">Point on circle closest to first intersection.</param>
        /// <param name="t2">Parameter on line for second intersection.</param>
        /// <param name="point2">Point on circle closest to second intersection.</param>
        /// <returns>
        /// If <see cref="LineCircleIntersection.Single"/> is returned, only t1 and point1 will have valid values. 
        /// If <see cref="LineCircleIntersection.Multiple"/> is returned, t2 and point2 will also be filled out.
        /// </returns>
        /// <example>
        /// <code source='examples\vbnet\ex_intersectlinecircle.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_intersectlinecircle.cs' lang='cs'/>
        /// <code source='examples\py\ex_intersectlinecircle.py' lang='py'/>
        /// </example>
        public static LineCircleIntersection LineCircle(Line line, Circle circle, out double t1, out Point3d point1, out double t2, out Point3d point2)
        {
            t1 = 0.0;
            t2 = 0.0;
            point1 = new Point3d();
            point2 = new Point3d();

            if (!line.IsValid || !circle.IsValid) { return LineCircleIntersection.None; }

            int rc = 0;
            // TODO
            return (LineCircleIntersection)rc;
        }
        /// <summary>
        /// Intersects a line with a sphere using exact calculations.
        /// </summary>
        /// <param name="line">Line for intersection.</param>
        /// <param name="sphere">Sphere for intersection.</param>
        /// <param name="intersectionPoint1">First intersection point.</param>
        /// <param name="intersectionPoint2">Second intersection point.</param>
        /// <returns>If <see cref="LineSphereIntersection.None"/> is returned, the first point is the point on the line closest to the sphere and 
        /// the second point is the point on the sphere closest to the line. 
        /// If <see cref="LineSphereIntersection.Single"/> is returned, the first point is the point on the line and the second point is the 
        /// same point on the sphere.</returns>
        public static LineSphereIntersection LineSphere(Line line, Sphere sphere, out Point3d intersectionPoint1, out Point3d intersectionPoint2)
        {
            intersectionPoint1 = new Point3d();
            intersectionPoint2 = new Point3d();


            int rc = 0;
            // TODO
            return (LineSphereIntersection)rc;
        }
        /// <summary>
        /// Intersects a line with a cylinder using exact calculations.
        /// </summary>
        /// <param name="line">Line for intersection.</param>
        /// <param name="cylinder">Cylinder for intersection.</param>
        /// <param name="intersectionPoint1">First intersection point.</param>
        /// <param name="intersectionPoint2">Second intersection point.</param>
        /// <returns>If None is returned, the first point is the point on the line closest
        /// to the cylinder and the second point is the point on the cylinder closest to
        /// the line. 
        /// <para>If <see cref="LineCylinderIntersection.Single"/> is returned, the first point
        /// is the point on the line and the second point is the  same point on the
        /// cylinder.</para></returns>
        public static LineCylinderIntersection LineCylinder(Line line, Cylinder cylinder, out Point3d intersectionPoint1, out Point3d intersectionPoint2)
        {
            intersectionPoint1 = new Point3d();
            intersectionPoint2 = new Point3d();

            int rc = 0;
            // TODO            
            return (LineCylinderIntersection)rc;
        }
        /// <summary>
        /// Intersects two spheres using exact calculations.
        /// </summary>
        /// <param name="sphereA">First sphere to intersect.</param>
        /// <param name="sphereB">Second sphere to intersect.</param>
        /// <param name="intersectionCircle">
        /// If intersection is a point, then that point will be the center, radius 0.
        /// </param>
        /// <returns>
        /// The intersection type.
        /// </returns>
        public static SphereSphereIntersection SphereSphere(Sphere sphereA, Sphere sphereB, out Circle intersectionCircle)
        {
            intersectionCircle = new Circle();

            int rc = 0;

            if (rc <= 0 || rc > 3)
            {
                return SphereSphereIntersection.None;
            }

            return (SphereSphereIntersection)rc;
        }
        /// <summary>
        /// Intersects an infinite line and an axis aligned bounding box.
        /// </summary>
        /// <param name="box">BoundingBox to intersect.</param>
        /// <param name="line">Line for intersection.</param>
        /// <param name="tolerance">
        /// If tolerance &gt; 0.0, then the intersection is performed against a box 
        /// that has each side moved out by tolerance.
        /// </param>
        /// <param name="lineParameters">
        /// The chord from line.PointAt(lineParameters.T0) to line.PointAt(lineParameters.T1) is the intersection.
        /// </param>
        /// <returns>true if the line intersects the box, false if no intersection occurs.</returns>
        public static bool LineBox(Line line, BoundingBox box, double tolerance, out Interval lineParameters)
        {
            lineParameters = new Interval();
            // TODO
            return false;
        }
        /// <summary>
        /// Intersects an infinite line with a box volume.
        /// </summary>
        /// <param name="box">Box to intersect.</param>
        /// <param name="line">Line for intersection.</param>
        /// <param name="tolerance">
        /// If tolerance &gt; 0.0, then the intersection is performed against a box 
        /// that has each side moved out by tolerance.
        /// </param>
        /// <param name="lineParameters">
        /// The chord from line.PointAt(lineParameters.T0) to line.PointAt(lineParameters.T1) is the intersection.
        /// </param>
        /// <returns>true if the line intersects the box, false if no intersection occurs.</returns>
        public static bool LineBox(Line line, Box box, double tolerance, out Interval lineParameters)
        {
            BoundingBox bbox = new BoundingBox(new Point3d(box.X.Min, box.Y.Min, box.Z.Min),
                                               new Point3d(box.X.Max, box.Y.Max, box.Z.Max));
            Transform xform = Transform.ChangeBasis(Plane.WorldXY, box.Plane);
            line.Transform(xform);

            return LineBox(line, bbox, tolerance, out lineParameters);
        }
        #endregion

        #region sections

        /// <summary>
        /// Utility function for creating a PlaneSurface through a Box.
        /// </summary>
        /// <param name="plane">Plane to extend.</param>
        /// <param name="box">Box to extend through.</param>
        /// <param name="fuzzyness">Box will be inflated by this amount.</param>
        /// <returns>A Plane surface through the box or null.</returns>
        internal static PlaneSurface ExtendThroughBox(Plane plane, BoundingBox box, double fuzzyness)
        {
            if (fuzzyness != 0.0) { box.Inflate(fuzzyness); }

            Point3d[] corners = box.GetCorners();
            int side = 0;
            bool valid = false;

            for (int i = 0; i < corners.Length; i++)
            {
                double d = plane.DistanceTo(corners[i]);
                if (d == 0.0) { continue; }

                if (d < 0.0)
                {
                    if (side > 0) { valid = true; break; }
                    side = -1;
                }
                else
                {
                    if (side < 0) { valid = true; break; }
                    side = +1;
                }
            }

            if (!valid) { return null; }

            Interval s, t;
            if (!plane.ExtendThroughBox(box, out s, out t)) { return null; }

            if (s.IsSingleton || t.IsSingleton)
                return null;

            var p = new PlaneSurface();

            // TODO Plane Surface

            return p;
        }
        #endregion

    }

    /// <summary>
    /// Represents all possible cases of a Plane|Circle intersection event.
    /// </summary>
    public enum PlaneCircleIntersection : int
    {
        /// <summary>
        /// No intersections. Either because radius is too small or because circle plane is parallel but not coincident with the intersection plane.
        /// </summary>
        None = 0,

        /// <summary>
        /// Tangent (one point) intersection.
        /// </summary>
        Tangent = 1,

        /// <summary>
        /// Secant (two point) intersection.
        /// </summary>
        Secant = 2,

        /// <summary>
        /// Circle and plane are planar but not coincident. 
        /// Parallel indicates no intersection took place.
        /// </summary>
        Parallel = 3,

        /// <summary>
        /// Circle and plane are co-planar, they intersect everywhere.
        /// </summary>
        Coincident = 4
    }

    /// <summary>
    /// Represents all possible cases of a Plane|Sphere intersection event.
    /// </summary>
    public enum PlaneSphereIntersection : int
    {
        /// <summary>
        /// No intersections.
        /// </summary>
        None = 0,

        /// <summary>
        /// Tangent intersection.
        /// </summary>
        Point = 1,

        /// <summary>
        /// Circular intersection.
        /// </summary>
        Circle = 2,
    }

    /// <summary>
    /// Represents all possible cases of a Line|Circle intersection event.
    /// </summary>
    public enum LineCircleIntersection : int
    {
        /// <summary>
        /// No intersections.
        /// </summary>
        None = 0,

        /// <summary>
        /// One intersection.
        /// </summary>
        Single = 1,

        /// <summary>
        /// Two intersections.
        /// </summary>
        Multiple = 2,
    }

    /// <summary>
    /// Represents all possible cases of a Line|Sphere intersection event.
    /// </summary>
    public enum LineSphereIntersection : int
    {
        /// <summary>
        /// No intersections.
        /// </summary>
        None = 0,

        /// <summary>
        /// One intersection.
        /// </summary>
        Single = 1,

        /// <summary>
        /// Two intersections.
        /// </summary>
        Multiple = 2,
    }

    /// <summary>
    /// Represents all possible cases of a Line|Cylinder intersection event.
    /// </summary>
    public enum LineCylinderIntersection : int
    {
        /// <summary>
        /// No intersections.
        /// </summary>
        None = 0,

        /// <summary>
        /// One intersection.
        /// </summary>
        Single = 1,

        /// <summary>
        /// Two intersections.
        /// </summary>
        Multiple = 2,

        /// <summary>
        /// Line lies on cylinder.
        /// </summary>
        Overlap = 3
    }

    /// <summary>
    /// Represents all possible cases of a Sphere|Sphere intersection event.
    /// </summary>
    public enum SphereSphereIntersection : int
    {
        /// <summary>
        /// Spheres do not intersect.
        /// </summary>
        None = 0,

        /// <summary>
        /// Spheres touch at a single point.
        /// </summary>
        Point = 1,

        /// <summary>
        /// Spheres intersect at a circle.
        /// </summary>
        Circle = 2,

        /// <summary>
        /// Spheres are identical.
        /// </summary>
        Overlap = 3
    }
}