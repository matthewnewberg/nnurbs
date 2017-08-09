using NN.Collections;
using System;
using System.Collections.Generic;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a Non Uniform Rational B-Splines (NURBS) curve.
    /// </summary>
    [Serializable]
    public class NurbsCurve : Curve, IEpsilonComparable<NurbsCurve>
    {
        public NurbsCurve() { }


#if RHINO3DMIO || RHINOCOMMON
        public NurbsCurve(Rhino.Geometry.NurbsCurve f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.NurbsCurve from)
        {

            if (from == null)
                return false;

            this.Order = from.Order;
            this.IsRational = from.IsRational;
            this.Knots = new NN.Geometry.Collections.NurbsCurveKnotList(from.Knots);
            this.Points = new NN.Geometry.Collections.NurbsCurvePointList(from.Points);
            //		this.HasBezierSpans = from.HasBezierSpans;// Missing This Prop
            this.Domain = new NN.Geometry.Interval(from.Domain);
            this.Dimension = from.Dimension;
            this.SpanCount = from.SpanCount;
            this.Degree = from.Degree;
            this.IsClosed = from.IsClosed;
            this.IsPeriodic = from.IsPeriodic;
            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;

            ComponentIndex = new ComponentIndex(from.ComponentIndex());

            return true;
        }


        public bool CopyTo(Rhino.Geometry.NurbsCurve f)
        {
            return true;
        }

        public Rhino.Geometry.NurbsCurve RhinoObject()
        {
            var rhinoNurbsCurve = new Rhino.Geometry.NurbsCurve(Dimension, IsRational, Order, Points.Count);

            for (int i = 0; i < Points.Count; ++i)
            {
                rhinoNurbsCurve.Points.SetPoint(i, this.Points[i].m_vertex.RhinoObject());
            }

            for (int i=0; i < Knots.Count; ++i)
            {
                rhinoNurbsCurve.Knots[i] = this.Knots[i];
            }

            rhinoNurbsCurve.Domain = this.Domain.RhinoObject();

            return rhinoNurbsCurve;
        }

        public override Rhino.Geometry.Curve RhinoCurveObject()
        {
            return RhinoObject();
        }
#endif


        /// <summary>
        /// Constructs a new Nurbscurve with a specific degree and control-point count.
        /// </summary>
        /// <param name="degree">Degree of curve. Must be equal to or larger than 1 and smaller than or equal to 11.</param>
        /// <param name="pointCount">Number of control-points.</param>
        public NurbsCurve(int degree, int pointCount)
        {
        }

        /// <summary>
        /// Constructs a new NurbsCurve with knot and CV memory allocated.
        /// </summary>
        /// <param name="dimension">&gt;=1.</param>
        /// <param name="rational">true to make a rational NURBS.</param>
        /// <param name="order">(&gt;= 2) The order=degree+1.</param>
        /// <param name="pointCount">(&gt;= order) number of control vertices.</param>
        /// <example>
        /// <code source='examples\vbnet\ex_addnurbscircle.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_addnurbscircle.cs' lang='cs'/>
        /// <code source='examples\py\ex_addnurbscircle.py' lang='py'/>
        /// </example>
        public NurbsCurve(int dimension, bool rational, int order, int pointCount)
        {

        }

        #region properties
        private Collections.NurbsCurveKnotList m_knots;
        private Collections.NurbsCurvePointList m_points;

        /// <summary>
        /// Gets the order of the curve. Order = Degree + 1.
        /// </summary>
        public int Order { get; set; }


        /// <summary>
        /// Gets a value indicating whether or not the curve is rational. 
        /// Rational curves have control-points with custom weights.
        /// </summary>
        public bool IsRational { get; set; }


        /// <summary>
        /// Gets access to the knots (or "knot vector") of this nurbs curve.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_addnurbscircle.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_addnurbscircle.cs' lang='cs'/>
        /// <code source='examples\py\ex_addnurbscircle.py' lang='py'/>
        /// </example>
        public Collections.NurbsCurveKnotList Knots
        {
            get { return m_knots ?? (m_knots = new NN.Geometry.Collections.NurbsCurveKnotList()); }
            set { m_knots = value; }
        }

        /// <summary>
        /// Gets access to the control points of this nurbs curve.
        /// </summary>
        public Collections.NurbsCurvePointList Points
        {
            get { return m_points ?? (m_points = new Collections.NurbsCurvePointList()); }
            set { m_points = value; }
        }
        #endregion


        System.Collections.Generic.List<double> GrevilleParameter = new List<double>();
        System.Collections.Generic.List<double> GrevillePoint = new List<double>();

        /// <summary>
        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(NurbsCurve other, double epsilon)
        {
            if (null == other) throw new ArgumentNullException("other");

            if (ReferenceEquals(this, other))
                return true;

            if (IsRational != other.IsRational)
                return false;

            if (Degree != other.Degree)
                return false;

            if (Points.Count != other.Points.Count)
                return false;

            if (!Knots.EpsilonEquals(other.Knots, epsilon))
                return false;

            if (!Points.EpsilonEquals(other.Points, epsilon))
                return false;

            return true;
        }
    }

    /// <summary>
    /// Represents control-point geometry with three-dimensional position and weight.
    /// </summary>
    [Serializable]
    public struct ControlPoint : IEpsilonComparable<ControlPoint>
    {
        #region members
        internal Point4d m_vertex;
        #endregion

        #region constructors
        /// <summary>
        /// Constructs a new unweighted control point.
        /// </summary>
        /// <param name="x">X coordinate of Control Point.</param>
        /// <param name="y">Y coordinate of Control Point.</param>
        /// <param name="z">Z coordinate of Control Point.</param>
        public ControlPoint(double x, double y, double z)
        {
            m_vertex = new Point4d(x, y, z, 1.0);
        }
        /// <summary>
        /// Constructs a new weighted control point.
        /// </summary>
        /// <param name="x">X coordinate of Control Point.</param>
        /// <param name="y">Y coordinate of Control Point.</param>
        /// <param name="z">Z coordinate of Control Point.</param>
        /// <param name="weight">Weight factor of Control Point. 
        /// You should not use weights equal to or less than zero.</param>
        public ControlPoint(double x, double y, double z, double weight)
        {
            m_vertex = new Point4d(x, y, z, weight);
        }
        /// <summary>
        /// Constructs a new unweighted control point.
        /// </summary>
        /// <param name="pt">Coordinate of Control Point.</param>
        public ControlPoint(Point3d pt)
        {
            m_vertex = new Point4d(pt.X, pt.Y, pt.Z, 1.0);
        }
        /// <summary>
        /// Constructs a new weighted control point.
        /// </summary>
        /// <param name="pt">Coordinate of Control Point.</param>
        /// <param name="weight">Weight factor of Control Point. 
        /// You should not use weights equal to or less than zero.</param>
        public ControlPoint(Point3d pt, double weight)
        {
            m_vertex = new Point4d(pt.X, pt.Y, pt.Z, weight);
        }
        /// <summary>
        /// Constructs a new weighted control point.
        /// </summary>
        /// <param name="pt">Control point values.</param>
        public ControlPoint(Point4d pt)
        {
            m_vertex = pt;
        }

        /// <summary>
        /// Gets the predefined unset control point.
        /// </summary>
        public static ControlPoint Unset
        {
            get
            {
                ControlPoint rc = new ControlPoint();
                Point3d unset = Point3d.Unset;
                rc.m_vertex.m_x = unset.m_x;
                rc.m_vertex.m_y = unset.m_y;
                rc.m_vertex.m_z = unset.m_z;
                rc.m_vertex.m_w = 1.0;
                return rc;
            }
        }
        #endregion

        #region properties
        /// <summary>
        /// Gets or sets the location of the control point. 
        /// Internally, Rhino stores the location of a weighted control-point 
        /// as a pre-multiplied coordinate, but RhinoCommon always provides 
        /// Euclidean coordinates for control-points, regardless of weight.
        /// </summary>
        public Point3d Location
        {
            get
            {
                return new Point3d(m_vertex.m_x, m_vertex.m_y, m_vertex.m_z);
            }
            set
            {
                m_vertex.m_x = value.m_x;
                m_vertex.m_y = value.m_y;
                m_vertex.m_z = value.m_z;
            }
        }

        /// <summary>
        /// Gets or sets the weight of this control point.
        /// </summary>
        public double Weight
        {
            get
            {
                return m_vertex.m_w;
            }
            set
            {
                m_vertex.m_w = value;
            }
        }
        #endregion

        /// <summary>
        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(ControlPoint other, double epsilon)
        {
            return m_vertex.EpsilonEquals(other.m_vertex, epsilon);
        }
    }
}

namespace NN.Geometry.Collections
{
    /// <summary>
    /// Provides access to the knot vector of a nurbs curve.
    /// </summary>
    public sealed class NurbsCurveKnotList : System.Collections.Generic.List<double>, IRhinoTable<double>, IEpsilonComparable<NurbsCurveKnotList>
    {
        public NurbsCurveKnotList()
        {

        }

#if RHINO3DMIO || RHINOCOMMON
        public NurbsCurveKnotList(Rhino.Geometry.Collections.NurbsCurveKnotList f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Collections.NurbsCurveKnotList from)
        {

            this.Clear();
            this.AddRange(from);

            this.IsClampedStart = from.IsClampedStart;
            this.IsClampedEnd = from.IsClampedEnd;
            return true;
        }


        public bool CopyTo(Rhino.Geometry.Collections.NurbsCurveKnotList to)
        {
            //		to.Count = this.Count;
            //
            //to.IsClampedStart = this.IsClampedStart;
            //to.IsClampedEnd = this.IsClampedEnd;
            return true;
        }
#endif

        public bool IsClampedStart { get; set; }

        public bool IsClampedEnd { get; set; }

        /// <summary>
        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(NurbsCurveKnotList other, double epsilon)
        {
            if (null == other)
                throw new ArgumentNullException("other");

            if (ReferenceEquals(this, other))
                return true;

            if (Count != other.Count)
                return false;

            // check for span equality
            for (int i = 1; i < Count; ++i)
            {
                double my_delta = this[i] - this[i - 1];
                double their_delta = other[i] - other[i - 1];
                if (!RhinoMath.EpsilonEquals(my_delta, their_delta, epsilon))
                    return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Provides access to the control points of a nurbs curve.
    /// </summary>
    public class NurbsCurvePointList : System.Collections.Generic.List<ControlPoint>, IRhinoTable<ControlPoint>, IEpsilonComparable<NurbsCurvePointList>
    {

        public NurbsCurvePointList() { }

#if RHINO3DMIO || RHINOCOMMON
        public NurbsCurvePointList(Rhino.Geometry.Collections.NurbsCurvePointList f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Collections.NurbsCurvePointList from)
        {
            this.Clear();

            foreach (var p in from)
            {
                var l = p.Location;
                this.Add(new ControlPoint(l.X, l.Y, l.Z, p.Weight));

            }

            return true;
        }


        public bool CopyTo(Rhino.Geometry.Collections.NurbsCurvePointList to)
        {
            return true;
        }
#endif

        /// <summary>
        /// Constructs a polyline through all the control points. 
        /// Note that periodic curves generate a closed polyline with <i>fewer</i> 
        /// points than control-points.
        /// </summary>
        /// <returns>A polyline connecting all control points.</returns>
        /// 

        /*
        public Polyline ControlPolygon()
        {
            int count = Count;
            int i_max = count;
            if (m_curve.IsPeriodic) { i_max -= (m_curve.Degree - 1); }

            Polyline rc = new Polyline(count);
            for (int i = 0; i < i_max; i++)
            {
                rc.Add(this[i].Location);
            }

            return rc;
        }
        */





        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(NurbsCurvePointList other, double epsilon)
        {
            if (null == other) throw new ArgumentNullException("other");

            if (ReferenceEquals(this, other))
                return true;

            if (Count != other.Count)
                return false;

            for (int i = 0; i < Count; ++i)
            {
                ControlPoint mine = this[i];
                ControlPoint theirs = other[i];
                if (!mine.EpsilonEquals(theirs, epsilon))
                    return false;
            }

            return true;
        }
    }
}

//public:
//  // NOTE: These members are left "public" so that expert users may efficiently
//  //       create NURBS curves using the default constructor and borrow the
//  //       knot and CV arrays from their native NURBS representation.
//  //       No technical support will be provided for users who access these
//  //       members directly.  If you can't get your stuff to work, then use
//  //       the constructor with the arguments and the SetKnot() and SetCV()
//  //       functions to fill in the arrays.

//  int     m_dim;            // (>=1)

//  int     m_is_rat;         // 1 for rational B-splines. (Rational control
//                            // vertices use homogeneous form.)
//                            // 0 for non-rational B-splines. (Control
//                            // verticies do not have a weight coordinate.)

//  int     m_order;          // order = degree+1 (>=2)

//  int     m_cv_count;       // number of control vertices ( >= order )

//  // knot vector memory

//  int     m_knot_capacity;  // If m_knot_capacity > 0, then m_knot[]
//                            // is an array of at least m_knot_capacity
//                            // doubles whose memory is managed by the
//                            // ON_NurbsCurve class using rhmalloc(),
//                            // onrealloc(), and rhfree().
//                            // If m_knot_capacity is 0 and m_knot is
//                            // not NULL, then  m_knot[] is assumed to
//                            // be big enough for any requested operation
//                            // and m_knot[] is not deleted by the
//                            // destructor.

//  double* m_knot;           // Knot vector. ( The knot vector has length
//                            // m_order+m_cv_count-2. )

//  // control vertex net memory

//  int     m_cv_stride;      // The pointer to start of "CV[i]" is
//                            //   m_cv + i*m_cv_stride.

//  int     m_cv_capacity;    // If m_cv_capacity > 0, then m_cv[] is an array
//                            // of at least m_cv_capacity doubles whose
//                            // memory is managed by the ON_NurbsCurve
//                            // class using rhmalloc(), onrealloc(), and rhfree().
//                            // If m_cv_capacity is 0 and m_cv is not
//                            // NULL, then m_cv[] is assumed to be big enough
//                            // for any requested operation and m_cv[] is not
//                            // deleted by the destructor.

//  double* m_cv;             // Control points.
//                            // If m_is_rat is FALSE, then control point is
//                            //
//                            //          ( CV(i)[0], ..., CV(i)[m_dim-1] ).
//                            //
//                            // If m_is_rat is TRUE, then the control point
//                            // is stored in HOMOGENEOUS form and is
//                            //
//                            //           [ CV(i)[0], ..., CV(i)[m_dim] ].
//                            //