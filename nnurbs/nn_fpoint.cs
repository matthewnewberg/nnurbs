using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NN.Geometry
{
    /// <summary>
    /// Represents the two coordinates of a point in two-dimensional space,
    /// using <see cref="Single"/>-precision floating point numbers.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 8)]
    [DebuggerDisplay("({m_x}, {m_y})")]
    public struct Point2f : IEquatable<Point2f>, IComparable<Point2f>, IComparable, IEpsilonFComparable<Point2f>
    {
        #region members
        internal float m_x;
        internal float m_y;
        #endregion

        #region constructors


#if RHINO3DMIO
        public Point2f(Rhino.Geometry.Point2f p)
        {
            m_x = p.X;
            m_y = p.Y;
        }

        public Rhino.Geometry.Point2f RhinoObject()
        {
            return new Rhino.Geometry.Point2f(m_x, m_y);
        }
#endif

        /// <summary>
        /// Initializes a new two-dimensional point from two components.
        /// </summary>
        /// <param name="x">X component of vector.</param>
        /// <param name="y">Y component of vector.</param>
        public Point2f(float x, float y)
        {
            m_x = x;
            m_y = y;
        }

        /// <summary>
        /// Initializes a new two-dimensional point from two double-precision floating point numbers as coordinates.
        /// <para>Coordinates will be internally converted to floating point numbers. This might cause precision loss.</para>
        /// </summary>
        /// <param name="x">X component of vector.</param>
        /// <param name="y">Y component of vector.</param>
        public Point2f(double x, double y)
        {
            m_x = (float)x;
            m_y = (float)y;
        }

        /// <summary>
        /// Gets the standard unset point.
        /// </summary>
        public static Point2f Unset
        {
            get
            {
                return new Point2f(RhinoMath.UnsetSingle, RhinoMath.UnsetSingle);
            }
        }
        #endregion

        #region properties
        /// <summary>
        /// Gets a value indicating whether this point is considered valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return RhinoMath.IsValidSingle(m_x) &&
                       RhinoMath.IsValidSingle(m_y);
            }
        }

        /// <summary>
        /// Gets or sets the X (first) component of the vector.
        /// </summary>
        public float X { get { return m_x; } set { m_x = value; } }

        /// <summary>
        /// Gets or sets the Y (second) component of the vector.
        /// </summary>
        public float Y { get { return m_y; } set { m_y = value; } }
        #endregion

        /// <summary>
        /// Determines whether the specified System.Object is a <see cref="Point2f"/> and has the same values as the present point.
        /// </summary>
        /// <param name="obj">The specified object.</param>
        /// <returns>true if obj is Point2f and has the same coordinates as this; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Point2f && this == (Point2f)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Point2f"/> has the same values as the present point.
        /// </summary>
        /// <param name="point">The specified point.</param>
        /// <returns>true if point has the same coordinates as this; otherwise false.</returns>
        public bool Equals(Point2f point)
        {
            return this == point;
        }

        /// <summary>
        /// Check that all values in other are within epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(Point2f other, float epsilon)
        {
            return RhinoMath.EpsilonEquals(m_x, other.m_x, epsilon) &&
                   RhinoMath.EpsilonEquals(m_y, other.m_y, epsilon);
        }

        /// <summary>
        /// Compares this <see cref="Point2f" /> with another <see cref="Point2f" />.
        /// <para>Coordinates evaluation priority is first X, then Y.</para>
        /// </summary>
        /// <param name="other">The other <see cref="Point2f" /> to use in comparison.</param>
        /// <returns>
        /// <para> 0: if this is identical to other</para>
        /// <para>-1: if this.X &lt; other.X</para>
        /// <para>-1: if this.X == other.X and this.Y &lt; other.Y</para>
        /// <para>+1: otherwise.</para>
        /// </returns>
        public int CompareTo(Point2f other)
        {
            // dictionary order
            if (m_x < other.m_x)
                return -1;
            if (m_x > other.m_x)
                return 1;

            if (m_y < other.m_y)
                return -1;
            if (m_y > other.m_y)
                return 1;

            return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is Point2f)
                return CompareTo((Point2f)obj);

            throw new ArgumentException("Input must be of type Point2f", "obj");
        }

        /// <summary>
        /// Computes a hash number that represents the current point.
        /// </summary>
        /// <returns>A hash code that is not unique for each point.</returns>
        public override int GetHashCode()
        {
            // MSDN docs recommend XOR'ing the internal values to get a hash code
            return m_x.GetHashCode() ^ m_y.GetHashCode();
        }

        /// <summary>
        /// Constructs the string representation for the current point.
        /// </summary>
        /// <returns>The point representation in the form X,Y.</returns>
        public override string ToString()
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            return String.Format("{0},{1}", m_x.ToString(culture), m_y.ToString(culture));
        }

        /// <summary>
        /// Determines whether two <see cref="Point2f"/> have equal values.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if the coordinates of the two points are exactly equal; otherwise false.</returns>
        public static bool operator ==(Point2f a, Point2f b)
        {
            return (a.m_x == b.m_x && a.m_y == b.m_y);
        }

        /// <summary>
        /// Determines whether two <see cref="Point2f"/> have different values.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if the two points differ in any coordinate; false otherwise.</returns>
        public static bool operator !=(Point2f a, Point2f b)
        {
            return (a.m_x != b.m_x || a.m_y != b.m_y);
        }

        /// <summary>
        /// Determines whether the first specified <see cref="Point2f"/> comes before
        /// (has inferior sorting value than) the second point.
        /// <para>Coordinates evaluation priority is first X, then Y.</para>
        /// </summary>
        /// <param name="a">First point.</param>
        /// <param name="b">Second point.</param>
        /// <returns>true if a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y; otherwise, false.</returns>
        public static bool operator <(Point2f a, Point2f b)
        {
            return (a.X < b.X) || (a.X == b.X && a.Y < b.Y);
        }

        /// <summary>
        /// Determines whether the first specified <see cref="Point2f"/> comes before
        /// (has inferior sorting value than) the second point, or it is equal to it.
        /// <para>Coordinates evaluation priority is first X, then Y.</para>
        /// </summary>
        /// <param name="a">First point.</param>
        /// <param name="b">Second point.</param>
        /// <returns>true if a.X is smaller than b.X, or a.X == b.X and a.Y &lt;= b.Y; otherwise, false.</returns>
        public static bool operator <=(Point2f a, Point2f b)
        {
            return (a.X < b.X) || (a.X == b.X && a.Y <= b.Y);
        }

        /// <summary>
        /// Determines whether the first specified <see cref="Point2f"/> comes after
        /// (has superior sorting value than) the second point.
        /// <para>Coordinates evaluation priority is first X, then Y.</para>
        /// </summary>
        /// <param name="a">First point.</param>
        /// <param name="b">Second point.</param>
        /// <returns>true if a.X is larger than b.X, or a.X == b.X and a.Y is larger than b.Y; otherwise, false.</returns>
        public static bool operator >(Point2f a, Point2f b)
        {
            return (a.X > b.X) || (a.X == b.X && a.Y > b.Y);
        }

        /// <summary>
        /// Determines whether the first specified <see cref="Point2f"/> comes after
        /// (has superior sorting value than) the second point, or it is equal to it.
        /// <para>Coordinates evaluation priority is first X, then Y.</para>
        /// </summary>
        /// <param name="a">First point.</param>
        /// <param name="b">Second point.</param>
        /// <returns>true if a.X is larger than b.X, or a.X == b.X and a.Y &gt;= b.Y; otherwise, false.</returns>
        public static bool operator >=(Point2f a, Point2f b)
        {
            return (a.X > b.X) || (a.X == b.X && a.Y >= b.Y);
        }
    }

    /// <summary>
    /// Represents the three coordinates of a point in three-dimensional space,
    /// using <see cref="Single"/>-precision floating point numbers.
    /// </summary>
    // holding off on making this IComparable until I understand all
    // of the rules that FxCop states about IComparable classes
    [DebuggerDisplay("({m_x}, {m_y}, {m_z})")]
    [Serializable]
    public struct Point3f : IEquatable<Point3f>, IComparable<Point3f>, IComparable, IEpsilonFComparable<Point3f>
    {
        internal System.Numerics.Vector3 p;

#if RHINO3DMIO
        public Point3f(Rhino.Geometry.Point3f rhinoVert)
        {
            p.X = rhinoVert.X;
            p.Y = rhinoVert.Y;
            p.Z = rhinoVert.Z;
        }

        public Rhino.Geometry.Point3f RhinoObject()
        {
            return new Rhino.Geometry.Point3f(p.X, p.Y, p.Z);
        }
#endif

        /// <summary>
        /// Initializes a new two-dimensional vector from two components.
        /// </summary>
        /// <param name="x">X component of vector.</param>
        /// <param name="y">Y component of vector.</param>
        /// <param name="z">Z component of vector.</param>
        public Point3f(float x, float y, float z)
        {
            p.X = x;
            p.Y = y;
            p.Z = z;
        }

        /// <summary>
        /// Gets the value of a point at location 0,0,0.
        /// </summary>
        public static Point3f Origin
        {
            get { return new Point3f(0f, 0f, 0f); }
        }

        /// <summary>
        /// Gets the value of a point at location RhinoMath.UnsetValue,RhinoMath.UnsetValue,RhinoMath.UnsetValue.
        /// </summary>
        public static Point3f Unset
        {
            get { return new Point3f(RhinoMath.UnsetSingle, RhinoMath.UnsetSingle, RhinoMath.UnsetSingle); }
        }

        /// <summary>
        /// Gets or sets the X (first) component of the vector.
        /// </summary>
        public float X { get { return p.X; } set { p.X = value; } }

        /// <summary>
        /// Gets or sets the Y (second) component of the vector.
        /// </summary>
        public float Y { get { return p.Y; } set { p.Y = value; } }

        /// <summary>
        /// Gets or sets the Z (third) component of the vector.
        /// </summary>
        public float Z { get { return p.Z; } set { p.Z = value; } }

        /// <summary>
        /// Determines whether the specified System.Object is a Point3f and has the same values as the present point.
        /// </summary>
        /// <param name="obj">The specified object.</param>
        /// <returns>true if obj is Point3f and has the same coordinates as this; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Point3f && this == (Point3f)obj);
        }

        /// <summary>
        /// Determines whether the specified Point3f has the same values as the present point.
        /// </summary>
        /// <param name="point">The specified point.</param>
        /// <returns>true if point has the same coordinates as this; otherwise false.</returns>
        public bool Equals(Point3f point)
        {
            return this == point;
        }

        /// <summary>
        /// Check that all values in other are withing epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(Point3f other, float epsilon)
        {
            return RhinoMath.EpsilonEquals(p.X, other.p.X, epsilon) &&
                   RhinoMath.EpsilonEquals(p.Y, other.p.Y, epsilon) &&
                   RhinoMath.EpsilonEquals(p.Z, other.p.Z, epsilon);
        }

        /// <summary>
        /// Compares this <see cref="Point3f" /> with another <see cref="Point3f" />.
        /// <para>Component evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="other">The other <see cref="Point3d" /> to use in comparison.</param>
        /// <returns>
        /// <para> 0: if this is identical to other</para>
        /// <para>-1: if this.X &lt; other.X</para>
        /// <para>-1: if this.X == other.X and this.Y &lt; other.Y</para>
        /// <para>-1: if this.X == other.X and this.Y == other.Y and this.Z &lt; other.Z</para>
        /// <para>+1: otherwise.</para>
        /// </returns>
        public int CompareTo(Point3f other)
        {
            if (p.X < other.p.X)
                return -1;
            if (p.X > other.p.X)
                return 1;

            if (p.Y < other.p.Y)
                return -1;
            if (p.Y > other.p.Y)
                return 1;

            if (p.Z < other.p.Z)
                return -1;
            if (p.Z > other.p.Z)
                return 1;

            return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is Point3f)
                return CompareTo((Point3f)obj);

            throw new ArgumentException("Input must be of type Point3f", "obj");
        }

        /// <summary>
        /// Computes a hash code for the present point.
        /// </summary>
        /// <returns>A non-unique integer that represents this point.</returns>
        public override int GetHashCode()
        {
            // MSDN docs recommend XOR'ing the internal values to get a hash code
            return p.X.GetHashCode() ^ p.Y.GetHashCode() ^ p.Z.GetHashCode();
        }

        /// <summary>
        /// Constructs the string representation for the current point.
        /// </summary>
        /// <returns>The point representation in the form X,Y,Z.</returns>
        public override string ToString()
        {
            return String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0},{1},{2}", p.X, p.Y, p.Z);
        }

        /// <summary>
        /// Each coordinate of the point must pass the <see cref="RhinoMath.IsValidSingle"/> test.
        /// </summary>
        public bool IsValid
        {
            get { return RhinoMath.IsValidSingle(p.X) && RhinoMath.IsValidSingle(p.Y) && RhinoMath.IsValidSingle(p.Z); }
        }

        /// <summary>
        /// Computes the distance between two points.
        /// </summary>
        /// <param name="other">Other point for distance measurement.</param>
        /// <returns>The length of the line between this and the other point; or 0 if any of the points is not valid.</returns>
        /// <example>
        /// <code source='examples\vbnet\ex_intersectcurves.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_intersectcurves.cs' lang='cs'/>
        /// <code source='examples\py\ex_intersectcurves.py' lang='py'/>
        /// </example>
        public double DistanceTo(Point3f other)
        {
            double d = 0.0;
            if (IsValid && other.IsValid)
            {
                double dx = other.p.X - p.X;
                double dy = other.p.Y - p.Y;
                double dz = other.p.Z - p.Z;
                d = Vector3d.GetLengthHelper(dx, dy, dz);
            }
            return d;
        }

        public float DistanceToFast(Point3f other)
        {
            System.Numerics.Vector3 l = other.p - p;

            return l.Length();
        }

        /// <summary>
        /// Transforms the present point in place. The transformation matrix acts on the left of the point. i.e.,
        /// <para>result = transformation*point</para>
        /// </summary>
        /// <param name="xform">Transformation to apply.</param>
        public void Transform(Transform xform)
        {
            //David: this method doesn't test for validity. Should it?
            double ww = xform.m_30 * p.X + xform.m_31 * p.Y + xform.m_32 * p.Z + xform.m_33;
            if (ww != 0.0) { ww = 1.0 / ww; }

            double tx = ww * (xform.m_00 * p.X + xform.m_01 * p.Y + xform.m_02 * p.Z + xform.m_03);
            double ty = ww * (xform.m_10 * p.X + xform.m_11 * p.Y + xform.m_12 * p.Z + xform.m_13);
            double tz = ww * (xform.m_20 * p.X + xform.m_21 * p.Y + xform.m_22 * p.Z + xform.m_23);
            p.X = (float)tx;
            p.Y = (float)ty;
            p.Z = (float)tz;
        }

        /// <summary>
        /// Determines whether two points have equal values.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if the coordinates of the two points are exactly equal; otherwise false.</returns>
        public static bool operator ==(Point3f a, Point3f b)
        {
            return (a.p.X == b.p.X && a.p.Y == b.p.Y && a.p.Z == b.p.Z);
        }

        /// <summary>
        /// Determines whether two points have different values.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if the two points differ in any coordinate; false otherwise.</returns>
        public static bool operator !=(Point3f a, Point3f b)
        {
            return (a.p.X != b.p.X || a.p.Y != b.p.Y || a.p.Z != b.p.Z);
        }

        /// <summary>
        /// Determines whether the first specified point comes before (has inferior sorting value than) the second point.
        /// <para>Coordinates evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if a.X is smaller than b.X,
        /// or a.X == b.X and a.Y is smaller than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z is smaller than b.Z;
        /// otherwise, false.</returns>
        public static bool operator <(Point3f a, Point3f b)
        {
            return a.CompareTo(b) < 0;
        }

        /// <summary>
        /// Determines whether the first specified point comes before
        /// (has inferior sorting value than) the second point, or it is equal to it.
        /// <para>Coordinates evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if a.X is smaller than b.X,
        /// or a.X == b.X and a.Y is smaller than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z &lt;= b.Z;
        /// otherwise, false.</returns>
        public static bool operator <=(Point3f a, Point3f b)
        {
            return a.CompareTo(b) <= 0;
        }

        /// <summary>
        /// Determines whether the first specified point comes after (has superior sorting value than) the second point.
        /// <para>Coordinates evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if a.X is larger than b.X,
        /// or a.X == b.X and a.Y is larger than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z is larger than b.Z;
        /// otherwise, false.</returns>
        public static bool operator >(Point3f a, Point3f b)
        {
            return a.CompareTo(b) > 0;
        }

        /// <summary>
        /// Determines whether the first specified point comes after
        /// (has superior sorting value than) the second point, or it is equal to it.
        /// <para>Coordinates evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if a.X is larger than b.X,
        /// or a.X == b.X and a.Y is larger than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z &gt;= b.Z;
        /// otherwise, false.</returns>
        public static bool operator >=(Point3f a, Point3f b)
        {
            return a.CompareTo(b) >= 0;
        }

        /// <summary>
        /// Subtracts a point from another point.
        /// </summary>
        /// <param name="point1">A point.</param>
        /// <param name="point2">Another point.</param>
        /// <returns>A new vector that is the difference of point minus vector.</returns>
        public static Vector3f operator -(Point3f point1, Point3f point2)
        {
            return new Vector3f(point1.p.X - point2.p.X, point1.p.Y - point2.p.Y, point1.p.Z - point2.p.Z);
        }

        /// <summary>
        /// Subtracts a point from another point.
        /// <para>(Provided for languages that do not support operator overloading. You can use the - operator otherwise)</para>
        /// </summary>
        /// <param name="point1">A point.</param>
        /// <param name="point2">Another point.</param>
        /// <returns>A new vector that is the difference of point minus vector.</returns>
        public static Vector3f Subtract(Point3f point1, Point3f point2)
        {
            return new Vector3f(point1.p.X - point2.p.X, point1.p.Y - point2.p.Y, point1.p.Z - point2.p.Z);
        }


    }

    //skipping ON_4fPoint. I don't think I've ever seen this used in NN.

    /// <summary>
    /// Represents the two components of a vector in two-dimensional space,
    /// using <see cref="Single"/>-precision floating point numbers.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 8)]
    [DebuggerDisplay("({m_x}, {m_y})")]
    [Serializable]
    public struct Vector2f : IEquatable<Vector2f>, IComparable<Vector2f>, IComparable, IEpsilonFComparable<Vector2f>
    {
        internal float m_x;
        internal float m_y;

        /// <summary>
        /// Gets or sets the X (first) component of this vector.
        /// </summary>
        public float X { get { return m_x; } set { m_x = value; } }

        /// <summary>
        /// Gets or sets the Y (second) component of this vector.
        /// </summary>
        public float Y { get { return m_y; } set { m_y = value; } }

        /// <summary>
        /// Determines whether the specified System.Object is a Vector2f and has the same values as the present vector.
        /// </summary>
        /// <param name="obj">The specified object.</param>
        /// <returns>true if obj is Vector2f and has the same coordinates as this; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector2f && this == (Vector2f)obj);
        }

        /// <summary>
        /// Determines whether the specified vector has the same values as the present vector.
        /// </summary>
        /// <param name="vector">The specified vector.</param>
        /// <returns>true if obj is Vector2f and has the same coordinates as this; otherwise false.</returns>
        public bool Equals(Vector2f vector)
        {
            return this == vector;
        }

        /// <summary>
        /// Check that all values in other are withing epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(Vector2f other, float epsilon)
        {
            return RhinoMath.EpsilonEquals(m_x, other.m_x, epsilon) &&
                   RhinoMath.EpsilonEquals(m_y, other.m_y, epsilon);
        }
        /// <summary>
        /// Compares this <see cref="Vector2f" /> with another <see cref="Vector2f" />.
        /// <para>Components evaluation priority is first X, then Y.</para>
        /// </summary>
        /// <param name="other">The other <see cref="Vector2f" /> to use in comparison.</param>
        /// <returns>
        /// <para> 0: if this is identical to other</para>
        /// <para>-1: if this.X &lt; other.X</para>
        /// <para>-1: if this.X == other.X and this.Y &lt; other.Y</para>
        /// <para>+1: otherwise.</para>
        /// </returns>
        public int CompareTo(Vector2f other)
        {
            if (m_x < other.m_x)
                return -1;
            if (m_x > other.m_x)
                return 1;

            if (m_y < other.m_y)
                return -1;
            if (m_y > other.m_y)
                return 1;

            return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is Vector2f)
                return CompareTo((Vector2f)obj);

            throw new ArgumentException("Input must be of type Vector2f", "obj");
        }

        /// <summary>
        /// Computes a hash number that represents the current vector.
        /// </summary>
        /// <returns>A hash code that is not unique for each vector.</returns>
        public override int GetHashCode()
        {
            // MSDN docs recommend XOR'ing the internal values to get a hash code
            return m_x.GetHashCode() ^ m_y.GetHashCode();
        }

        /// <summary>
        /// Constructs the string representation of the current vector.
        /// </summary>
        /// <returns>The vector representation in the form X,Y,Z.</returns>
        public override string ToString()
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            return String.Format("{0},{1}", m_x.ToString(culture), m_y.ToString(culture));
        }

        /// <summary>
        /// Determines whether two vectors have equal values.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if the components of the two vectors are exactly equal; otherwise false.</returns>
        public static bool operator ==(Vector2f a, Vector2f b)
        {
            return (a.m_x == b.m_x && a.m_y == b.m_y);
        }

        /// <summary>
        /// Determines whether two vectors have different values.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if the two vectors differ in any component; false otherwise.</returns>
        public static bool operator !=(Vector2f a, Vector2f b)
        {
            return (a.m_x != b.m_x || a.m_y != b.m_y);
        }

        /// <summary>
        /// Determines whether the first specified vector comes before
        /// (has inferior sorting value than) the second vector.
        /// <para>Components have decreasing evaluation priority: first X, then Y.</para>
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>true if a.X is smaller than b.X, or a.X == b.X and a.Y is smaller than b.Y; otherwise, false.</returns>
        public static bool operator <(Vector2f a, Vector2f b)
        {
            return (a.X < b.X) || (a.X == b.X && a.Y < b.Y);
        }

        /// <summary>
        /// Determines whether the first specified vector comes before
        /// (has inferior sorting value than) the second vector, or it is equal to it.
        /// <para>Components have decreasing evaluation priority: first X, then Y.</para>
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>true if a.X is smaller than b.X, or a.X == b.X and a.Y &lt;= b.Y; otherwise, false.</returns>
        public static bool operator <=(Vector2f a, Vector2f b)
        {
            return (a.X < b.X) || (a.X == b.X && a.Y <= b.Y);
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than) the second vector.
        /// <para>Components have decreasing evaluation priority: first X, then Y.</para>
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>true if a.X is larger than b.X, or a.X == b.X and a.Y is larger than b.Y; otherwise, false.</returns>
        public static bool operator >(Vector2f a, Vector2f b)
        {
            return (a.X > b.X) || (a.X == b.X && a.Y > b.Y);
        }

        /// <summary>
        /// Determines whether the first specified vector comes after
        /// (has superior sorting value than) the second vector, or it is equal to it.
        /// <para>Components have decreasing evaluation priority: first X, then Y.</para>
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>true if a.X is larger than b.X, or a.X == b.X and a.Y &gt;= b.Y; otherwise, false.</returns>
        public static bool operator >=(Vector2f a, Vector2f b)
        {
            return (a.X > b.X) || (a.X == b.X && a.Y >= b.Y);
        }
    }

    /// <summary>
    /// Represents the three components of a vector in three-dimensional space,
    /// using <see cref="Single"/>-precision floating point numbers.
    /// </summary>
    //[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 12)]
    //[DebuggerDisplay("({m_x}, {m_y}, {m_z})")]
    [Serializable]
    public struct Vector3f : IEquatable<Vector3f>, IComparable<Vector3f>, IComparable, IEpsilonFComparable<Vector3f>
    {
        internal System.Numerics.Vector3 v;

        #region constructors



#if RHINO3DMIO
        public Vector3f(Rhino.Geometry.Vector3f rhinoNormal)
        {
            v.X = rhinoNormal.X;
            v.Y = rhinoNormal.Y;
            v.Z = rhinoNormal.Z;
        }

        public Rhino.Geometry.Vector3f RhinoObject()
        {
            return new Rhino.Geometry.Vector3f(v.X, v.Y, v.Z);
        }
#endif

        /// <summary>
        /// Constructs a new vector from 3 single precision numbers.
        /// </summary>
        /// <param name="x">X component of vector.</param>
        /// <param name="y">Y component of vector.</param>
        /// <param name="z">Z component of vector.</param>
        public Vector3f(float x, float y, float z)
        {
            v = new System.Numerics.Vector3();
            v.X = x;
            v.Y = y;
            v.Z = z;
        }

        #endregion

        #region static properties
        /// <summary>
        /// Gets the value of the vector with components 0,0,0.
        /// </summary>
        public static Vector3f Zero
        {
            get { return new Vector3f(0, 0, 0); }
        }

        /// <summary>
        /// Gets the value of the vector with components 1,0,0.
        /// </summary>
        public static Vector3f XAxis
        {
            get { return new Vector3f(1f, 0f, 0f); }
        }

        /// <summary>
        /// Gets the value of the vector with components 0,1,0.
        /// </summary>
        public static Vector3f YAxis
        {
            get { return new Vector3f(0f, 1f, 0f); }
        }

        /// <summary>
        /// Gets the value of the vector with components 0,0,1.
        /// </summary>
        public static Vector3f ZAxis
        {
            get { return new Vector3f(0f, 0f, 1f); }
        }

        /// <summary>
        /// Gets the value of the vector with each component set to RhinoMath.UnsetValue.
        /// </summary>
        public static Vector3f Unset
        {
            get { return new Vector3f(RhinoMath.UnsetSingle, RhinoMath.UnsetSingle, RhinoMath.UnsetSingle); }
        }
        #endregion static properties

        #region properties
        /// <summary>
        /// Gets or sets the X (first) component of this vector.
        /// </summary>
        public float X { get { return v.X; } set { v.X = value; } }

        /// <summary>
        /// Gets or sets the Y (second) component of this vector.
        /// </summary>
        public float Y { get { return v.Y; } set { v.Y = value; } }

        /// <summary>
        /// Gets or sets the Z (third) component of this vector.
        /// </summary>
        public float Z { get { return v.Z; } set { v.Z = value; } }
        #endregion

        /// <summary>
        /// Determines whether the specified System.Object is a Vector3f and has the same values as the present vector.
        /// </summary>
        /// <param name="obj">The specified object.</param>
        /// <returns>true if obj is Vector3f and has the same components as this; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector3f && this == (Vector3f)obj);
        }

        /// <summary>
        /// Determines whether the specified vector has the same values as the present vector.
        /// </summary>
        /// <param name="vector">The specified vector.</param>
        /// <returns>true if vector has the same components as this; otherwise false.</returns>
        public bool Equals(Vector3f vector)
        {
            return this == vector;
        }

        /// <summary>
        /// Check that all values in other are withing epsilon of the values in this
        /// </summary>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public bool EpsilonEquals(Vector3f other, float epsilon)
        {
            return RhinoMath.EpsilonEquals(v.X, other.v.X, epsilon) &&
                   RhinoMath.EpsilonEquals(v.Y, other.v.Y, epsilon) &&
                   RhinoMath.EpsilonEquals(v.Z, other.v.Z, epsilon);
        }

        /// <summary>
        /// Compares this <see cref="Vector3f" /> with another <see cref="Vector3f" />.
        /// <para>Component evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="other">The other <see cref="Vector3f" /> to use in comparison.</param>
        /// <returns>
        /// <para> 0: if this is identical to other</para>
        /// <para>-1: if this.X &lt; other.X</para>
        /// <para>-1: if this.X == other.X and this.Y &lt; other.Y</para>
        /// <para>-1: if this.X == other.X and this.Y == other.Y and this.Z &lt; other.Z</para>
        /// <para>+1: otherwise.</para>
        /// </returns>
        public int CompareTo(Vector3f other)
        {
            if (v.X < other.v.X)
                return -1;
            if (v.X > other.v.X)
                return 1;

            if (v.Y < other.v.Y)
                return -1;
            if (v.Y > other.v.Y)
                return 1;

            if (v.Z < other.v.Z)
                return -1;
            if (v.Z > other.v.Z)
                return 1;

            return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is Vector3f)
                return CompareTo((Vector3f)obj);

            throw new ArgumentException("Input must be of type Vector3f", "obj");
        }

        /// <summary>
        /// Computes a hash number that represents the current vector.
        /// </summary>
        /// <returns>A hash code that is not unique for each vector.</returns>
        public override int GetHashCode()
        {
            return v.GetHashCode();
        }

        /// <summary>
        /// Constructs the string representation of the current vector.
        /// </summary>
        /// <returns>The vector representation in the form X,Y,Z.</returns>
        public override string ToString()
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            return String.Format("{0},{1},{2}",
              v.X.ToString(culture), v.Y.ToString(culture), v.Z.ToString(culture));
        }

        /// <summary>
        /// Unitizes the vector in place. A unit vector has length 1 unit. 
        /// <para>An invalid or zero length vector cannot be unitized.</para>
        /// </summary>
        /// <returns>true on success or false on failure.</returns>
        public bool Unitize()
        {


            bool rc = false;

            float length = this.Length;

            if (length > 0)
            {
                v = System.Numerics.Vector3.Divide(v, length);
                rc = true;
            }

            return rc;
        }

        public void FastUnitize()
        {
            v = System.Numerics.Vector3.Normalize(v);
        }

        /// <summary>
        /// Transforms the vector in place.
        /// <para>The transformation matrix acts on the left of the vector; i.e.,</para>
        /// <para>result = transformation*vector</para>
        /// </summary>
        /// <param name="transformation">Transformation matrix to apply.</param>
        public void Transform(Transform transformation)
        {
            double xx = transformation.m_00 * v.X + transformation.m_01 * v.Y + transformation.m_02 * v.Z;
            double yy = transformation.m_10 * v.X + transformation.m_11 * v.Y + transformation.m_12 * v.Z;
            double zz = transformation.m_20 * v.X + transformation.m_21 * v.Y + transformation.m_22 * v.Z;

            v.X = (float)xx;
            v.Y = (float)yy;
            v.Z = (float)zz;
        }


        ///<summary>
        /// Reverses (inverts) this vector in place.
        /// <para>If this vector contains RhinoMath.UnsetValue, the 
        /// reverse will also be invalid and false will be returned.</para>
        ///</summary>
        ///<returns>true on success or false if the vector is invalid.</returns>
        public bool Reverse()
        {
            bool rc = true;

            if (RhinoMath.UnsetSingle != v.X) { v.X = -v.X; } else { rc = false; }
            if (RhinoMath.UnsetSingle != v.Y) { v.Y = -v.Y; } else { rc = false; }
            if (RhinoMath.UnsetSingle != v.Z) { v.Z = -v.Z; } else { rc = false; }

            return rc;
        }

        ///<summary>
        /// Sets this vector to be perpendicular to another vector. 
        /// Result is not unitized.
        ///</summary>
        /// <param name="other">Vector to use as guide.</param>
        ///<returns>true on success, false if input vector is zero or invalid.</returns>
        public bool PerpendicularTo(Vector3f other)
        {
            // TODO
            return false;
        }

        /// <summary>
        /// Determines whether two vectors have equal values.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if the components of the two vectors are exactly equal; otherwise false.</returns>
        public static bool operator ==(Vector3f a, Vector3f b)
        {
            return (a.v.X == b.v.X && a.v.Y == b.v.Y && a.v.Z == b.v.Z);
        }

        /// <summary>
        /// Determines whether two vectors have different values.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if the two vectors differ in any component; false otherwise.</returns>
        public static bool operator !=(Vector3f a, Vector3f b)
        {
            return (a.v.X != b.v.X || a.v.Y != b.v.Y || a.v.Z != b.v.Z);
        }

        /// <summary>
        /// Determines whether the first specified vector comes before
        /// (has inferior sorting value than) the second vector.
        /// <para>Components evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if a.X is smaller than b.X,
        /// or a.X == b.X and a.Y is smaller than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z is smaller than b.Z;
        /// otherwise, false.</returns>
        public static bool operator <(Vector3f a, Vector3f b)
        {
            return a.CompareTo(b) < 0;
        }

        /// <summary>
        /// Determines whether the first specified vector comes before
        /// (has inferior sorting value than) the second vector, or it is equal to it.
        /// <para>Components evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if a.X is smaller than b.X,
        /// or a.X == b.X and a.Y is smaller than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z &lt;= b.Z;
        /// otherwise, false.</returns>
        public static bool operator <=(Vector3f a, Vector3f b)
        {
            return a.CompareTo(b) <= 0;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than)
        /// the second vector.
        /// <para>Components evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if a.X is larger than b.X,
        /// or a.X == b.X and a.Y is larger than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z is larger than b.Z;
        /// otherwise, false.</returns>
        public static bool operator >(Vector3f a, Vector3f b)
        {
            return a.CompareTo(b) > 0;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than)
        /// the second vector, or it is equal to it.
        /// <para>Components evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if a.X is larger than b.X,
        /// or a.X == b.X and a.Y is larger than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z &gt;= b.Z;
        /// otherwise, false.</returns>
        public static bool operator >=(Vector3f a, Vector3f b)
        {
            return a.CompareTo(b) >= 0;
        }

        /// <summary>
        /// Sums up a point and a vector, and returns a new point.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new point that results from the addition of point and vector.</returns>
        public static Point3f operator +(Point3f point, Vector3f vector)
        {
            return new Point3f(point.p.X + vector.v.X, point.p.Y + vector.v.Y, point.p.Z + vector.v.Z);
        }

        /// <summary>
        /// Sums up a point and a vector, and returns a new point.
        /// <para>(Provided for languages that do not support operator overloading. You can use the + operator otherwise)</para>
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new point that results from the addition of point and vector.</returns>
        public static Point3f Add(Point3f point, Vector3f vector)
        {
            return new Point3f(point.p.X + vector.v.X, point.p.Y + vector.v.Y, point.p.Z + vector.v.Z);
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static Vector3f operator *(Vector3f vector, float t)
        {
            return new Vector3f(vector.v.X * t, vector.v.Y * t, vector.v.Z * t);
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// <para>(Provided for languages that do not support operator overloading. You can use the * operator otherwise)</para>
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static Vector3f Multiply(Vector3f vector, float t)
        {
            return new Vector3f(vector.v.X * t, vector.v.Y * t, vector.v.Z * t);
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// </summary>
        /// <param name="t">A number.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static Vector3f operator *(float t, Vector3f vector)
        {
            return new Vector3f(vector.v.X * t, vector.v.Y * t, vector.v.Z * t);
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// <para>(Provided for languages that do not support operator overloading. You can use the * operator otherwise)</para>
        /// </summary>
        /// <param name="t">A number.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static Vector3f Multiply(float t, Vector3f vector)
        {
            return new Vector3f(vector.v.X * t, vector.v.Y * t, vector.v.Z * t);
        }

        /// <summary>
        /// Computes the cross product (or vector product, or exterior product) of two vectors.
        /// <para>This operation is not commutative.</para>
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>A new vector that is perpendicular to both a and b,
        /// <para>has Length == a.Length * b.Length and</para>
        /// <para>with a result that is oriented following the right hand rule.</para>
        /// </returns>
        public static Vector3f CrossProduct(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.v.Y * b.v.Z - b.v.Y * a.v.Z, a.v.Z * b.v.X - b.v.Z * a.v.X, a.v.X * b.v.Y - b.v.X * a.v.Y);
        }

        /// <summary>
        /// Computes the length (or magnitude, or size) of this vector.
        /// This is an application of Pythagoras' theorem.
        /// If this vector is invalid, its length is considered 0.
        /// </summary>
        public float Length
        {
            get { return GetLengthHelper(v); }
        }

        public float FastLenght { get { return v.Length(); } }

        internal static float GetLengthHelper(System.Numerics.Vector3 v)
        {
            if (!RhinoMath.IsValidSingle(v.X) ||
                !RhinoMath.IsValidSingle(v.Y) ||
                !RhinoMath.IsValidSingle(v.Z))
                return 0f;

            float len;

            System.Numerics.Vector3 f = System.Numerics.Vector3.Abs(v);

            if (f.Y >= f.X && f.Y >= f.Z)
            {
                len = f.X; f.X = f.Y; f.Y = len;
            }
            else if (f.Z >= f.X && f.Z >= f.Y)
            {
                len = f.X; f.X = f.Z; f.Z = len;
            }

            // 15 September 2003 Dale Lear
            //     For small denormalized doubles (positive but smaller
            //     than DBL_MIN), some compilers/FPUs set 1.0/fx to +INF.
            //     Without the ON_DBL_MIN test we end up with
            //     microscopic vectors that have infinite length!
            //
            //     Since this code starts with doubles, none of this
            //     should be necessary, but it doesn't hurt anything.
            const float ON_SINGLE_MIN = (float)2.2250738585072014e-308;
            if (f.X > ON_SINGLE_MIN)
            {
                len = 1f / f.X;
                f.X *= len;
                f.Z *= len;
                len = f.X * (float)Math.Sqrt(1.0 + f.Y * f.Y + f.Z * f.Z);
            }
            else if (f.X> 0.0 && RhinoMath.IsValidSingle(f.X))
                len = f.X;
            else
                len = 0f;

            return len;
        }
    }
}