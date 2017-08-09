using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

//public class ON_TensorProduct { } never seen this used
//  public class ON_CageMorph { }

namespace NN.Geometry
{
    /// <summary>
    /// Represents a Non Uniform Rational B-Splines (NURBS) surface.
    /// </summary>
    [Serializable]
    public class NurbsSurface : Surface, IEpsilonComparable<NurbsSurface>
    {
#if RHINO3DMIO || RHINOCOMMON
        public NurbsSurface(Rhino.Geometry.NurbsSurface f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.NurbsSurface from)
        {

            if (from == null)
                return false;

            this.KnotsU = new NN.Geometry.Collections.NurbsSurfaceKnotList(from.KnotsU);
            this.KnotsV = new NN.Geometry.Collections.NurbsSurfaceKnotList(from.KnotsV);
            this.Points = new NN.Geometry.Collections.NurbsSurfacePointList(from.Points);

            this.IsRational = from.IsRational;
            this.OrderU = from.OrderU;
            this.OrderV = from.OrderV;
            this.IsSolid = from.IsSolid;

            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;

            this.ComponentIndex = new ComponentIndex(from.ComponentIndex());

            this.Dimension = 3;

            return true;
        }


        public bool CopyTo(Rhino.Geometry.NurbsSurface to)
        {
            return true;
        }

        public Rhino.Geometry.NurbsSurface RhinoObject()
        {

            var surface = Rhino.Geometry.NurbsSurface.Create(Dimension, IsRational, OrderU, OrderV, Points.CountU, Points.CountV);

            if (surface == null)
                return null;

            for (int i = 0; i < KnotsU.Count && i < surface.KnotsU.Count; i++)
                surface.KnotsU[i] = KnotsU[i];

            for (int i = 0; i < KnotsV.Count && i < surface.KnotsV.Count; i++)
                surface.KnotsV[i] = KnotsV[i];

            for (int i = 0; i < Points.Points.Count; i++) {
                
                int u = i / Points.CountV;
                int v = i % Points.CountV;

                surface.Points.SetControlPoint(u, v, new Rhino.Geometry.ControlPoint(Points.Points[i].Location.RhinoObject(), Points.Points[i].Weight));
            }

            return surface;
        }
        
        public override Rhino.Geometry.Surface RhinoSurfaceObject()
        {
            return RhinoObject();
        }
#endif


        /// <summary>
        /// Initializes a new NURBS surface by copying the values from another surface.
        /// </summary>
        /// <param name="other">Another surface.</param>
        public NurbsSurface() { }


        #region properties
        private Collections.NurbsSurfaceKnotList m_knots_u;
        private Collections.NurbsSurfaceKnotList m_knots_v;
        /// <summary>
        /// The U direction knot vector.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_createsurfaceexample.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_createsurfaceexample.cs' lang='cs'/>
        /// <code source='examples\py\ex_createsurfaceexample.py' lang='py'/>
        /// </example>
        public Collections.NurbsSurfaceKnotList KnotsU
        {
            get { return m_knots_u ?? (m_knots_u = new Collections.NurbsSurfaceKnotList()); }
            set { m_knots_u = value; }
        }

        /// <summary>
        /// The V direction knot vector.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_createsurfaceexample.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_createsurfaceexample.cs' lang='cs'/>
        /// <code source='examples\py\ex_createsurfaceexample.py' lang='py'/>
        /// </example>
        public Collections.NurbsSurfaceKnotList KnotsV
        {
            get { return m_knots_v ?? (m_knots_v = new Collections.NurbsSurfaceKnotList()); }
            set { m_knots_v = value; }
        }

        private Collections.NurbsSurfacePointList m_points;

        /// <summary>
        /// Gets a collection of surface control points that form this surface.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_createsurfaceexample.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_createsurfaceexample.cs' lang='cs'/>
        /// <code source='examples\py\ex_createsurfaceexample.py' lang='py'/>
        /// </example>
        public Collections.NurbsSurfacePointList Points
        {
            get { return m_points ?? (m_points = new Collections.NurbsSurfacePointList()); }
            set { m_points = value; }
        }

        /// <summary>
        /// Gets a value indicating whether or not the nurbs surface is rational.
        /// </summary>
        public bool IsRational { get; set; }

        public int Dimension { get; set; }


        /// <summary>
        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(NurbsSurface other, double epsilon)
        {
            if (null == other) throw new ArgumentNullException("other");

            if (ReferenceEquals(this, other))
                return true;

            // TODO
            //if ((Degree(0) != other.Degree(0)) || (Degree(1) != other.Degree(1)))
            // return false;

            if (IsRational != other.IsRational)
                return false;

            if (Points.CountU != other.Points.CountU || Points.CountV != other.Points.CountV)
                return false;

            if (!KnotsU.EpsilonEquals(other.KnotsU, epsilon))
                return false;

            if (!KnotsV.EpsilonEquals(other.KnotsV, epsilon))
                return false;

            return true;
        }

        /// <summary>
        /// Gets the order in the U direction.
        /// </summary>
        public int OrderU { get; set; }


        /// <summary>
        /// Gets the order in the V direction.
        /// </summary>
        public int OrderV { get; set; }

    }
    //  public class ON_NurbsCage : ON_Geometry { }
    //  public class ON_MorphControl : ON_Geometry { }

    /// <summary>
    /// Represents a geometry that is able to control the morphing behaviour of some other geometry.
    /// </summary>
    [Serializable]
    public class MorphControl : GeometryBase
    {
        public MorphControl()
        {
        }


        /// <summary>
        /// The 3d fitting tolerance used when morphing surfaces and breps.
        /// The default is 0.0 and any value &lt;= 0.0 is ignored by morphing functions.
        /// The value returned by Tolerance does not affect the way meshes and points are morphed.
        /// </summary>
        public double SpaceMorphTolerance { get; set; }


        /// <summary>
        /// true if the morph should be done as quickly as possible because the
        /// result is being used for some type of dynamic preview.  If QuickPreview
        /// is true, the tolerance may be ignored. The QuickPreview value does not
        /// affect the way meshes and points are morphed. The default is false.
        /// </summary>
        public bool QuickPreview { get; set; }


        /// <summary>
        /// true if the morph should be done in a way that preserves the structure
        /// of the geometry.  In particular, for NURBS objects, true  eans that
        /// only the control points are moved.  The PreserveStructure value does not
        /// affect the way meshes and points are morphed. The default is false.
        /// </summary>
        public bool PreserveStructure { get; set; }
    }
}


namespace NN.Geometry.Collections
{
    /// <summary>
    /// Provides access to the control points of a nurbs surface.
    /// </summary>
    public sealed class NurbsSurfacePointList : IEpsilonComparable<NurbsSurfacePointList>
    {

#if RHINO3DMIO || RHINOCOMMON
        public NurbsSurfacePointList(Rhino.Geometry.Collections.NurbsSurfacePointList f)
        {

            Points = new List<ControlPoint>();

            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Collections.NurbsSurfacePointList from)
        {
            Points.Clear();

            foreach (var p in from)
            {
                var l = p.Location;
                Points.Add(new ControlPoint(l.X, l.Y, l.Z, p.Weight));

            }

            CountU = from.CountU;
            CountV = from.CountV;

            return true;
        }

#endif


        public NurbsSurfacePointList()
        {
            Points = new List<ControlPoint>();
        }

        /// <summary>
        /// Gets the number of control points in the U direction of this surface.
        /// </summary>
        public int CountU { get; set; }

        /// <summary>
        /// Gets the number of control points in the V direction of this surface.
        /// </summary>
        public int CountV { get; set; }


        public System.Collections.Generic.List<ControlPoint> Points {get; set;}

        private class NurbsSrfEnum : IEnumerator<ControlPoint>
        {
            #region members
            readonly NurbsSurfacePointList m_surface_cv;
            readonly int m_count_u = -1;
            readonly int m_count_v = -1;
            bool m_disposed; // = false; <- initialized by runtime
            int m_position = -1;
            #endregion

            #region constructor
            public NurbsSrfEnum(NurbsSurfacePointList surfacePoints)
            {
                m_surface_cv = surfacePoints;
                m_count_u = surfacePoints.CountU;
                m_count_v = surfacePoints.CountV;
            }
            #endregion

            #region enumeration logic
            int Count
            {
                get { return m_count_u * m_count_v; }
            }

            public bool MoveNext()
            {
                m_position++;
                return (m_position < Count);
            }
            public void Reset()
            {
                m_position = -1;
            }

            public ControlPoint Current
            {
                get
                {
                    try
                    {
                        int u = m_position / m_count_v;
                        int v = m_position % m_count_v;
                        // TODO
                        //return m_surface_cv.GetControlPoint(u, v);

                        return new ControlPoint();
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    try
                    {
                        int u = m_position / m_count_v;
                        int v = m_position % m_count_v;
                        // TODO
                        //return m_surface_cv.GetControlPoint(u, v);
                        return null;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
            #endregion

            #region IDisposable logic
            public void Dispose()
            {
                if (m_disposed)
                    return;
                m_disposed = true;
                GC.SuppressFinalize(this);
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(NurbsSurfacePointList other, double epsilon)
        {
            if (null == other) throw new ArgumentNullException("other");

            if (ReferenceEquals(this, other))
                return true;

            if (CountU != other.CountU)
                return false;

            if (CountV != other.CountV)
                return false;


            for (int u = 0; u < CountU; ++u)
            {
                for (int v = 0; v < CountV; ++v)
                {
                    //ControlPoint mine = GetControlPoint(u, v);
                    //ControlPoint theirs = other.GetControlPoint(u, v);

                    // TODO
                    //if (!mine.EpsilonEquals(theirs, epsilon))
                    //    return false;
                }
            }

            return true;
        }
    }
    /// <summary>
    /// Provides access to the knot vector of a nurbs surface.
    /// </summary>
    public sealed class NurbsSurfaceKnotList : System.Collections.Generic.List<double>, NN.Collections.IRhinoTable<double>, IEpsilonComparable<NurbsSurfaceKnotList>
    {
        public NurbsSurfaceKnotList()
        {

        }

#if RHINO3DMIO || RHINOCOMMON
        public NurbsSurfaceKnotList(Rhino.Geometry.Collections.NurbsSurfaceKnotList f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Collections.NurbsSurfaceKnotList from)
        {

            if (from == null)
                return false;

            this.Clear();
            this.AddRange(from);

            this.ClampedAtStart = from.ClampedAtStart;
            this.ClampedAtEnd = from.ClampedAtEnd;

            return true;
        }

        public bool CopyTo(Rhino.Geometry.Collections.NurbsSurfaceKnotList to)
        {

            for (int i = 0; i < this.Count; i++)
                to.InsertKnot(this[i]);

            return true;
        }

#endif

        /// <summary>Determines if a knot vector is clamped.</summary>
        public bool ClampedAtStart { get; set; }

        /// <summary>Determines if a knot vector is clamped.</summary>
        public bool ClampedAtEnd { get; set; }


        /// <summary>
        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(NurbsSurfaceKnotList other, double epsilon)
        {
            if (null == other) throw new ArgumentNullException("other");

            if (ReferenceEquals(this, other))
                return true;

            // TODO ?? 
            //if (m_direction != other.m_direction)
            //   return false;

            if (Count != other.Count)
                return false;

            // check for equality of spans
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
}