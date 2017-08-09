using System;
using System.Runtime.InteropServices;

namespace NN.Geometry
{
    /// <summary>
    /// Represents the value of a plane, two angles and a radius in
    /// a subcurve of a three-dimensional circle.
    /// 
    /// <para>The curve is parameterized by an angle expressed in radians. For an IsValid arc
    /// the total subtended angle AngleRadians() = Domain()(1) - Domain()(0) must satisfy
    /// 0 &lt; AngleRadians() &lt; 2*Pi</para>
    /// 
    /// <para>The parameterization of the Arc is inherited from the Circle it is derived from.
    /// In particular</para>
    /// <para>t -> center + cos(t)*radius*xaxis + sin(t)*radius*yaxis</para>
    /// <para>where xaxis and yaxis, (part of Circle.Plane) form an othonormal frame of the plane
    /// containing the circle.</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 152)]
    [Serializable]
    public struct Arc : IEquatable<Arc>, IEpsilonComparable<Arc>
    {

        #region members
        internal Plane m_plane;
        internal double m_radius;
        internal Interval m_angle;
        #endregion


#if RHINO3DMIO
        public Arc(Rhino.Geometry.Arc f)
        {
            this.m_plane = new Plane(f.Plane);
            this.m_radius = f.Radius;
            this.m_angle = new Interval(f.AngleDomain);
            BoundingBox = new BoundingBox(f.BoundingBox());
            IsValid = f.IsValid;
        }

        public bool CopyFrom(Rhino.Geometry.Arc from)
        {
            if (from == null)
                return false;

            this.m_plane = new Plane(from.Plane);
            this.m_radius = from.Radius;
            this.m_angle = new Interval(from.AngleDomain);
            BoundingBox = new BoundingBox(from.BoundingBox());

            return true;
        }

        public bool CopyTo(Rhino.Geometry.Arc to)
        {
            to.Plane = m_plane.RhinoObject();
            to.Radius = this.m_radius;
            to.AngleDomain = new Rhino.Geometry.Interval(m_angle.T0, m_angle.T1);
            return true;
        }

        public Rhino.Geometry.Arc RhinoObject()
        {
            Rhino.Geometry.Circle cir = new Rhino.Geometry.Circle(m_plane.RhinoObject(), m_radius);
            return new Rhino.Geometry.Arc(cir, m_angle.RhinoObject());
        }
#endif

        public Arc(Plane p, double r, double angle)
        {
            IsValid = true;
            m_plane = p;
            m_radius = r;
            m_angle = new Interval(0, angle);
            BoundingBox = new BoundingBox();
        }

        /// <summary>
        /// Initializes a new instance of an invalid arc.
        /// </summary>
        static public Arc Invalid
        {
            get { return new Arc(Plane.WorldXY, 0.0, 0.0); }
        }

        /// <summary>
        /// Gets an Arc with Unset components.
        /// </summary>
        static public Arc Unset
        {
            get
            {
                return new Arc(Plane.Unset, RhinoMath.UnsetValue, 0.0);
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this arc is valid.
        /// Detail:
        ///	 Radius&gt;0 and 0&lt;AngleRadians()&lt;=2*Math.Pi.
        /// </summary>
        /// <returns>true if the arc is valid.</returns>
        public bool IsValid { get; }


        /// <summary>
        /// Gets a value indicating whether or not this arc is a complete circle.
        /// </summary>
        public bool IsCircle
        {
            get
            {
                return (Math.Abs(Math.Abs(Angle) - (2.0 * Math.PI)) <= RhinoMath.ZeroTolerance);
            }
        }

        /// <summary>
        /// Gets or sets the plane in which this arc lies.
        /// </summary>
        public Plane Plane
        {
            get { return m_plane; }
            set { m_plane = value; }
        }

        /// <summary>
        /// Gets or sets the radius of this arc.
        /// </summary>
        public double Radius
        {
            get { return m_radius; }
            set { m_radius = value; }
        }

        /// <summary>
        /// Gets or sets the Diameter of this arc.
        /// </summary>
        public double Diameter
        {
            get { return m_radius * 2.0; }
            set { m_radius = 0.5 * value; }
        }

        /// <summary>
        /// Gets or sets the center point for this arc.
        /// </summary>
        public Point3d Center
        {
            get { return m_plane.Origin; }
            set { m_plane.Origin = value; }
        }

        /// <summary>
        /// Gets the circumference of the circle that is coincident with this arc.
        /// </summary>
        public double Circumference
        {
            get { return Math.Abs(2.0 * Math.PI * m_radius); }
        }

        /// <summary>
        /// Gets the length of the arc. (Length = Radius * (subtended angle in radians)).
        /// </summary>
        public double Length
        {
            get { return Math.Abs(Angle * m_radius); }
        }

        /// <summary>
        /// Gets or sets the angle domain (in Radians) of this arc.
        /// </summary>
        public Interval AngleDomain
        {
            get { return m_angle; }
            set { m_angle = value; }
        }

        /// <summary>
        /// Gets or sets the start angle (in Radians) for this arc segment.
        /// </summary>
        public double StartAngle
        {
            get { return m_angle.T0; }
            set { m_angle.T0 = value; }
        }

        /// <summary>
        /// Gets or sets the end angle (in Radians) for this arc segment.
        /// </summary>
        public double EndAngle
        {
            get { return m_angle.T1; }
            set { m_angle.T1 = value; }
        }

        /// <summary>
        /// Gets or sets the sweep -or subtended- angle (in Radians) for this arc segment.
        /// </summary>
        public double Angle
        {
            get { return m_angle.Length; }
            set { m_angle.T1 = m_angle.T0 + value; }
        }

        /// <summary>
        /// Gets or sets the start angle (in Radians) for this arc segment.
        /// </summary>
        public double StartAngleDegrees
        {
            get { return RhinoMath.ToDegrees(StartAngle); }
            set { StartAngle = RhinoMath.ToRadians(value); }
        }

        /// <summary>
        /// Gets or sets the end angle (in Radians) for this arc segment.
        /// </summary>
        public double EndAngleDegrees
        {
            get { return RhinoMath.ToDegrees(EndAngle); }
            set { EndAngle = RhinoMath.ToRadians(value); }
        }

        /// <summary>
        /// Gets or sets the sweep -or subtended- angle (in Radians) for this arc segment.
        /// </summary>
        public double AngleDegrees
        {
            get { return RhinoMath.ToDegrees(Angle); }
            set { Angle = RhinoMath.ToRadians(value); }
        }


        /// <summary>
        /// Determines whether another object is an arc and has the same value as this arc.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <returns>true if obj is an arc and is exactly equal to this arc; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return obj is Arc && Equals((Arc)obj);
        }

        /// <summary>
        /// Determines whether another arc has the same value as this arc.
        /// </summary>
        /// <param name="other">An arc.</param>
        /// <returns>true if obj is equal to this arc; otherwise false.</returns>
        public bool Equals(Arc other)
        {
            return Math.Abs(Radius - other.Radius) < RhinoMath.ZeroTolerance && m_angle == other.m_angle && m_plane == other.m_plane;
        }

        /// <summary>
        /// Computes a hash code for the present arc.
        /// </summary>
        /// <returns>A non-unique integer that represents this arc.</returns>
        public override int GetHashCode()
        {
            return Radius.GetHashCode() ^ m_angle.GetHashCode() ^ m_plane.GetHashCode();
        }

        /// <summary>
        /// Determines whether two arcs have equal values.
        /// </summary>
        /// <param name="a">The first arc.</param>
        /// <param name="b">The second arc.</param>
        /// <returns>true if all values of the two arcs are exactly equal; otherwise false.</returns>
        public static bool operator ==(Arc a, Arc b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether two arcs have different values.
        /// </summary>
        /// <param name="a">The first arc.</param>
        /// <param name="b">The second arc.</param>
        /// <returns>true if any value of the two arcs differ; otherwise false.</returns>
        public static bool operator !=(Arc a, Arc b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Computes the 3D axis aligned bounding box for this arc.
        /// </summary>
        /// <returns>Bounding box of arc.</returns>
        [System.Xml.Serialization.XmlIgnore]
        public BoundingBox BoundingBox { get; private set; }
        
        /// <summary>
        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(Arc other, double epsilon)
        {
            return RhinoMath.EpsilonEquals(m_radius, other.Radius, epsilon) &&
                   m_plane.EpsilonEquals(other.m_plane, epsilon) &&
                   m_angle.EpsilonEquals(other.m_angle, epsilon);
        }
        
    }
}