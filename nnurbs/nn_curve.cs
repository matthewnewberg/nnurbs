using System;
using NN.Collections;
using System.Collections.Generic;
using NN.Runtime.InteropWrappers;
using System.Runtime.Serialization;
// don't wrap ON_MeshCurveParameters. It is only needed for the ON_Curve::MeshCurveFunction

namespace NN.Geometry
{
    /// <summary>
    /// Used in curve and surface blending functions
    /// </summary>
    public enum BlendContinuity : int
    {
        /// <summary></summary>
        Position = 0,
        /// <summary></summary>
        Tangency = 1,
        /// <summary></summary>
        Curvature = 2
    }

    /// <summary>
    /// Defines enumerated values for all implemented corner styles in curve offsets.
    /// </summary>
    public enum CurveOffsetCornerStyle : int
    {
        /// <summary>
        /// The dafault value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Offsets and extends curves with a straight line until they intersect.
        /// </summary>
        Sharp = 1,

        /// <summary>
        /// Offsets and fillets curves with an arc of radius equal to the offset distance.
        /// </summary>
        Round = 2,

        /// <summary>
        /// Offsets and connects curves with a smooth (G1 continuity) curve.
        /// </summary>
        Smooth = 3,

        /// <summary>
        /// Offsets and connects curves with a straight line between their endpoints.
        /// </summary>
        Chamfer = 4
    }

    /// <summary>
    /// Defines enumerated values for knot spacing styles in interpolated curves.
    /// </summary>
    public enum CurveKnotStyle : int
    {
        /// <summary>
        /// Parameter spacing between consecutive knots is 1.0.
        /// </summary>
        Uniform = 0,

        /// <summary>
        /// Chord length spacing, requires degree=3 with CV1 and CVn1 specified.
        /// </summary>
        Chord = 1,

        /// <summary>
        /// Square root of chord length, requires degree=3 with CV1 and CVn1 specified.
        /// </summary>
        ChordSquareRoot = 2,

        /// <summary>
        /// Periodic with uniform spacing.
        /// </summary>
        UniformPeriodic = 3,

        /// <summary>
        /// Periodic with chord length spacing.
        /// </summary>
        ChordPeriodic = 4,

        /// <summary>
        /// Periodic with square roor of chord length spacing. 
        /// </summary>
        ChordSquareRootPeriodic = 5
    }

    /// <summary>
    /// Defines enumerated values for closed curve orientations.
    /// </summary>
    public enum CurveOrientation : int
    {
        /// <summary>
        /// Orientation is undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The curve's orientation is clockwise in the xy plane.
        /// </summary>
        Clockwise = -1,

        /// <summary>
        /// The curve's orientation is counter clockwise in the xy plane.
        /// </summary>
        CounterClockwise = +1
    }

    /// <summary>
    /// Defines enumerated values for closed curve/point spatial relationships.
    /// </summary>
    public enum PointContainment : int
    {
        /// <summary>
        /// Relation is meaningless.
        /// </summary>
        Unset,

        /// <summary>
        /// Point is on the interior of the region implied by the closed curve.
        /// </summary>
        Inside,

        /// <summary>
        /// Point is on the exterior of the region implied by the closed curve.
        /// </summary>
        Outside,

        /// <summary>
        /// Point is coincident with the curve and therefor neither inside not outside.
        /// </summary>
        Coincident
    }

    /// <summary>
    /// Defines enumerated values for closed curve/closed curve relationships.
    /// </summary>
    public enum RegionContainment : int
    {
        /// <summary>
        /// There is no common area between the two regions.
        /// </summary>
        Disjoint = 0,

        /// <summary>
        /// The two curves intersect. There is therefore no full containment relationship either way.
        /// </summary>
        MutualIntersection = 1,

        /// <summary>
        /// Region bounded by curveA (first curve) is inside of curveB (second curve).
        /// </summary>
        AInsideB = 2,

        /// <summary>
        /// Region bounded by curveB (second curve) is inside of curveA (first curve).
        /// </summary>
        BInsideA = 3,
    }

    /// <summary>
    /// Defines enumerated values for styles to use during curve extension, such as "Line", "Arc" or "Smooth".
    /// </summary>
    public enum CurveExtensionStyle : int
    {
        /// <summary>
        /// Curve ends will be propagated linearly according to tangents.
        /// </summary>
        Line = 0,

        /// <summary>
        /// Curve ends will be propagated arc-wise according to curvature.
        /// </summary>
        Arc = 1,

        /// <summary>
        /// Curve ends will be propagated smoothly according to curvature.
        /// </summary>
        Smooth = 2,
    }

    /// <summary>
    /// Enumerates the options to use when simplifying a curve.
    /// </summary>
    [FlagsAttribute]
    public enum CurveSimplifyOptions : int
    {
        /// <summary>
        /// No option is specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Split NurbsCurves at fully multiple knots. 
        /// Effectively turning single nurbs segments with kinks into multiple segments.
        /// </summary>
        SplitAtFullyMultipleKnots = 1,

        /// <summary>
        /// Replace linear segments with LineCurves.
        /// </summary>
        RebuildLines = 2,

        /// <summary>
        /// Replace partially circular segments with ArcCurves.
        /// </summary>
        RebuildArcs = 4,

        /// <summary>
        /// Replace rational nurbscurves with constant weights 
        /// with an equivalent non-rational NurbsCurve.
        /// </summary>
        RebuildRationals = 8,

        /// <summary>
        /// Adjust Curves at G1-joins.
        /// </summary>
        AdjustG1 = 16,

        /// <summary>
        /// Merge adjacent co-linear lines or co-circular arcs 
        /// or combine consecutive line segments into a polyline.
        /// </summary>
        Merge = 32,

        /// <summary>
        /// Implies all of the simplification functions will be used.
        /// </summary>
        All = SplitAtFullyMultipleKnots | RebuildLines | RebuildArcs | RebuildRationals | AdjustG1 | Merge
    }

    /// <summary>
    /// Defines the extremes of a curve through a flagged enumeration. 
    /// </summary>
    /// <example>
    /// <code source='examples\vbnet\ex_extendcurve.vb' lang='vbnet'/>
    /// <code source='examples\cs\ex_extendcurve.cs' lang='cs'/>
    /// <code source='examples\py\ex_extendcurve.py' lang='py'/>
    /// </example>
    [FlagsAttribute]
    public enum CurveEnd : int
    {
        /// <summary>
        /// Not the start nor the end.
        /// </summary>
        None = 0,

        /// <summary>
        /// The frontal part of the curve.
        /// </summary>
        Start = 1,

        /// <summary>
        /// The tail part of the curve.
        /// </summary>
        End = 2,

        /// <summary>
        /// Both the start and the end of the curve.
        /// </summary>
        Both = 3,
    }

    /// <summary>
    /// Defines enumerated values for the options that defines a curve evaluation side when evaluating kinks.
    /// </summary>
    public enum CurveEvaluationSide : int
    {
        /// <summary>
        /// The default evaluation side.
        /// </summary>
        Default = 0,

        /// <summary>
        /// The below evaluation side.
        /// </summary>
        Below = -1,

        /// <summary>
        /// The above evaluation side.
        /// </summary>
        Above = +1
    }

    /// <summary>
    /// Represents a base class that is common to most RhinoCommon curve types.
    /// <para>A curve represents an entity that can be all visited by providing
    /// a single parameter, usually called t.</para>
    /// </summary>
    [Serializable]
    public class Curve : GeometryBase
    {
        /// <summary>
        /// Protected constructor for internal use.
        /// </summary>
        public Curve() { }


#if RHINO3DMIO || RHINOCOMMON

        public static Curve CreateCurve(Rhino.Geometry.Curve f)
        {

            var nurbs = f as Rhino.Geometry.NurbsCurve;

            if (nurbs != null)
                return new NN.Geometry.NurbsCurve(nurbs);


            var polyCurve = f as Rhino.Geometry.PolyCurve;

            if (polyCurve != null)
                return new NN.Geometry.PolyCurve(polyCurve);

            var arcCurve = f as Rhino.Geometry.ArcCurve;

            if (arcCurve != null)
                return new NN.Geometry.ArcCurve(arcCurve);

            var linecurve = f as Rhino.Geometry.LineCurve;

            if (linecurve != null)
                return new NN.Geometry.LineCurve(linecurve);

            var polylineCurve = f as Rhino.Geometry.PolylineCurve;

            if (polylineCurve != null)
                return new NN.Geometry.PolylineCurve(polylineCurve);

            return null;
        }

        public bool CopyFrom(Rhino.Geometry.Curve from)
        {

            return true;
        }


        public bool CopyTo(Rhino.Geometry.Curve to)
        {
            return true;
        }

        public virtual Rhino.Geometry.Curve RhinoCurveObject()
        {
            return null;
        }
#endif

        /// <summary>
        /// Gets or sets the domain of the curve.
        /// </summary>
        public Interval Domain { get; set; }


        /// <summary>
        /// Gets the dimension of the object.
        /// <para>The dimension is typically three. For parameter space trimming
        /// curves the dimension is two. In rare cases the dimension can
        /// be one or greater than three.</para>
        /// </summary>
        public int Dimension { get; set; }


        /// <summary>
        /// Gets the number of non-empty smooth (c-infinity) spans in the curve.
        /// </summary>
        public int SpanCount { get; set; }

        /// <summary>
        /// Gets the maximum algebraic degree of any span
        /// or a good estimate if curve spans are not algebraic.
        /// </summary>
        public int Degree { get; set; }


        public bool IsLinear { get; set; }

        /// <summary>
        /// Several types of Curve can have the form of a polyline
        /// including a degree 1 NurbsCurve, a PolylineCurve,
        /// and a PolyCurve all of whose segments are some form of
        /// polyline. IsPolyline tests a curve to see if it can be
        /// represented as a polyline.
        /// </summary>
        /// <returns>true if this curve can be represented as a polyline; otherwise, false.</returns>
        /// <example>
        /// <code source='examples\vbnet\ex_addradialdimension.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_addradialdimension.cs' lang='cs'/>
        /// <code source='examples\py\ex_addradialdimension.py' lang='py'/>
        /// </example>
        public bool IsPolyline { get; }

        public Polyline CurvePolyLine { get; }
        
        public bool IsClosed { get; set; }

        public bool IsPeriodic { get; set; }
        
        public CurveOrientation ClosedCurveOrientation { get; set; }
        

        public double Length { get; }

        public bool IsShort { get; }

        public System.Collections.Generic.List<Interval> SpanDomain = new List<Interval>();

    }
}
